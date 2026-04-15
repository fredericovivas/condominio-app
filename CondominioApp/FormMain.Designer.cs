namespace CondominioApp
{
    partial class FormMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnApartamentos = new System.Windows.Forms.Button();
            this.btnCondominos = new System.Windows.Forms.Button();
            this.btnEventos = new System.Windows.Forms.Button();
            this.btnPagamentos = new System.Windows.Forms.Button();
            this.btnIntervencoes = new System.Windows.Forms.Button();
            this.btnNotificacoes = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnApartamentos
            // 
            this.btnApartamentos.Location = new System.Drawing.Point(60, 80);
            this.btnApartamentos.Name = "btnApartamentos";
            this.btnApartamentos.Size = new System.Drawing.Size(180, 60);
            this.btnApartamentos.TabIndex = 0;
            this.btnApartamentos.Text = "Apartamentos";
            this.btnApartamentos.UseVisualStyleBackColor = true;
            this.btnApartamentos.Click += new System.EventHandler(this.btnApartamentos_Click);
            // 
            // btnCondominos
            // 
            this.btnCondominos.Location = new System.Drawing.Point(60, 180);
            this.btnCondominos.Name = "btnCondominos";
            this.btnCondominos.Size = new System.Drawing.Size(180, 60);
            this.btnCondominos.TabIndex = 1;
            this.btnCondominos.Text = "Condóminos";
            this.btnCondominos.UseVisualStyleBackColor = true;
            this.btnCondominos.Click += new System.EventHandler(this.btnCondominos_Click);
            // 
            // btnEventos
            // 
            this.btnEventos.Location = new System.Drawing.Point(60, 280);
            this.btnEventos.Name = "btnEventos";
            this.btnEventos.Size = new System.Drawing.Size(180, 60);
            this.btnEventos.TabIndex = 2;
            this.btnEventos.Text = "Eventos";
            this.btnEventos.UseVisualStyleBackColor = true;
            this.btnEventos.Click += new System.EventHandler(this.btnEventos_Click);
            // 
            // btnPagamentos
            // 
            this.btnPagamentos.Location = new System.Drawing.Point(60, 380);
            this.btnPagamentos.Name = "btnPagamentos";
            this.btnPagamentos.Size = new System.Drawing.Size(180, 60);
            this.btnPagamentos.TabIndex = 3;
            this.btnPagamentos.Text = "Pagamentos";
            this.btnPagamentos.UseVisualStyleBackColor = true;
            this.btnPagamentos.Click += new System.EventHandler(this.btnPagamentos_Click);
            // 
            // btnIntervencoes
            // 
            this.btnIntervencoes.Location = new System.Drawing.Point(60, 480);
            this.btnIntervencoes.Name = "btnIntervencoes";
            this.btnIntervencoes.Size = new System.Drawing.Size(180, 60);
            this.btnIntervencoes.TabIndex = 4;
            this.btnIntervencoes.Text = "Intervenções";
            this.btnIntervencoes.UseVisualStyleBackColor = true;
            this.btnIntervencoes.Click += new System.EventHandler(this.btnIntervencoes_Click);
            // 
            // btnNotificacoes
            // 
            this.btnNotificacoes.Location = new System.Drawing.Point(300, 80);
            this.btnNotificacoes.Name = "btnNotificacoes";
            this.btnNotificacoes.Size = new System.Drawing.Size(180, 60);
            this.btnNotificacoes.TabIndex = 5;
            this.btnNotificacoes.Text = "Notificações";
            this.btnNotificacoes.UseVisualStyleBackColor = true;
            this.btnNotificacoes.Click += new System.EventHandler(this.btnNotificacoes_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Black;
            this.lblTitulo.Location = new System.Drawing.Point(300, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(339, 36);
            this.lblTitulo.TabIndex = 6;
            this.lblTitulo.Text = "Gestão de Condomínio";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnNotificacoes);
            this.Controls.Add(this.btnIntervencoes);
            this.Controls.Add(this.btnPagamentos);
            this.Controls.Add(this.btnEventos);
            this.Controls.Add(this.btnCondominos);
            this.Controls.Add(this.btnApartamentos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Gestão de Condomínio";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApartamentos;
        private System.Windows.Forms.Button btnCondominos;
        private System.Windows.Forms.Button btnEventos;
        private System.Windows.Forms.Button btnPagamentos;
        private System.Windows.Forms.Button btnIntervencoes;
        private System.Windows.Forms.Button btnNotificacoes;
        private System.Windows.Forms.Label lblTitulo;
    }
}

