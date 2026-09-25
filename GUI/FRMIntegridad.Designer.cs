namespace GUI
{
    partial class FRMIntegridad
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
            this.LBTitulo = new System.Windows.Forms.Label();
            this.LBDescripcion = new System.Windows.Forms.Label();
            this.TXTDetalle = new System.Windows.Forms.TextBox();
            this.BTNRecalcular = new System.Windows.Forms.Button();
            this.BTNRestore = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LBTitulo
            // 
            this.LBTitulo.AutoSize = true;
            this.LBTitulo.BackColor = System.Drawing.SystemColors.Control;
            this.LBTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBTitulo.Location = new System.Drawing.Point(15, 15);
            this.LBTitulo.Name = "LBTitulo";
            this.LBTitulo.Size = new System.Drawing.Size(561, 25);
            this.LBTitulo.TabIndex = 0;
            this.LBTitulo.Text = "Detección de inconsistencias en los Dígitos Verificadores";
            // 
            // LBDescripcion
            // 
            this.LBDescripcion.AutoSize = true;
            this.LBDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBDescripcion.Location = new System.Drawing.Point(15, 45);
            this.LBDescripcion.Name = "LBDescripcion";
            this.LBDescripcion.Size = new System.Drawing.Size(472, 25);
            this.LBDescripcion.TabIndex = 1;
            this.LBDescripcion.Text = "Si encuentra una inconsistencia, debe repararla aquí!";
            // 
            // TXTDetalle
            // 
            this.TXTDetalle.Location = new System.Drawing.Point(15, 80);
            this.TXTDetalle.Multiline = true;
            this.TXTDetalle.Name = "TXTDetalle";
            this.TXTDetalle.ReadOnly = true;
            this.TXTDetalle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TXTDetalle.Size = new System.Drawing.Size(690, 250);
            this.TXTDetalle.TabIndex = 2;
            // 
            // BTNRecalcular
            // 
            this.BTNRecalcular.Location = new System.Drawing.Point(15, 350);
            this.BTNRecalcular.Name = "BTNRecalcular";
            this.BTNRecalcular.Size = new System.Drawing.Size(150, 35);
            this.BTNRecalcular.TabIndex = 3;
            this.BTNRecalcular.Text = "Recalcular DV";
            this.BTNRecalcular.UseVisualStyleBackColor = true;
            this.BTNRecalcular.Click += new System.EventHandler(this.BTNRecalcular_Click);
            // 
            // BTNRestore
            // 
            this.BTNRestore.Location = new System.Drawing.Point(180, 350);
            this.BTNRestore.Name = "BTNRestore";
            this.BTNRestore.Size = new System.Drawing.Size(150, 35);
            this.BTNRestore.TabIndex = 4;
            this.BTNRestore.Text = "Restore BD";
            this.BTNRestore.UseVisualStyleBackColor = true;
            this.BTNRestore.Click += new System.EventHandler(this.BTNRestore_Click);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(555, 350);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(150, 35);
            this.BTNSalir.TabIndex = 5;
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // FRMIntegridad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 420);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNRestore);
            this.Controls.Add(this.BTNRecalcular);
            this.Controls.Add(this.TXTDetalle);
            this.Controls.Add(this.LBDescripcion);
            this.Controls.Add(this.LBTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRMIntegridad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inconsistencia de Base de Datos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBTitulo;
        private System.Windows.Forms.Label LBDescripcion;
        private System.Windows.Forms.TextBox TXTDetalle;
        private System.Windows.Forms.Button BTNRecalcular;
        private System.Windows.Forms.Button BTNRestore;
        private System.Windows.Forms.Button BTNSalir;
    }
}