using System;
using System.Windows.Forms;
using CondominioApp.Models;
using CondominioApp.Repositories;

namespace CondominioApp.Forms
{
    public partial class FormApartamentos : Form
    {
        private readonly ApartamentoRepository _repo = new ApartamentoRepository();
        private int _idSelecionado = -1; // guarda o id do registo selecionado na grelha

        public FormApartamentos()
        {
            InitializeComponent();
            this.Load += FormApartamentos_Load;
            txtPesquisa.TextChanged += txtPesquisa_TextChanged;
            dgvApartamentos.SelectionChanged += dgvApartamentos_SelectionChanged;
        }

        // ── Quando o formulário abre ──────────────────────────────────
        private void FormApartamentos_Load(object sender, EventArgs e)
        {
            CarregarGrelha();
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
        }

        private void FormApartamentos_Activated(object sender, EventArgs e)
        {
            CarregarGrelha();
        }

        // ── Carrega os dados na grelha ────────────────────────────────
        private void CarregarGrelha()
        {
            FiltrarGrelha(txtPesquisa.Text.Trim());
        }

        private void FiltrarGrelha(string termo)
        {
            var lista = _repo.GetAll();

            dgvApartamentos.Rows.Clear();
            dgvApartamentos.Columns.Clear();

            dgvApartamentos.Columns.Add("colId", "ID");
            dgvApartamentos.Columns.Add("colBloco", "Bloco");
            dgvApartamentos.Columns.Add("colPiso", "Piso");
            dgvApartamentos.Columns.Add("colNumero", "Número");

            dgvApartamentos.Columns["colId"].Width = 50;
            dgvApartamentos.Columns["colBloco"].Width = 150;
            dgvApartamentos.Columns["colPiso"].Width = 100;
            dgvApartamentos.Columns["colNumero"].Width = 100;

            foreach (var a in lista)
            {
                bool corresponde =
                    string.IsNullOrEmpty(termo) ||
                    a.Bloco.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    a.Numero.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    a.Piso.ToString().IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0;

                if (!corresponde) continue;

                dgvApartamentos.Rows.Add(
                    a.IdApartamento,
                    a.Bloco,
                    a.Piso,
                    a.Numero
                );
            }

        }

        // ── Botão Guardar ─────────────────────────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                if (_idSelecionado == -1)
                {
                    // Inserir novo
                    var novo = new Apartamento
                    {
                        Bloco = txtBloco.Text.Trim(),
                        Piso = int.Parse(txtPiso.Text.Trim()),
                        Numero = txtNumero.Text.Trim()
                    };
                    _repo.Insert(novo);
                    MessageBox.Show("Apartamento registado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Atualizar existente
                    var atualizado = new Apartamento
                    {
                        IdApartamento = _idSelecionado,
                        Bloco = txtBloco.Text.Trim(),
                        Piso = int.Parse(txtPiso.Text.Trim()),
                        Numero = txtNumero.Text.Trim()
                    };
                    _repo.Update(atualizado);
                    MessageBox.Show("Apartamento atualizado com sucesso!", "Sucesso",
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
            if (dgvApartamentos.CurrentRow == null) return;

            _idSelecionado = Convert.ToInt32(dgvApartamentos.CurrentRow.Cells["colId"].Value);
            txtBloco.Text = dgvApartamentos.CurrentRow.Cells["colBloco"].Value.ToString();
            txtPiso.Text = dgvApartamentos.CurrentRow.Cells["colPiso"].Value.ToString();
            txtNumero.Text = dgvApartamentos.CurrentRow.Cells["colNumero"].Value.ToString();

            btnGuardar.Text = "Atualizar";
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
        }

        // ── Botão Apagar ──────────────────────────────────────────────
        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dgvApartamentos.CurrentRow == null) return;

            var id = Convert.ToInt32(dgvApartamentos.CurrentRow.Cells["colId"].Value);
            var bloco = dgvApartamentos.CurrentRow.Cells["colBloco"].Value.ToString();
            var num = dgvApartamentos.CurrentRow.Cells["colNumero"].Value.ToString();

            var confirmacao = MessageBox.Show(
                $"Tens a certeza que queres apagar o apartamento Bloco {bloco} - Nº {num}?\n\nEsta ação não pode ser desfeita.",
                "Confirmar eliminação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    _repo.Delete(id);
                    MessageBox.Show("Apartamento eliminado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarGrelha();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("foreign key constraint"))
                    {
                        MessageBox.Show(
                            "Não é possível apagar este registo porque tem dados associados.\n\nElimina primeiro os registos relacionados.",
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
        private void dgvApartamentos_SelectionChanged(object sender, EventArgs e)
        {
            bool temSelecao = dgvApartamentos.CurrentRow != null;
            btnEditar.Enabled = temSelecao;
            btnApagar.Enabled = temSelecao;
        }

        // ── Métodos auxiliares ────────────────────────────────────────
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtBloco.Text))
            {
                MessageBox.Show("O campo Bloco é obrigatório.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBloco.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPiso.Text) || !int.TryParse(txtPiso.Text, out _))
            {
                MessageBox.Show("O campo Piso deve ser um número inteiro.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPiso.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("O campo Número é obrigatório.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            txtBloco.Clear();
            txtPiso.Clear();
            txtNumero.Clear();
            _idSelecionado = -1;
            btnGuardar.Text = "Guardar";
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
            dgvApartamentos.ClearSelection();
        }

        private void dgvApartamentos_SelectionChanged_1(object sender, EventArgs e)
        {
            bool temSelecao = dgvApartamentos.CurrentRow != null;
            btnEditar.Enabled = temSelecao;
            btnApagar.Enabled = temSelecao;
        }

        private void FormApartamentos_Load_1(object sender, EventArgs e)
        {

        }

        private void FormApartamentos_Activated_1(object sender, EventArgs e)
        {

        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            FiltrarGrelha(txtPesquisa.Text.Trim());
        }
    }
}