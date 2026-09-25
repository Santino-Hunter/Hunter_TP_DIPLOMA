namespace GUI
{
    partial class FRMBitacora
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
            this.LBBitacora = new System.Windows.Forms.Label();
            this.DGVBitacora = new System.Windows.Forms.DataGridView();
            this.lbNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lbApellido = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbFechaHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lbFechaDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cbCriticidad = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbModulo = new System.Windows.Forms.ComboBox();
            this.cbUsuarios = new System.Windows.Forms.ComboBox();
            this.lbModulo = new System.Windows.Forms.Label();
            this.lbLogin = new System.Windows.Forms.Label();
            this.BTNFiltrar = new System.Windows.Forms.Button();
            this.BTNLimpiar = new System.Windows.Forms.Button();
            this.BTNReporte = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVBitacora)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LBBitacora
            // 
            this.LBBitacora.AutoSize = true;
            this.LBBitacora.BackColor = System.Drawing.Color.PeachPuff;
            this.LBBitacora.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBBitacora.Location = new System.Drawing.Point(12, 9);
            this.LBBitacora.Name = "LBBitacora";
            this.LBBitacora.Size = new System.Drawing.Size(233, 24);
            this.LBBitacora.TabIndex = 0;
            this.LBBitacora.Text = "BITÁCORA DE EVENTOS";
            // 
            // DGVBitacora
            // 
            this.DGVBitacora.AllowUserToAddRows = false;
            this.DGVBitacora.AllowUserToDeleteRows = false;
            this.DGVBitacora.AllowUserToResizeColumns = false;
            this.DGVBitacora.AllowUserToResizeRows = false;
            this.DGVBitacora.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVBitacora.Location = new System.Drawing.Point(16, 36);
            this.DGVBitacora.MultiSelect = false;
            this.DGVBitacora.Name = "DGVBitacora";
            this.DGVBitacora.ReadOnly = true;
            this.DGVBitacora.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVBitacora.Size = new System.Drawing.Size(656, 294);
            this.DGVBitacora.TabIndex = 1;
            this.DGVBitacora.TabStop = false;
            this.DGVBitacora.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVBitacora_CellClick);
            // 
            // lbNombre
            // 
            this.lbNombre.AutoSize = true;
            this.lbNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombre.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbNombre.Location = new System.Drawing.Point(12, 336);
            this.lbNombre.Name = "lbNombre";
            this.lbNombre.Size = new System.Drawing.Size(65, 20);
            this.lbNombre.TabIndex = 2;
            this.lbNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(104, 336);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(125, 20);
            this.txtNombre.TabIndex = 3;
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(325, 336);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.ReadOnly = true;
            this.txtApellido.Size = new System.Drawing.Size(125, 20);
            this.txtApellido.TabIndex = 5;
            // 
            // lbApellido
            // 
            this.lbApellido.AutoSize = true;
            this.lbApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbApellido.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbApellido.Location = new System.Drawing.Point(236, 336);
            this.lbApellido.Name = "lbApellido";
            this.lbApellido.Size = new System.Drawing.Size(65, 20);
            this.lbApellido.TabIndex = 4;
            this.lbApellido.Text = "Apellido";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbFechaHasta);
            this.groupBox1.Controls.Add(this.dtpHasta);
            this.groupBox1.Controls.Add(this.lbFechaDesde);
            this.groupBox1.Controls.Add(this.dtpDesde);
            this.groupBox1.Controls.Add(this.cbCriticidad);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbModulo);
            this.groupBox1.Controls.Add(this.cbUsuarios);
            this.groupBox1.Controls.Add(this.lbModulo);
            this.groupBox1.Controls.Add(this.lbLogin);
            this.groupBox1.Location = new System.Drawing.Point(16, 362);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 118);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // lbFechaHasta
            // 
            this.lbFechaHasta.AutoSize = true;
            this.lbFechaHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFechaHasta.Location = new System.Drawing.Point(220, 62);
            this.lbFechaHasta.Name = "lbFechaHasta";
            this.lbFechaHasta.Size = new System.Drawing.Size(105, 20);
            this.lbFechaHasta.TabIndex = 17;
            this.lbFechaHasta.Text = "Fecha Hasta:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(224, 85);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(200, 20);
            this.dtpHasta.TabIndex = 16;
            // 
            // lbFechaDesde
            // 
            this.lbFechaDesde.AutoSize = true;
            this.lbFechaDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFechaDesde.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbFechaDesde.Location = new System.Drawing.Point(6, 62);
            this.lbFechaDesde.Name = "lbFechaDesde";
            this.lbFechaDesde.Size = new System.Drawing.Size(109, 20);
            this.lbFechaDesde.TabIndex = 15;
            this.lbFechaDesde.Text = "Fecha Desde:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(10, 85);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(200, 20);
            this.dtpDesde.TabIndex = 14;
            // 
            // cbCriticidad
            // 
            this.cbCriticidad.FormattingEnabled = true;
            this.cbCriticidad.Location = new System.Drawing.Point(291, 38);
            this.cbCriticidad.Name = "cbCriticidad";
            this.cbCriticidad.Size = new System.Drawing.Size(121, 21);
            this.cbCriticidad.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(287, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Criticidad";
            // 
            // cbModulo
            // 
            this.cbModulo.FormattingEnabled = true;
            this.cbModulo.Location = new System.Drawing.Point(151, 38);
            this.cbModulo.Name = "cbModulo";
            this.cbModulo.Size = new System.Drawing.Size(121, 21);
            this.cbModulo.TabIndex = 11;
            // 
            // cbUsuarios
            // 
            this.cbUsuarios.FormattingEnabled = true;
            this.cbUsuarios.Location = new System.Drawing.Point(10, 38);
            this.cbUsuarios.Name = "cbUsuarios";
            this.cbUsuarios.Size = new System.Drawing.Size(121, 21);
            this.cbUsuarios.TabIndex = 10;
            // 
            // lbModulo
            // 
            this.lbModulo.AutoSize = true;
            this.lbModulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbModulo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbModulo.Location = new System.Drawing.Point(147, 15);
            this.lbModulo.Name = "lbModulo";
            this.lbModulo.Size = new System.Drawing.Size(61, 20);
            this.lbModulo.TabIndex = 9;
            this.lbModulo.Text = "Módulo";
            // 
            // lbLogin
            // 
            this.lbLogin.AutoSize = true;
            this.lbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogin.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbLogin.Location = new System.Drawing.Point(6, 15);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(107, 20);
            this.lbLogin.TabIndex = 7;
            this.lbLogin.Text = "Usuario/Login";
            // 
            // BTNFiltrar
            // 
            this.BTNFiltrar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.BTNFiltrar.Location = new System.Drawing.Point(463, 336);
            this.BTNFiltrar.Name = "BTNFiltrar";
            this.BTNFiltrar.Size = new System.Drawing.Size(83, 61);
            this.BTNFiltrar.TabIndex = 7;
            this.BTNFiltrar.Text = "FILTRAR";
            this.BTNFiltrar.UseVisualStyleBackColor = true;
            this.BTNFiltrar.Click += new System.EventHandler(this.BTNFiltrar_Click);
            // 
            // BTNLimpiar
            // 
            this.BTNLimpiar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.BTNLimpiar.Location = new System.Drawing.Point(589, 336);
            this.BTNLimpiar.Name = "BTNLimpiar";
            this.BTNLimpiar.Size = new System.Drawing.Size(83, 61);
            this.BTNLimpiar.TabIndex = 8;
            this.BTNLimpiar.Text = "LIMPIAR";
            this.BTNLimpiar.UseVisualStyleBackColor = true;
            this.BTNLimpiar.Click += new System.EventHandler(this.BTNLimpiar_Click);
            // 
            // BTNReporte
            // 
            this.BTNReporte.ForeColor = System.Drawing.SystemColors.WindowText;
            this.BTNReporte.Location = new System.Drawing.Point(463, 424);
            this.BTNReporte.Name = "BTNReporte";
            this.BTNReporte.Size = new System.Drawing.Size(83, 56);
            this.BTNReporte.TabIndex = 9;
            this.BTNReporte.Text = "REPORTE";
            this.BTNReporte.UseVisualStyleBackColor = true;
            this.BTNReporte.Click += new System.EventHandler(this.BTNReporte_Click);
            // 
            // BTNSalir
            // 
            this.BTNSalir.ForeColor = System.Drawing.SystemColors.WindowText;
            this.BTNSalir.Location = new System.Drawing.Point(589, 445);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(83, 35);
            this.BTNSalir.TabIndex = 10;
            this.BTNSalir.Text = "SALIR";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // FRMBitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(684, 495);
            this.ControlBox = false;
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNReporte);
            this.Controls.Add(this.BTNLimpiar);
            this.Controls.Add(this.BTNFiltrar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lbApellido);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lbNombre);
            this.Controls.Add(this.DGVBitacora);
            this.Controls.Add(this.LBBitacora);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMBitacora";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRMBitacora";
            this.Load += new System.EventHandler(this.FRMBitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVBitacora)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBBitacora;
        private System.Windows.Forms.DataGridView DGVBitacora;
        private System.Windows.Forms.Label lbNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lbApellido;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbUsuarios;
        private System.Windows.Forms.Label lbModulo;
        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.ComboBox cbModulo;
        private System.Windows.Forms.Label lbFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lbFechaDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.ComboBox cbCriticidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTNFiltrar;
        private System.Windows.Forms.Button BTNLimpiar;
        private System.Windows.Forms.Button BTNReporte;
        private System.Windows.Forms.Button BTNSalir;
    }
}