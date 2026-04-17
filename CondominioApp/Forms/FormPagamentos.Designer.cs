namespace CondominioApp.Forms
{
    partial class FormPagamentos
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
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblValor = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cmbCondomino = new System.Windows.Forms.ComboBox();
            this.cmbEvento = new System.Windows.Forms.ComboBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnMarcarPago = new System.Windows.Forms.Button();
            this.btnApagar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnDevedores = new System.Windows.Forms.Button();
            this.dgvPagamentos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagamentos)).BeginInit();
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
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Location = new System.Drawing.Point(20, 60);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(52, 16);
            this.lblEvento.TabIndex = 1;
            this.lblEvento.Text = "Evento:";
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(20, 100);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(60, 16);
            this.lblValor.TabIndex = 2;
            this.lblValor.Text = "Valor (€):";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(20, 140);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(53, 16);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Estado:";
            // 
            // cmbCondomino
            // 
            this.cmbCondomino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondomino.FormattingEnabled = true;
            this.cmbCondomino.Location = new System.Drawing.Point(110, 17);
            this.cmbCondomino.Name = "cmbCondomino";
            this.cmbCondomino.Size = new System.Drawing.Size(220, 24);
            this.cmbCondomino.TabIndex = 4;
            // 
            // cmbEvento
            // 
            this.cmbEvento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEvento.FormattingEnabled = true;
            this.cmbEvento.Location = new System.Drawing.Point(110, 57);
            this.cmbEvento.Name = "cmbEvento";
            this.cmbEvento.Size = new System.Drawing.Size(220, 24);
            this.cmbEvento.TabIndex = 5;
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(110, 137);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(150, 24);
            this.cmbEstado.TabIndex = 6;
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(110, 97);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(120, 22);
            this.txtValor.TabIndex = 7;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(20, 180);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 30);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnMarcarPago
            // 
            this.btnMarcarPago.Location = new System.Drawing.Point(120, 180);
            this.btnMarcarPago.Name = "btnMarcarPago";
            this.btnMarcarPago.Size = new System.Drawing.Size(110, 30);
            this.btnMarcarPago.TabIndex = 9;
            this.btnMarcarPago.Text = "Marcar Pago";
            this.btnMarcarPago.UseVisualStyleBackColor = true;
            this.btnMarcarPago.Click += new System.EventHandler(this.btnMarcarPago_Click);
            // 
            // btnApagar
            // 
            this.btnApagar.Location = new System.Drawing.Point(240, 180);
            this.btnApagar.Name = "btnApagar";
            this.btnApagar.Size = new System.Drawing.Size(90, 30);
            this.btnApagar.TabIndex = 10;
            this.btnApagar.Text = "Apagar";
            this.btnApagar.UseVisualStyleBackColor = true;
            this.btnApagar.Click += new System.EventHandler(this.btnApagar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(340, 180);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(90, 30);
            this.btnLimpar.TabIndex = 11;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnDevedores
            // 
            this.btnDevedores.Location = new System.Drawing.Point(440, 180);
            this.btnDevedores.Name = "btnDevedores";
            this.btnDevedores.Size = new System.Drawing.Size(120, 30);
            this.btnDevedores.TabIndex = 12;
            this.btnDevedores.Text = "Ver Devedores";
            this.btnDevedores.UseVisualStyleBackColor = true;
            this.btnDevedores.Click += new System.EventHandler(this.btnDevedores_Click);
            // 
            // dgvPagamentos
            // 
            this.dgvPagamentos.AllowUserToAddRows = false;
            this.dgvPagamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagamentos.Location = new System.Drawing.Point(20, 225);
            this.dgvPagamentos.MultiSelect = false;
            this.dgvPagamentos.Name = "dgvPagamentos";
            this.dgvPagamentos.ReadOnly = true;
            this.dgvPagamentos.RowHeadersWidth = 51;
            this.dgvPagamentos.RowTemplate.Height = 24;
            this.dgvPagamentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPagamentos.Size = new System.Drawing.Size(740, 270);
            this.dgvPagamentos.TabIndex = 13;
            this.dgvPagamentos.SelectionChanged += new System.EventHandler(this.dgvPagamentos_SelectionChanged);
            // 
            // FormPagamentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 503);
            this.Controls.Add(this.dgvPagamentos);
            this.Controls.Add(this.btnDevedores);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnApagar);
            this.Controls.Add(this.btnMarcarPago);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.cmbEvento);
            this.Controls.Add(this.cmbCondomino);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.lblEvento);
            this.Controls.Add(this.lblCondomino);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPagamentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestão de Pagamentos";
            this.Activated += new System.EventHandler(this.FormPagamentos_Activated_1);
            this.Load += new System.EventHandler(this.FormPagamentos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagamentos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCondomino;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cmbCondomino;
        private System.Windows.Forms.ComboBox cmbEvento;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnMarcarPago;
        private System.Windows.Forms.Button btnApagar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnDevedores;
        private System.Windows.Forms.DataGridView dgvPagamentos;
    }
}