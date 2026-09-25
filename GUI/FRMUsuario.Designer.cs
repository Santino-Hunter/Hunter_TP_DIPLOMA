namespace GUI
{
    partial class FRMUsuario
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
            this.DGVUsuarios = new System.Windows.Forms.DataGridView();
            this.RBActivos = new System.Windows.Forms.RadioButton();
            this.RBTodos = new System.Windows.Forms.RadioButton();
            this.LBDNI = new System.Windows.Forms.Label();
            this.BTNCrear = new System.Windows.Forms.Button();
            this.TXTDNI = new System.Windows.Forms.TextBox();
            this.BTNDesbloquear = new System.Windows.Forms.Button();
            this.BTNModificar = new System.Windows.Forms.Button();
            this.BTNActivarDesactivar = new System.Windows.Forms.Button();
            this.BTNAplicar = new System.Windows.Forms.Button();
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            this.TXTNombre = new System.Windows.Forms.TextBox();
            this.LBNombre = new System.Windows.Forms.Label();
            this.TXTApellido = new System.Windows.Forms.TextBox();
            this.LBApellido = new System.Windows.Forms.Label();
            this.CBRol = new System.Windows.Forms.ComboBox();
            this.LBRol = new System.Windows.Forms.Label();
            this.TXTMail = new System.Windows.Forms.TextBox();
            this.LBMail = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TXTMensaje = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVUsuarios
            // 
            this.DGVUsuarios.AllowUserToAddRows = false;
            this.DGVUsuarios.AllowUserToDeleteRows = false;
            this.DGVUsuarios.AllowUserToResizeColumns = false;
            this.DGVUsuarios.AllowUserToResizeRows = false;
            this.DGVUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVUsuarios.Location = new System.Drawing.Point(12, 35);
            this.DGVUsuarios.MultiSelect = false;
            this.DGVUsuarios.Name = "DGVUsuarios";
            this.DGVUsuarios.ReadOnly = true;
            this.DGVUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVUsuarios.Size = new System.Drawing.Size(643, 294);
            this.DGVUsuarios.TabIndex = 0;
            this.DGVUsuarios.TabStop = false;
            this.DGVUsuarios.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGVUsuarios_CellMouseClick);
            // 
            // RBActivos
            // 
            this.RBActivos.AutoSize = true;
            this.RBActivos.Location = new System.Drawing.Point(12, 12);
            this.RBActivos.Name = "RBActivos";
            this.RBActivos.Size = new System.Drawing.Size(71, 17);
            this.RBActivos.TabIndex = 0;
            this.RBActivos.Text = "ACTIVOS";
            this.RBActivos.UseVisualStyleBackColor = true;
            this.RBActivos.CheckedChanged += new System.EventHandler(this.RBActivos_CheckedChanged);
            // 
            // RBTodos
            // 
            this.RBTodos.AutoSize = true;
            this.RBTodos.Checked = true;
            this.RBTodos.Location = new System.Drawing.Point(89, 12);
            this.RBTodos.Name = "RBTodos";
            this.RBTodos.Size = new System.Drawing.Size(63, 17);
            this.RBTodos.TabIndex = 1;
            this.RBTodos.TabStop = true;
            this.RBTodos.Text = "TODOS";
            this.RBTodos.UseVisualStyleBackColor = true;
            this.RBTodos.CheckedChanged += new System.EventHandler(this.RBTodos_CheckedChanged);
            // 
            // LBDNI
            // 
            this.LBDNI.AutoSize = true;
            this.LBDNI.Location = new System.Drawing.Point(9, 343);
            this.LBDNI.Name = "LBDNI";
            this.LBDNI.Size = new System.Drawing.Size(26, 13);
            this.LBDNI.TabIndex = 3;
            this.LBDNI.Text = "DNI";
            // 
            // BTNCrear
            // 
            this.BTNCrear.Location = new System.Drawing.Point(671, 35);
            this.BTNCrear.Name = "BTNCrear";
            this.BTNCrear.Size = new System.Drawing.Size(112, 44);
            this.BTNCrear.TabIndex = 2;
            this.BTNCrear.Text = "CREAR";
            this.BTNCrear.UseVisualStyleBackColor = true;
            this.BTNCrear.Click += new System.EventHandler(this.BTNCrear_Click);
            // 
            // TXTDNI
            // 
            this.TXTDNI.Location = new System.Drawing.Point(89, 336);
            this.TXTDNI.Name = "TXTDNI";
            this.TXTDNI.Size = new System.Drawing.Size(121, 20);
            this.TXTDNI.TabIndex = 6;
            // 
            // BTNDesbloquear
            // 
            this.BTNDesbloquear.Location = new System.Drawing.Point(671, 85);
            this.BTNDesbloquear.Name = "BTNDesbloquear";
            this.BTNDesbloquear.Size = new System.Drawing.Size(112, 44);
            this.BTNDesbloquear.TabIndex = 3;
            this.BTNDesbloquear.Text = "DESBLOQUEAR";
            this.BTNDesbloquear.UseVisualStyleBackColor = true;
            this.BTNDesbloquear.Click += new System.EventHandler(this.BTNDesbloquear_Click);
            // 
            // BTNModificar
            // 
            this.BTNModificar.Location = new System.Drawing.Point(671, 135);
            this.BTNModificar.Name = "BTNModificar";
            this.BTNModificar.Size = new System.Drawing.Size(112, 44);
            this.BTNModificar.TabIndex = 4;
            this.BTNModificar.Text = "MODIFICAR";
            this.BTNModificar.UseVisualStyleBackColor = true;
            this.BTNModificar.Click += new System.EventHandler(this.BTNModificar_Click);
            // 
            // BTNActivarDesactivar
            // 
            this.BTNActivarDesactivar.Location = new System.Drawing.Point(671, 185);
            this.BTNActivarDesactivar.Name = "BTNActivarDesactivar";
            this.BTNActivarDesactivar.Size = new System.Drawing.Size(112, 44);
            this.BTNActivarDesactivar.TabIndex = 5;
            this.BTNActivarDesactivar.Text = "ACT./DES.";
            this.BTNActivarDesactivar.UseVisualStyleBackColor = true;
            this.BTNActivarDesactivar.Click += new System.EventHandler(this.BTNActivarDesactivar_Click);
            // 
            // BTNAplicar
            // 
            this.BTNAplicar.Location = new System.Drawing.Point(671, 235);
            this.BTNAplicar.Name = "BTNAplicar";
            this.BTNAplicar.Size = new System.Drawing.Size(112, 44);
            this.BTNAplicar.TabIndex = 11;
            this.BTNAplicar.Text = "APLICAR";
            this.BTNAplicar.UseVisualStyleBackColor = true;
            this.BTNAplicar.Click += new System.EventHandler(this.BTNAplicar_Click);
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(671, 285);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(112, 44);
            this.BTNCancelar.TabIndex = 12;
            this.BTNCancelar.Text = "CANCELAR";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(671, 335);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(112, 44);
            this.BTNSalir.TabIndex = 13;
            this.BTNSalir.Text = "SALIR";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // TXTNombre
            // 
            this.TXTNombre.Location = new System.Drawing.Point(89, 362);
            this.TXTNombre.Name = "TXTNombre";
            this.TXTNombre.Size = new System.Drawing.Size(121, 20);
            this.TXTNombre.TabIndex = 7;
            // 
            // LBNombre
            // 
            this.LBNombre.AutoSize = true;
            this.LBNombre.Location = new System.Drawing.Point(9, 369);
            this.LBNombre.Name = "LBNombre";
            this.LBNombre.Size = new System.Drawing.Size(44, 13);
            this.LBNombre.TabIndex = 12;
            this.LBNombre.Text = "Nombre";
            // 
            // TXTApellido
            // 
            this.TXTApellido.Location = new System.Drawing.Point(89, 388);
            this.TXTApellido.Name = "TXTApellido";
            this.TXTApellido.Size = new System.Drawing.Size(121, 20);
            this.TXTApellido.TabIndex = 8;
            // 
            // LBApellido
            // 
            this.LBApellido.AutoSize = true;
            this.LBApellido.Location = new System.Drawing.Point(9, 395);
            this.LBApellido.Name = "LBApellido";
            this.LBApellido.Size = new System.Drawing.Size(44, 13);
            this.LBApellido.TabIndex = 14;
            this.LBApellido.Text = "Apellido";
            // 
            // CBRol
            // 
            this.CBRol.FormattingEnabled = true;
            this.CBRol.Location = new System.Drawing.Point(89, 414);
            this.CBRol.Name = "CBRol";
            this.CBRol.Size = new System.Drawing.Size(121, 21);
            this.CBRol.TabIndex = 9;
            // 
            // LBRol
            // 
            this.LBRol.AutoSize = true;
            this.LBRol.Location = new System.Drawing.Point(9, 422);
            this.LBRol.Name = "LBRol";
            this.LBRol.Size = new System.Drawing.Size(23, 13);
            this.LBRol.TabIndex = 17;
            this.LBRol.Text = "Rol";
            // 
            // TXTMail
            // 
            this.TXTMail.Location = new System.Drawing.Point(89, 441);
            this.TXTMail.Name = "TXTMail";
            this.TXTMail.Size = new System.Drawing.Size(121, 20);
            this.TXTMail.TabIndex = 10;
            // 
            // LBMail
            // 
            this.LBMail.AutoSize = true;
            this.LBMail.Location = new System.Drawing.Point(9, 448);
            this.LBMail.Name = "LBMail";
            this.LBMail.Size = new System.Drawing.Size(26, 13);
            this.LBMail.TabIndex = 18;
            this.LBMail.Text = "Mail";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(329, 395);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Mensaje";
            // 
            // TXTMensaje
            // 
            this.TXTMensaje.Location = new System.Drawing.Point(382, 392);
            this.TXTMensaje.Multiline = true;
            this.TXTMensaje.Name = "TXTMensaje";
            this.TXTMensaje.ReadOnly = true;
            this.TXTMensaje.Size = new System.Drawing.Size(173, 43);
            this.TXTMensaje.TabIndex = 21;
            this.TXTMensaje.TabStop = false;
            // 
            // FRMUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(795, 473);
            this.ControlBox = false;
            this.Controls.Add(this.TXTMensaje);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TXTMail);
            this.Controls.Add(this.LBMail);
            this.Controls.Add(this.LBRol);
            this.Controls.Add(this.CBRol);
            this.Controls.Add(this.TXTApellido);
            this.Controls.Add(this.LBApellido);
            this.Controls.Add(this.TXTNombre);
            this.Controls.Add(this.LBNombre);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.BTNAplicar);
            this.Controls.Add(this.BTNActivarDesactivar);
            this.Controls.Add(this.BTNModificar);
            this.Controls.Add(this.BTNDesbloquear);
            this.Controls.Add(this.TXTDNI);
            this.Controls.Add(this.BTNCrear);
            this.Controls.Add(this.LBDNI);
            this.Controls.Add(this.RBTodos);
            this.Controls.Add(this.RBActivos);
            this.Controls.Add(this.DGVUsuarios);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMUsuario";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRMUsuario";
            this.Load += new System.EventHandler(this.FRMUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVUsuarios;
        private System.Windows.Forms.RadioButton RBActivos;
        private System.Windows.Forms.RadioButton RBTodos;
        private System.Windows.Forms.Label LBDNI;
        private System.Windows.Forms.Button BTNCrear;
        private System.Windows.Forms.TextBox TXTDNI;
        private System.Windows.Forms.Button BTNDesbloquear;
        private System.Windows.Forms.Button BTNModificar;
        private System.Windows.Forms.Button BTNActivarDesactivar;
        private System.Windows.Forms.Button BTNAplicar;
        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.Button BTNSalir;
        private System.Windows.Forms.TextBox TXTNombre;
        private System.Windows.Forms.Label LBNombre;
        private System.Windows.Forms.TextBox TXTApellido;
        private System.Windows.Forms.Label LBApellido;
        private System.Windows.Forms.ComboBox CBRol;
        private System.Windows.Forms.Label LBRol;
        private System.Windows.Forms.TextBox TXTMail;
        private System.Windows.Forms.Label LBMail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TXTMensaje;
    }
}