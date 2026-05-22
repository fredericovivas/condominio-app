using System;
using System.Windows.Forms;
using CondominioApp.Models;
using CondominioApp.Repositories;

namespace CondominioApp.Forms
{
    public partial class FormIntervencoes : Form
    {
        private readonly IntervencaoRepository _repo = new IntervencaoRepository();
        private readonly EventoRepository _repoEvento = new EventoRepository();


        public FormIntervencoes()
        {
            InitializeComponent();
        }

        // ── Quando o formulário abre ──────────────────────────────────
        private void FormIntervencoes_Load(object sender, EventArgs e)
        {
            CarregarEventos();
            CarregarGrelha();
            btnApagar.Enabled = false;
        }

        // ── Preenche o ComboBox com os eventos existentes ─────────────
        private void CarregarEventos()
        {
            var lista = _repoEvento.GetAll();
            cmbEvento.DataSource = lista;
            cmbEvento.DisplayMember = "Titulo";
            cmbEvento.ValueMember = "IdEvento";
            cmbEvento.SelectedIndex = -1;
        }

        // ── Carrega as intervenções na grelha ─────────────────────────
        private void CarregarGrelha()
        {
            FiltrarGrelha(txtPesquisa.Text.Trim());
        }

        private void FiltrarGrelha(string termo)
        {
            var lista = _repo.GetAll();

            dgvIntervencoes.Rows.Clear();
            dgvIntervencoes.Columns.Clear();

            dgvIntervencoes.Columns.Add("colId", "ID");
            dgvIntervencoes.Columns.Add("colEvento", "Evento");
            dgvIntervencoes.Columns.Add("colDescricao", "Descrição");
            dgvIntervencoes.Columns.Add("colCusto", "Custo (€)");
            dgvIntervencoes.Columns.Add("colData", "Data");

            dgvIntervencoes.Columns["colId"].Width = 40;
            dgvIntervencoes.Columns["colEvento"].Width = 160;
            dgvIntervencoes.Columns["colDescricao"].Width = 240;
            dgvIntervencoes.Columns["colCusto"].Width = 80;
            dgvIntervencoes.Columns["colData"].Width = 120;

            foreach (var i in lista)
            {
                string custoTexto = i.Custo.HasValue ? i.Custo.Value.ToString("F2") : "-";

                bool corresponde =
                    string.IsNullOrEmpty(termo) ||
                    i.Evento.Titulo.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    i.Descricao.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    custoTexto.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    i.Data.ToString("dd/MM/yyyy HH:mm").IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0;

                if (!corresponde) continue;

                dgvIntervencoes.Rows.Add(
                    i.IdIntervencao,
                    i.Evento.Titulo,
                    i.Descricao,
                    custoTexto,
                    i.Data.ToString("dd/MM/yyyy HH:mm")
                );
            }
        }

        // ── Botão Guardar ─────────────────────────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                decimal? custo = null;
                if (!string.IsNullOrWhiteSpace(txtCusto.Text))
                {
                    if (decimal.TryParse(txtCusto.Text.Replace(",", "."),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal custoValor))
                    {
                        custo = custoValor;
                    }
                }

                var nova = new Intervencao
                {
                    Descricao = txtDescricao.Text.Trim(),
                    Custo = custo,
                    Data = dtpData.Value,
                    IdEvento = (int)cmbEvento.SelectedValue
                };

                _repo.Insert(nova);
                MessageBox.Show("Intervenção registada com sucesso!", "Sucesso",
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

        // ── Botão Apagar ──────────────────────────────────────────────
        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dgvIntervencoes.CurrentRow == null) return;

            var id = Convert.ToInt32(dgvIntervencoes.CurrentRow.Cells["colId"].Value);
            var desc = dgvIntervencoes.CurrentRow.Cells["colDescricao"].Value.ToString();

            var confirmacao = MessageBox.Show(
                $"Tens a certeza que queres apagar a intervenção '{desc}'?\n\nEsta ação não pode ser desfeita.",
                "Confirmar eliminação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    _repo.Delete(id);
                    MessageBox.Show("Intervenção eliminada com sucesso!", "Sucesso",
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
        private void dgvIntervencoes_SelectionChanged(object sender, EventArgs e)
        {
            btnApagar.Enabled = dgvIntervencoes.CurrentRow != null;
        }

        // ── Métodos auxiliares ────────────────────────────────────────
        private bool ValidarCampos()
        {
            if (cmbEvento.SelectedIndex == -1)
            {
                MessageBox.Show("Seleciona um evento.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("O campo Descrição é obrigatório.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescricao.Focus();
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            cmbEvento.SelectedIndex = -1;
            txtDescricao.Clear();
            txtCusto.Clear();
            dtpData.Value = DateTime.Now;
            btnApagar.Enabled = false;
            dgvIntervencoes.ClearSelection();
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            FiltrarGrelha(txtPesquisa.Text.Trim());
        }
    }
}