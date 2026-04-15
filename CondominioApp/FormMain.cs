using System;
using System.Windows.Forms;

namespace CondominioApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Nada a carregar por agora
        }

        private void btnApartamentos_Click(object sender, EventArgs e)
        {
            var form = new Forms.FormApartamentos();
            form.ShowDialog();
        }

        private void btnCondominos_Click(object sender, EventArgs e)
        {
            var form = new Forms.FormCondominos();
            form.ShowDialog();
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            var form = new Forms.FormEventos();
            form.ShowDialog();
        }

        private void btnPagamentos_Click(object sender, EventArgs e)
        {
            var form = new Forms.FormPagamentos();
            form.ShowDialog();
        }

        private void btnIntervencoes_Click(object sender, EventArgs e)
        {
            var form = new Forms.FormIntervencoes();
            form.ShowDialog();
        }

        private void btnNotificacoes_Click(object sender, EventArgs e)
        {
            var form = new Forms.FormNotificacoes();
            form.ShowDialog();
        }
    }
}