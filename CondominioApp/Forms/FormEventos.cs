using System;
using System.Windows.Forms;
using CondominioApp.Models;
using CondominioApp.Repositories;

namespace CondominioApp.Forms
{
    public partial class FormEventos : Form
    {
        private readonly EventoRepository _repo = new EventoRepository();
        private int _idSelecionado = -1;

        public FormEventos()
        {
            InitializeComponent();
        }

        // ── Quando o formulário abre ──────────────────────────────────
        private void FormEventos_Load(object sender, EventArgs e)
        {
            CarregarTipos();
            CarregarGrelha();
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
        }

        // ── Preenche o ComboBox com os tipos de evento ────────────────
        private void CarregarTipos()
        {
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("Reunião");
            cmbTipo.Items.Add("Manutenção");
            cmbTipo.Items.Add("Prazo de Pagamento");
            cmbTipo.SelectedIndex = 0;
        }

        // ── Converte o índice do ComboBox para o enum ─────────────────
        private TipoEvento ObterTipoSelecionado()
        {
            switch (cmbTipo.SelectedIndex)
            {
                case 1: return TipoEvento.Manutencao;
                case 2: return TipoEvento.PrazoPagamento;
                default: return TipoEvento.Reuniao;
            }
        }

        // ── Converte o enum para o índice do ComboBox ─────────────────
        private int TipoParaIndice(TipoEvento tipo)
        {
            switch (tipo)
            {
                case TipoEvento.Manutencao: return 1;
                case TipoEvento.PrazoPagamento: return 2;
                default: return 0;
            }
        }

        // ── Carrega os eventos na grelha ──────────────────────────────
        private void CarregarGrelha()
        {
            var lista = _repo.GetAll();

            dgvEventos.Rows.Clear();
            dgvEventos.Columns.Clear();

            dgvEventos.Columns.Add("colId", "ID");
            dgvEventos.Columns.Add("colTitulo", "Título");
            dgvEventos.Columns.Add("colTipo", "Tipo");
            dgvEventos.Columns.Add("colData", "Data");
            dgvEventos.Columns.Add("colDescricao", "Descrição");

            dgvEventos.Columns["colId"].Width = 40;
            dgvEventos.Columns["colTitulo"].Width = 180;
            dgvEventos.Columns["colTipo"].Width = 130;
            dgvEventos.Columns["colData"].Width = 120;
            dgvEventos.Columns["colDescricao"].Width = 200;

            foreach (var ev in lista)
            {
                string tipoTexto;
                switch (ev.Tipo)
                {
                    case TipoEvento.Manutencao: tipoTexto = "Manutenção"; break;
                    case TipoEvento.PrazoPagamento: tipoTexto = "Prazo Pagamento"; break;
                    default: tipoTexto = "Reunião"; break;
                }

                dgvEventos.Rows.Add(
                    ev.IdEvento,
                    ev.Titulo,
                    tipoTexto,
                    ev.Data.ToString("dd/MM/yyyy HH:mm"),
                    ev.Descricao
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
                    var novo = new Evento
                    {
                        Titulo = txtTitulo.Text.Trim(),
                        Tipo = ObterTipoSelecionado(),
                        Data = dtpData.Value,
                        Descricao = txtDescricao.Text.Trim()
                    };
                    _repo.Insert(novo);
                    MessageBox.Show("Evento registado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var atualizado = new Evento
                    {
                        IdEvento = _idSelecionado,
                        Titulo = txtTitulo.Text.Trim(),
                        Tipo = ObterTipoSelecionado(),
                        Data = dtpData.Value,
                        Descricao = txtDescricao.Text.Trim()
                    };
                    _repo.Update(atualizado);
                    MessageBox.Show("Evento atualizado com sucesso!", "Sucesso",
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
            if (dgvEventos.CurrentRow == null) return;

            _idSelecionado = Convert.ToInt32(dgvEventos.CurrentRow.Cells["colId"].Value);
            txtTitulo.Text = dgvEventos.CurrentRow.Cells["colTitulo"].Value.ToString();
            txtDescricao.Text = dgvEventos.CurrentRow.Cells["colDescricao"].Value.ToString();

            // Recupera o tipo a partir do texto na grelha
            var tipoTexto = dgvEventos.CurrentRow.Cells["colTipo"].Value.ToString();
            switch (tipoTexto)
            {
                case "Manutenção": cmbTipo.SelectedIndex = 1; break;
                case "Prazo Pagamento": cmbTipo.SelectedIndex = 2; break;
                default: cmbTipo.SelectedIndex = 0; break;
            }

            // Recupera a data
            var dataTexto = dgvEventos.CurrentRow.Cells["colData"].Value.ToString();
            if (DateTime.TryParseExact(dataTexto, "dd/MM/yyyy HH:mm",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime data))
            {
                dtpData.Value = data;
            }

            btnGuardar.Text = "Atualizar";
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
        }

        // ── Botão Apagar ──────────────────────────────────────────────
        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dgvEventos.CurrentRow == null) return;

            var id = Convert.ToInt32(dgvEventos.CurrentRow.Cells["colId"].Value);
            var titulo = dgvEventos.CurrentRow.Cells["colTitulo"].Value.ToString();

            var confirmacao = MessageBox.Show(
                $"Tens a certeza que queres apagar o evento '{titulo}'?\n\nEsta ação não pode ser desfeita.",
                "Confirmar eliminação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    _repo.Delete(id);
                    MessageBox.Show("Evento eliminado com sucesso!", "Sucesso",
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
        private void dgvEventos_SelectionChanged(object sender, EventArgs e)
        {
            bool temSelecao = dgvEventos.CurrentRow != null;
            btnEditar.Enabled = temSelecao;
            btnApagar.Enabled = temSelecao;
        }

        // ── Métodos auxiliares ────────────────────────────────────────
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("O campo Título é obrigatório.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitulo.Focus();
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            txtTitulo.Clear();
            txtDescricao.Clear();
            cmbTipo.SelectedIndex = 0;
            dtpData.Value = DateTime.Now;
            _idSelecionado = -1;
            btnGuardar.Text = "Guardar";
            btnEditar.Enabled = false;
            btnApagar.Enabled = false;
            dgvEventos.ClearSelection();
        }
    }
}