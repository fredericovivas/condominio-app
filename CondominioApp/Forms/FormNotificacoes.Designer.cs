namespace CondominioApp.Forms
{
    partial class FormNotificacoes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCondomino = new System.Windows.Forms.Label();
            this.lblMensagem = new System.Windows.Forms.Label();
            this.cmbCondomino = new System.Windows.Forms.ComboBox();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.dgvNotificacoes = new System.Windows.Forms.DataGridView();
            this.btnApagar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotificacoes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCondomino
            // 
            this.lblCondomino.AutoSize = true;
            this.lblCondomino.Location = new System.Drawing.Point(20, 20);
            this.lblCondomino.Name = "lblCondomino";
            this.lblCondomino.Size = new System.Drawing.Size(79, 16);
            this.lblCondomino.TabIndex = 0;
            this.lblCondomino.Text = "Condómino:";
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Location = new System.Drawing.Point(20, 60);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(78, 16);
            this.lblMensagem.TabIndex = 1;
            this.lblMensagem.Text = "Mensagem:";
            // 
            // cmbCondomino
            // 
            this.cmbCondomino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondomino.FormattingEnabled = true;
            this.cmbCondomino.Location = new System.Drawing.Point(110, 17);
            this.cmbCondomino.Name = "cmbCondomino";
            this.cmbCondomino.Size = new System.Drawing.Size(250, 24);
            this.cmbCondomino.TabIndex = 2;
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(110, 57);
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensagem.Size = new System.Drawing.Size(250, 60);
            this.txtMensagem.TabIndex = 3;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(20, 135);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 30);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Registar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(220, 135);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(90, 30);
            this.btnLimpar.TabIndex = 6;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // dgvNotificacoes
            // 
            this.dgvNotificacoes.AllowUserToAddRows = false;
            this.dgvNotificacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNotificacoes.Location = new System.Drawing.Point(20, 180);
            this.dgvNotificacoes.MultiSelect = false;
            this.dgvNotificacoes.Name = "dgvNotificacoes";
            this.dgvNotificacoes.ReadOnly = true;
            this.dgvNotificacoes.RowHeadersWidth = 51;
            this.dgvNotificacoes.RowTemplate.Height = 24;
            this.dgvNotificacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNotificacoes.Size = new System.Drawing.Size(690, 270);
            this.dgvNotificacoes.TabIndex = 7;
            this.dgvNotificacoes.SelectionChanged += new System.EventHandler(this.dgvNotificacoes_SelectionChanged);
            // 
            // btnApagar
            // 
            this.btnApagar.Location = new System.Drawing.Point(120, 135);
            this.btnApagar.Name = "btnApagar";
            this.btnApagar.Size = new System.Drawing.Size(90, 30);
            this.btnApagar.TabIndex = 8;
            this.btnApagar.Text = "Apagar";
            this.btnApagar.UseVisualStyleBackColor = true;
            this.btnApagar.Click += new System.EventHandler(this.btnApagar_Click);
            // 
            // FormNotificacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 453);
            this.Controls.Add(this.btnApagar);
            this.Controls.Add(this.dgvNotificacoes);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtMensagem);
            this.Controls.Add(this.cmbCondomino);
            this.Controls.Add(this.lblMensagem);
            this.Controls.Add(this.lblCondomino);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormNotificacoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestão de Notificações";
            this.Load += new System.EventHandler(this.FormNotificacoes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotificacoes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCondomino;
        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.ComboBox cmbCondomino;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.DataGridView dgvNotificacoes;
        private System.Windows.Forms.Button btnApagar;
    }
}