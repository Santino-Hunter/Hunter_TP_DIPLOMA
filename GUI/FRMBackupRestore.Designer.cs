namespace GUI
{
    partial class FRMBackupRestore
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
            this.grpRestore = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.lblAdvertenciaRestore = new System.Windows.Forms.Label();
            this.btnExaminarRestore = new System.Windows.Forms.Button();
            this.txtArchivoBackup = new System.Windows.Forms.TextBox();
            this.lblArchivoBackup = new System.Windows.Forms.Label();
            this.grpBackup = new System.Windows.Forms.GroupBox();
            this.btnCancelarBackup = new System.Windows.Forms.Button();
            this.btnGenerarBackup = new System.Windows.Forms.Button();
            this.lblInformacionBackup = new System.Windows.Forms.Label();
            this.btnExaminarBackup = new System.Windows.Forms.Button();
            this.txtCarpetaDestino = new System.Windows.Forms.TextBox();
            this.lblCarpetaDestino = new System.Windows.Forms.Label();
            this.grpRestore.SuspendLayout();
            this.grpBackup.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpRestore
            // 
            this.grpRestore.Controls.Add(this.btnCancelar);
            this.grpRestore.Controls.Add(this.btnRestaurar);
            this.grpRestore.Controls.Add(this.lblAdvertenciaRestore);
            this.grpRestore.Controls.Add(this.btnExaminarRestore);
            this.grpRestore.Controls.Add(this.txtArchivoBackup);
            this.grpRestore.Controls.Add(this.lblArchivoBackup);
            this.grpRestore.Location = new System.Drawing.Point(20, 20);
            this.grpRestore.Name = "grpRestore";
            this.grpRestore.Size = new System.Drawing.Size(660, 230);
            this.grpRestore.TabIndex = 0;
            this.grpRestore.TabStop = false;
            this.grpRestore.Text = "Restaurar base de datos";
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(335, 170);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(140, 38);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(490, 170);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(140, 38);
            this.btnRestaurar.TabIndex = 4;
            this.btnRestaurar.Text = "Restaurar";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // lblAdvertenciaRestore
            // 
            this.lblAdvertenciaRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvertenciaRestore.ForeColor = System.Drawing.Color.DarkRed;
            this.lblAdvertenciaRestore.Location = new System.Drawing.Point(25, 110);
            this.lblAdvertenciaRestore.Name = "lblAdvertenciaRestore";
            this.lblAdvertenciaRestore.Size = new System.Drawing.Size(605, 33);
            this.lblAdvertenciaRestore.TabIndex = 3;
            this.lblAdvertenciaRestore.Text = "Advertencia: la restauración reemplazará los datos actuales y cerrará la sesión a" +
    "ctiva.\n";
            // 
            // btnExaminarRestore
            // 
            this.btnExaminarRestore.Location = new System.Drawing.Point(530, 58);
            this.btnExaminarRestore.Name = "btnExaminarRestore";
            this.btnExaminarRestore.Size = new System.Drawing.Size(100, 30);
            this.btnExaminarRestore.TabIndex = 2;
            this.btnExaminarRestore.Text = "Examinar";
            this.btnExaminarRestore.UseVisualStyleBackColor = true;
            this.btnExaminarRestore.Click += new System.EventHandler(this.btnExaminarRestore_Click);
            // 
            // txtArchivoBackup
            // 
            this.txtArchivoBackup.Location = new System.Drawing.Point(25, 60);
            this.txtArchivoBackup.Name = "txtArchivoBackup";
            this.txtArchivoBackup.ReadOnly = true;
            this.txtArchivoBackup.Size = new System.Drawing.Size(490, 20);
            this.txtArchivoBackup.TabIndex = 1;
            // 
            // lblArchivoBackup
            // 
            this.lblArchivoBackup.AutoSize = true;
            this.lblArchivoBackup.Location = new System.Drawing.Point(25, 35);
            this.lblArchivoBackup.Name = "lblArchivoBackup";
            this.lblArchivoBackup.Size = new System.Drawing.Size(97, 13);
            this.lblArchivoBackup.TabIndex = 0;
            this.lblArchivoBackup.Text = "Archivo de backup";
            // 
            // grpBackup
            // 
            this.grpBackup.Controls.Add(this.btnCancelarBackup);
            this.grpBackup.Controls.Add(this.btnGenerarBackup);
            this.grpBackup.Controls.Add(this.lblInformacionBackup);
            this.grpBackup.Controls.Add(this.btnExaminarBackup);
            this.grpBackup.Controls.Add(this.txtCarpetaDestino);
            this.grpBackup.Controls.Add(this.lblCarpetaDestino);
            this.grpBackup.Location = new System.Drawing.Point(20, 256);
            this.grpBackup.Name = "grpBackup";
            this.grpBackup.Size = new System.Drawing.Size(660, 230);
            this.grpBackup.TabIndex = 6;
            this.grpBackup.TabStop = false;
            this.grpBackup.Text = "Generar copia de seguridad";
            this.grpBackup.Visible = false;
            // 
            // btnCancelarBackup
            // 
            this.btnCancelarBackup.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelarBackup.Location = new System.Drawing.Point(335, 170);
            this.btnCancelarBackup.Name = "btnCancelarBackup";
            this.btnCancelarBackup.Size = new System.Drawing.Size(140, 38);
            this.btnCancelarBackup.TabIndex = 5;
            this.btnCancelarBackup.Text = "Cancelar";
            this.btnCancelarBackup.UseVisualStyleBackColor = true;
            // 
            // btnGenerarBackup
            // 
            this.btnGenerarBackup.Location = new System.Drawing.Point(490, 170);
            this.btnGenerarBackup.Name = "btnGenerarBackup";
            this.btnGenerarBackup.Size = new System.Drawing.Size(140, 38);
            this.btnGenerarBackup.TabIndex = 4;
            this.btnGenerarBackup.Text = "Generar Backup";
            this.btnGenerarBackup.UseVisualStyleBackColor = true;
            this.btnGenerarBackup.Click += new System.EventHandler(this.btnGenerarBackup_Click);
            // 
            // lblInformacionBackup
            // 
            this.lblInformacionBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformacionBackup.ForeColor = System.Drawing.Color.DarkRed;
            this.lblInformacionBackup.Location = new System.Drawing.Point(25, 110);
            this.lblInformacionBackup.Name = "lblInformacionBackup";
            this.lblInformacionBackup.Size = new System.Drawing.Size(605, 33);
            this.lblInformacionBackup.TabIndex = 3;
            this.lblInformacionBackup.Text = "El sistema generará una copia de seguridad de la base de datos.";
            // 
            // btnExaminarBackup
            // 
            this.btnExaminarBackup.Location = new System.Drawing.Point(530, 58);
            this.btnExaminarBackup.Name = "btnExaminarBackup";
            this.btnExaminarBackup.Size = new System.Drawing.Size(100, 30);
            this.btnExaminarBackup.TabIndex = 2;
            this.btnExaminarBackup.Text = "Examinar";
            this.btnExaminarBackup.UseVisualStyleBackColor = true;
            this.btnExaminarBackup.Click += new System.EventHandler(this.btnExaminarBackup_Click);
            // 
            // txtCarpetaDestino
            // 
            this.txtCarpetaDestino.Location = new System.Drawing.Point(25, 60);
            this.txtCarpetaDestino.Name = "txtCarpetaDestino";
            this.txtCarpetaDestino.ReadOnly = true;
            this.txtCarpetaDestino.Size = new System.Drawing.Size(490, 20);
            this.txtCarpetaDestino.TabIndex = 1;
            // 
            // lblCarpetaDestino
            // 
            this.lblCarpetaDestino.AutoSize = true;
            this.lblCarpetaDestino.Location = new System.Drawing.Point(25, 35);
            this.lblCarpetaDestino.Name = "lblCarpetaDestino";
            this.lblCarpetaDestino.Size = new System.Drawing.Size(96, 13);
            this.lblCarpetaDestino.TabIndex = 0;
            this.lblCarpetaDestino.Text = "Carpeta de destino";
            // 
            // FRMBackupRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 508);
            this.ControlBox = false;
            this.Controls.Add(this.grpBackup);
            this.Controls.Add(this.grpRestore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMBackupRestore";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restaurar base de datos";
            this.grpRestore.ResumeLayout(false);
            this.grpRestore.PerformLayout();
            this.grpBackup.ResumeLayout(false);
            this.grpBackup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpRestore;
        private System.Windows.Forms.Label lblAdvertenciaRestore;
        private System.Windows.Forms.Button btnExaminarRestore;
        private System.Windows.Forms.TextBox txtArchivoBackup;
        private System.Windows.Forms.Label lblArchivoBackup;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox grpBackup;
        private System.Windows.Forms.Button btnCancelarBackup;
        private System.Windows.Forms.Button btnGenerarBackup;
        private System.Windows.Forms.Label lblInformacionBackup;
        private System.Windows.Forms.Button btnExaminarBackup;
        private System.Windows.Forms.TextBox txtCarpetaDestino;
        private System.Windows.Forms.Label lblCarpetaDestino;
    }
}