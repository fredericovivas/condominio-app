using System;
using System.Drawing;
using System.Windows.Forms;
using CondominioApp.Repositories;

namespace CondominioApp
{
    public partial class FormMain : Form
    {
        private readonly ApartamentoRepository _repoApartamento = new ApartamentoRepository();
        private readonly CondominoRepository _repoCondomino = new CondominoRepository();
        private readonly EventoRepository _repoEvento = new EventoRepository();
        private readonly PagamentoRepository _repoPagamento = new PagamentoRepository();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            AtualizarDashboard();
        }

        // ── Dashboard ─────────────────────────────────────────────────
        private void AtualizarDashboard()
        {
            try
            {
                // Totais
                var apartamentos = _repoApartamento.GetAll();
                var condominos = _repoCondomino.GetAll();
                var eventos = _repoEvento.GetAll();
                var devedores = _repoPagamento.GetDevedores();

                lblNumApartamentos.Text = apartamentos.Count.ToString();
                lblNumCondominos.Text = condominos.Count.ToString();
                lblNumEventos.Text = eventos.Count.ToString();
                lblNumDevedores.Text = devedores.Count.ToString();

                // Total em dívida
                decimal totalDivida = 0;
                foreach (var d in devedores)
                    totalDivida += d.Valor;

                lblDescDevedores.Text = $"Em dívida (€ {totalDivida:F2})";

                // Próximos eventos (a partir de hoje, ordenados por data)
                CarregarProximosEventos(eventos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dashboard:\n\n" + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarProximosEventos(System.Collections.Generic.List<Models.Evento> eventos)
        {
            dgvProximosEventos.Rows.Clear();
            dgvProximosEventos.Columns.Clear();

            dgvProximosEventos.Columns.Add("colData", "Data");
            dgvProximosEventos.Columns.Add("colTitulo", "Título");
            dgvProximosEventos.Columns.Add("colTipo", "Tipo");
            dgvProximosEventos.Columns.Add("colDesc", "Descrição");

            dgvProximosEventos.Columns["colData"].Width = 130;
            dgvProximosEventos.Columns["colTitulo"].Width = 250;
            dgvProximosEventos.Columns["colTipo"].Width = 150;
            dgvProximosEventos.Columns["colDesc"].Width = 350;

            var hoje = DateTime.Today;

            foreach (var ev in eventos)
            {
                if (ev.Data < hoje) continue; // ignora eventos passados

                string tipoTexto;
                switch (ev.Tipo)
                {
                    case Models.TipoEvento.Manutencao:
                        tipoTexto = "Manutenção"; break;
                    case Models.TipoEvento.PrazoPagamento:
                        tipoTexto = "Prazo Pagamento"; break;
                    default:
                        tipoTexto = "Reunião"; break;
                }

                var row = dgvProximosEventos.Rows.Add(
                    ev.Data.ToString("dd/MM/yyyy HH:mm"),
                    ev.Titulo,
                    tipoTexto,
                    ev.Descricao
                );

                // Colore a linha conforme o tipo
                if (ev.Tipo == Models.TipoEvento.PrazoPagamento)
                    dgvProximosEventos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                else if (ev.Tipo == Models.TipoEvento.Manutencao)
                    dgvProximosEventos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 220);
            }
        }

        // ── Navegação ─────────────────────────────────────────────────
        private void btnApartamentos_Click(object sender, EventArgs e)
        {
            new Forms.FormApartamentos().ShowDialog();
            AtualizarDashboard();
        }

        private void btnCondominos_Click(object sender, EventArgs e)
        {
            new Forms.FormCondominos().ShowDialog();
            AtualizarDashboard();
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            new Forms.FormEventos().ShowDialog();
            AtualizarDashboard();
        }

        private void btnPagamentos_Click(object sender, EventArgs e)
        {
            new Forms.FormPagamentos().ShowDialog();
            AtualizarDashboard();
        }

        private void btnIntervencoes_Click(object sender, EventArgs e)
        {
            new Forms.FormIntervencoes().ShowDialog();
            AtualizarDashboard();
        }

        private void btnNotificacoes_Click(object sender, EventArgs e)
        {
            new Forms.FormNotificacoes().ShowDialog();
            AtualizarDashboard();
        }

        // ── Efeito hover ──────────────────────────────────────────────
        private void BtnOnMouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.BackColor = Color.FromArgb(41, 84, 163);
                btn.ForeColor = Color.White;
            }
        }

        private void BtnOnMouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(41, 84, 163);
            }
        }
    }
}