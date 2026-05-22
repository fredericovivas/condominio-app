using System;
using System.Windows.Forms;
using CondominioApp.Models;
using CondominioApp.Repositories;

namespace CondominioApp.Forms
{
    public partial class FormPagamentos : Form
    {
        private readonly PagamentoRepository _repoPagamento = new PagamentoRepository();
        private readonly CondominoRepository _repoCondomino = new CondominoRepository();
        private readonly EventoRepository _repoEvento = new EventoRepository();
        private int _idSelecionado = -1;

        public FormPagamentos()
        {
            InitializeComponent();
        }

        // -- Refresh ao abrir --------------
        private void FormPagamentos_Activated(object sender, EventArgs e)
        {
            CarregarCondominos();
            CarregarEventos();
            CarregarGrelha();
        }

        // ── Quando o formulário abre ──────────────────────────────────
        private void FormPagamentos_Load(object sender, EventArgs e)
        {
            CarregarCondominos();
            CarregarEventos();
            CarregarEstados();
            CarregarGrelha();
            btnMarcarPago.Enabled = false;
            btnApagar.Enabled = false;
        }

        // ── Preenche os ComboBoxes ────────────────────────────────────
        private void CarregarCondominos()
        {
            var lista = _repoCondomino.GetAll();
            cmbCondomino.DataSource = lista;
            cmbCondomino.DisplayMember = "Nome";
            cmbCondomino.ValueMember = "IdCondomino";
            cmbCondomino.SelectedIndex = -1;
        }

        private void CarregarEventos()
        {
            var lista = _repoEvento.GetPrazosPagamento();
            cmbEvento.DataSource = lista;
            cmbEvento.DisplayMember = "Titulo";
            cmbEvento.ValueMember = "IdEvento";
            cmbEvento.SelectedIndex = -1;
        }

        private void CarregarEstados()
        {
            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Em dívida");
            cmbEstado.Items.Add("Pago");
            cmbEstado.SelectedIndex = 0;
        }

        // ── Carrega os pagamentos na grelha ───────────────────────────
        private void CarregarGrelha()
        {
            FiltrarGrelha(txtPesquisa.Text.Trim());
        }

        private void FiltrarGrelha(string termo)
        {
            var lista = _repoPagamento.GetAll();

            dgvPagamentos.Rows.Clear();
            dgvPagamentos.Columns.Clear();

            dgvPagamentos.Columns.Add("colId", "ID");
            dgvPagamentos.Columns.Add("colCondomino", "Condómino");
            dgvPagamentos.Columns.Add("colEvento", "Evento");
            dgvPagamentos.Columns.Add("colValor", "Valor (€)");
            dgvPagamentos.Columns.Add("colEstado", "Estado");
            dgvPagamentos.Columns.Add("colData", "Data Registo");

            dgvPagamentos.Columns["colId"].Width = 40;
            dgvPagamentos.Columns["colCondomino"].Width = 160;
            dgvPagamentos.Columns["colEvento"].Width = 160;
            dgvPagamentos.Columns["colValor"].Width = 80;
            dgvPagamentos.Columns["colEstado"].Width = 90;
            dgvPagamentos.Columns["colData"].Width = 120;

            foreach (var p in lista)
            {
                string estadoTexto = p.Estado == EstadoPagamento.Pago ? "Pago" : "Em dívida";

                bool corresponde =
                    string.IsNullOrEmpty(termo) ||
                    p.Condomino.Nome.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.Evento.Titulo.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    estadoTexto.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.Valor.ToString("F2").IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0;

                if (!corresponde) continue;

                var row = dgvPagamentos.Rows.Add(
                    p.IdPagamento,
                    p.Condomino.Nome,
                    p.Evento.Titulo,
                    p.Valor.ToString("F2"),
                    estadoTexto,
                    p.DataRegisto.ToString("dd/MM/yyyy HH:mm")
                );

                if (p.Estado == EstadoPagamento.EmDivida)
                    dgvPagamentos.Rows[row].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }
        }

        // ── Botão Guardar ─────────────────────────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                var estado = cmbEstado.SelectedIndex == 1
                    ? EstadoPagamento.Pago
                    : EstadoPagamento.EmDivida;

                var novo = new Pagamento
                {
                    Valor = decimal.Parse(txtValor.Text.Trim()),
                    Estado = estado,
                    IdCondomino = (int)cmbCondomino.SelectedValue,
                    IdEvento = (int)cmbEvento.SelectedValue
                };

                _repoPagamento.Insert(novo);
                MessageBox.Show("Pagamento registado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparCampos();
                CarregarGrelha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao guardar:\n\n" + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Botão Marcar Pago ─────────────────────────────────────────
        private void btnMarcarPago_Click(object sender, EventArgs e)
        {
            if (_idSelecionado == -1) return;

            try
            {
                _repoPagamento.UpdateEstado(_idSelecionado, EstadoPagamento.Pago);
                MessageBox.Show("Pagamento marcado como pago!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
                CarregarGrelha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:\n\n" + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Botão Apagar ──────────────────────────────────────────────
        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dgvPagamentos.CurrentRow == null) return;

            var id = Convert.ToInt32(dgvPagamentos.CurrentRow.Cells["colId"].Value);
            var condomino = dgvPagamentos.CurrentRow.Cells["colCondomino"].Value.ToString();

            var confirmacao = MessageBox.Show(
                $"Tens a certeza que queres apagar o pagamento de '{condomino}'?\n\nEsta ação não pode ser desfeita.",
                "Confirmar eliminação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    _repoPagamento.Delete(id);
                    MessageBox.Show("Pagamento eliminado com sucesso!", "Sucesso",
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

        // ── Botão Ver Devedores ───────────────────────────────────────
        private void btnDevedores_Click(object sender, EventArgs e)
        {
            var lista = _repoPagamento.GetDevedores();

            dgvPagamentos.Rows.Clear();
            dgvPagamentos.Columns.Clear();

            dgvPagamentos.Columns.Add("colId", "ID");
            dgvPagamentos.Columns.Add("colCondomino", "Condómino");
            dgvPagamentos.Columns.Add("colEvento", "Evento");
            dgvPagamentos.Columns.Add("colValor", "Valor (€)");
            dgvPagamentos.Columns.Add("colEstado", "Estado");
            dgvPagamentos.Columns.Add("colData", "Data Registo");

            dgvPagamentos.Columns["colId"].Width = 40;
            dgvPagamentos.Columns["colCondomino"].Width = 160;
            dgvPagamentos.Columns["colEvento"].Width = 160;
            dgvPagamentos.Columns["colValor"].Width = 80;
            dgvPagamentos.Columns["colEstado"].Width = 90;
            dgvPagamentos.Columns["colData"].Width = 120;

            foreach (var p in lista)
            {
                var row = dgvPagamentos.Rows.Add(
                    p.IdPagamento,
                    p.Condomino.Nome,
                    p.Evento.Titulo,
                    p.Valor.ToString("F2"),
                    "Em dívida",
                    p.DataRegisto.ToString("dd/MM/yyyy HH:mm")
                );
                dgvPagamentos.Rows[row].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            }

            MessageBox.Show($"A mostrar {lista.Count} pagamento(s) em dívida.", "Devedores",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ── Botão Limpar ──────────────────────────────────────────────
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            CarregarGrelha();
        }

        // ── Clique numa linha da grelha ───────────────────────────────
        private void dgvPagamentos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPagamentos.CurrentRow == null) return;

            _idSelecionado = Convert.ToInt32(dgvPagamentos.CurrentRow.Cells["colId"].Value);
            var estado = dgvPagamentos.CurrentRow.Cells["colEstado"].Value.ToString();

            btnMarcarPago.Enabled = (estado == "Em dívida");
            btnApagar.Enabled = true;
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

            if (cmbEvento.SelectedIndex == -1)
            {
                MessageBox.Show("Seleciona um evento.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtValor.Text) ||
                !decimal.TryParse(txtValor.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("O valor deve ser um número válido (ex: 50.00).", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValor.Focus();
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            cmbCondomino.SelectedIndex = -1;
            cmbEvento.SelectedIndex = -1;
            cmbEstado.SelectedIndex = 0;
            txtValor.Clear();
            _idSelecionado = -1;
            btnMarcarPago.Enabled = false;
            btnApagar.Enabled = false;
            dgvPagamentos.ClearSelection();
        }

        private void FormPagamentos_Activated_1(object sender, EventArgs e)
        {

        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            FiltrarGrelha(txtPesquisa.Text.Trim());
        }
    }
}