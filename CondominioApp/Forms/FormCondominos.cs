using System;
using System.Windows.Forms;
using CondominioApp.Models;
using CondominioApp.Repositories;

namespace CondominioApp.Forms
{
    public partial class FormCondominos : Form
    {
        private readonly CondominoRepository _repoCondomino = new CondominoRepository();
        private readonly ApartamentoRepository _repoApartamento = new ApartamentoRepository();
        private int _idSelecionado = -1;

        public FormCondominos()
        {
            InitializeComponent();
        }

        // ── Quando o formulário abre ──────────────────────────────────
        private void FormCondominos_Load(object sender, EventArgs e)
        {
            CarregarApartamentos();
            CarregarGrelha();
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
        }

        private void FormCondominos_Activated(object sender, EventArgs e)
        {
            CarregarApartamentos();
            CarregarGrelha();
        }

        // ── Preenche o ComboBox com os apartamentos existentes ────────
        private void CarregarApartamentos()
        {
            var lista = _repoApartamento.GetAll();

            lista.Sort((a, b) =>
            {
                int numA, numB;
                bool aIsNum = int.TryParse(a.Numero, out numA);
                bool bIsNum = int.TryParse(b.Numero, out numB);

                if (aIsNum && bIsNum)
                    return numA.CompareTo(numB);

                return string.Compare(a.Numero, b.Numero, StringComparison.Ordinal);
            });

            cmbApartamento.DataSource = null;
            cmbApartamento.Items.Clear();
            cmbApartamento.DataSource = lista;
            cmbApartamento.DisplayMember = "Display";
            cmbApartamento.ValueMember = "IdApartamento";
            cmbApartamento.SelectedIndex = -1;
        }

        // ── Carrega os condóminos na grelha ───────────────────────────
        private void CarregarGrelha()
        {
            FiltrarGrelha(txtPesquisa.Text.Trim());
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            FiltrarGrelha(txtPesquisa.Text.Trim());
        }

        private void FiltrarGrelha(string termo)
        {
            var lista = _repoCondomino.GetAll();

            dgvCondominos.Rows.Clear();
            dgvCondominos.Columns.Clear();

            dgvCondominos.Columns.Add("colId", "ID");
            dgvCondominos.Columns.Add("colNome", "Nome");
            dgvCondominos.Columns.Add("colEmail", "Email");
            dgvCondominos.Columns.Add("colTelefone", "Telefone");
            dgvCondominos.Columns.Add("colApartamento", "Apartamento");
            dgvCondominos.Columns.Add("colIdApt", "IdApt");

            dgvCondominos.Columns["colId"].Width = 40;
            dgvCondominos.Columns["colNome"].Width = 160;
            dgvCondominos.Columns["colEmail"].Width = 160;
            dgvCondominos.Columns["colTelefone"].Width = 100;
            dgvCondominos.Columns["colApartamento"].Width = 180;
            dgvCondominos.Columns["colIdApt"].Visible = false;

            foreach (var c in lista)
            {
                bool corresponde =
                    string.IsNullOrEmpty(termo) ||
                    c.Nome.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    c.Email.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    c.Telefone.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    c.Apartamento.Display.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0;

                if (!corresponde) continue;

                dgvCondominos.Rows.Add(
                    c.IdCondomino,
                    c.Nome,
                    c.Email,
                    c.Telefone,
                    c.Apartamento.ToString(),
                    c.IdApartamento
                );
            }
        }

        // ── Botão Guardar ─────────────────────────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                var idApt = (int)cmbApartamento.SelectedValue;

                if (_idSelecionado == -1)
                {
                    var novo = new Condomino
                    {
                        Nome = txtNome.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Telefone = txtTelefone.Text.Trim(),
                        IdApartamento = idApt
                    };
                    _repoCondomino.Insert(novo);
                    MessageBox.Show("Condómino registado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var atualizado = new Condomino
                    {
                        IdCondomino = _idSelecionado,
                        Nome = txtNome.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Telefone = txtTelefone.Text.Trim(),
                        IdApartamento = idApt
                    };
                    _repoCondomino.Update(atualizado);
                    MessageBox.Show("Condómino atualizado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimparCampos();
                CarregarGrelha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao guardar:\n\n" + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Botão Editar ──────────────────────────────────────────────
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCondominos.CurrentRow == null) return;

            _idSelecionado = Convert.ToInt32(dgvCondominos.CurrentRow.Cells["colId"].Value);
            txtNome.Text = dgvCondominos.CurrentRow.Cells["colNome"].Value.ToString();
            txtEmail.Text = dgvCondominos.CurrentRow.Cells["colEmail"].Value.ToString();
            txtTelefone.Text = dgvCondominos.CurrentRow.Cells["colTelefone"].Value.ToString();

            var idApt = Convert.ToInt32(dgvCondominos.CurrentRow.Cells["colIdApt"].Value);
            cmbApartamento.SelectedValue = idApt;

            btnGuardar.Text = "Atualizar";
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
        }

        // ── Botão Apagar ──────────────────────────────────────────────
        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dgvCondominos.CurrentRow == null) return;

            var id = Convert.ToInt32(dgvCondominos.CurrentRow.Cells["colId"].Value);
            var nome = dgvCondominos.CurrentRow.Cells["colNome"].Value.ToString();

            var confirmacao = MessageBox.Show(
                $"Tens a certeza que queres apagar o condómino '{nome}'?\n\nEsta ação não pode ser desfeita.",
                "Confirmar eliminação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    _repoCondomino.Delete(id);
                    MessageBox.Show("Condómino eliminado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarGrelha();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("foreign key constraint"))
                    {
                        MessageBox.Show(
                            $"Não é possível apagar o condómino '{nome}' porque tem pagamentos ou notificações associadas.\n\nElimina primeiro os registos associados.",
                            "Ação não permitida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao apagar:\n\n" + ex.Message, "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ── Botão Limpar ──────────────────────────────────────────────
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        // ── Clique numa linha da grelha ───────────────────────────────
        private void dgvCondominos_SelectionChanged(object sender, EventArgs e)
        {
            bool temSelecao = dgvCondominos.CurrentRow != null;
            btnEditar.Enabled = temSelecao;
            btnApagar.Enabled = temSelecao;
        }

        // ── Métodos auxiliares ────────────────────────────────────────
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }

            if (cmbApartamento.SelectedIndex == -1)
            {
                MessageBox.Show("Seleciona um apartamento.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbApartamento.Focus();
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            cmbApartamento.SelectedIndex = -1;
            _idSelecionado = -1;
            btnGuardar.Text = "Guardar";
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
            dgvCondominos.ClearSelection();
        }
    }
}