using System;
using System.Windows.Forms;
using CondominioApp.Models;
using CondominioApp.Repositories;

namespace CondominioApp.Forms
{
    public partial class FormNotificacoes : Form
    {
        private readonly NotificacaoRepository _repo = new NotificacaoRepository();
        private readonly CondominoRepository _repoCondomino = new CondominoRepository();
        private int _idSelecionado = -1;

        public FormNotificacoes()
        {
            InitializeComponent();
        }

        // ── Quando o formulário abre ──────────────────────────────────
        private void FormNotificacoes_Load(object sender, EventArgs e)
        {
            CarregarCondominos();
            CarregarGrelha();
            btnApagar.Enabled = false;
        }

        // ── Preenche o ComboBox com os condóminos ─────────────────────
        private void CarregarCondominos()
        {
            var lista = _repoCondomino.GetAll();
            cmbCondomino.DataSource = lista;
            cmbCondomino.DisplayMember = "Nome";
            cmbCondomino.ValueMember = "IdCondomino";
            cmbCondomino.SelectedIndex = -1;
        }

        // ── Carrega as notificações na grelha ─────────────────────────
        private void CarregarGrelha()
        {
            FiltrarGrelha(txtPesquisa.Text.Trim());
        }

        private void FiltrarGrelha(string termo)
        {
            var lista = _repo.GetAll();

            dgvNotificacoes.Rows.Clear();
            dgvNotificacoes.Columns.Clear();

            dgvNotificacoes.Columns.Add("colId", "ID");
            dgvNotificacoes.Columns.Add("colCondomino", "Condómino");
            dgvNotificacoes.Columns.Add("colMensagem", "Mensagem");
            dgvNotificacoes.Columns.Add("colData", "Data Envio");

            dgvNotificacoes.Columns["colId"].Width = 40;
            dgvNotificacoes.Columns["colCondomino"].Width = 160;
            dgvNotificacoes.Columns["colMensagem"].Width = 340;
            dgvNotificacoes.Columns["colData"].Width = 120;

            foreach (var n in lista)
            {
                bool corresponde =
                    string.IsNullOrEmpty(termo) ||
                    n.Condomino.Nome.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    n.Mensagem.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    n.DataEnvio.ToString("dd/MM/yyyy HH:mm").IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0;

                if (!corresponde) continue;

                dgvNotificacoes.Rows.Add(
                    n.IdNotificacao,
                    n.Condomino.Nome,
                    n.Mensagem,
                    n.DataEnvio.ToString("dd/MM/yyyy HH:mm")
                );
            }
        }

        // ── Botão Registar ────────────────────────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                var nova = new Notificacao
                {
                    Mensagem = txtMensagem.Text.Trim(),
                    IdCondomino = (int)cmbCondomino.SelectedValue
                };

                _repo.Insert(nova);
                MessageBox.Show("Notificação registada com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparCampos();
                CarregarGrelha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registar:\n\n" + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Botão Apagar ──────────────────────────────────────────────
        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dgvNotificacoes.CurrentRow == null) return;

            var id = Convert.ToInt32(dgvNotificacoes.CurrentRow.Cells["colId"].Value);
            var condomino = dgvNotificacoes.CurrentRow.Cells["colCondomino"].Value.ToString();

            var confirmacao = MessageBox.Show(
                $"Tens a certeza que queres apagar a notificação de '{condomino}'?\n\nEsta ação não pode ser desfeita.",
                "Confirmar eliminação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    _repo.Delete(id);
                    MessageBox.Show("Notificação eliminada com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarGrelha();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao apagar:\n\n" + ex.Message, "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── Botão Limpar ──────────────────────────────────────────────
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        // ── Clique numa linha da grelha ───────────────────────────────
        private void dgvNotificacoes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNotificacoes.CurrentRow != null)
            {
                _idSelecionado = Convert.ToInt32(dgvNotificacoes.CurrentRow.Cells["colId"].Value);
                btnApagar.Enabled = true;
            }
        }

        // ── Métodos auxiliares ────────────────────────────────────────
        private bool ValidarCampos()
        {
            if (cmbCondomino.SelectedIndex == -1)
            {
                MessageBox.Show("Seleciona um condómino.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMensagem.Text))
            {
                MessageBox.Show("O campo Mensagem é obrigatório.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMensagem.Focus();
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            cmbCondomino.SelectedIndex = -1;
            txtMensagem.Clear();
            _idSelecionado = -1;
            btnApagar.Enabled = false;
            dgvNotificacoes.ClearSelection();
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            FiltrarGrelha(txtPesquisa.Text.Trim());
        }
    }
}