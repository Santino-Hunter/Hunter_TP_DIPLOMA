namespace GUI
{
    partial class FRMMenu
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarContraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarIdiomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inglesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espanolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MaestroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mesasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rolesYPermisosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitácoraDeEventosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionOperativaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reservasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consumosDeMesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cobrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaYSoporteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualDeUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sesionToolStripMenuItem,
            this.MaestroToolStripMenuItem,
            this.administracionToolStripMenuItem,
            this.gestionOperativaToolStripMenuItem,
            this.ayudaYSoporteToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(914, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sesionToolStripMenuItem
            // 
            this.sesionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarSesiónToolStripMenuItem,
            this.cambiarContraToolStripMenuItem,
            this.cambiarIdiomaToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem});
            this.sesionToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sesionToolStripMenuItem.ForeColor = System.Drawing.Color.Silver;
            this.sesionToolStripMenuItem.Name = "sesionToolStripMenuItem";
            this.sesionToolStripMenuItem.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.sesionToolStripMenuItem.Size = new System.Drawing.Size(67, 31);
            this.sesionToolStripMenuItem.Text = "Sesión";
            // 
            // iniciarSesiónToolStripMenuItem
            // 
            this.iniciarSesiónToolStripMenuItem.Name = "iniciarSesiónToolStripMenuItem";
            this.iniciarSesiónToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.iniciarSesiónToolStripMenuItem.Text = "Iniciar Sesión";
            this.iniciarSesiónToolStripMenuItem.Click += new System.EventHandler(this.iniciarSesiónToolStripMenuItem_Click);
            // 
            // cambiarContraToolStripMenuItem
            // 
            this.cambiarContraToolStripMenuItem.Name = "cambiarContraToolStripMenuItem";
            this.cambiarContraToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.cambiarContraToolStripMenuItem.Text = "Cambiar Contraseña";
            this.cambiarContraToolStripMenuItem.Click += new System.EventHandler(this.cambiarContraToolStripMenuItem_Click);
            // 
            // cambiarIdiomaToolStripMenuItem
            // 
            this.cambiarIdiomaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inglesToolStripMenuItem,
            this.espanolToolStripMenuItem});
            this.cambiarIdiomaToolStripMenuItem.Name = "cambiarIdiomaToolStripMenuItem";
            this.cambiarIdiomaToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.cambiarIdiomaToolStripMenuItem.Text = "Cambiar Idioma";
            // 
            // inglesToolStripMenuItem
            // 
            this.inglesToolStripMenuItem.Name = "inglesToolStripMenuItem";
            this.inglesToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.inglesToolStripMenuItem.Text = "Inglés";
            this.inglesToolStripMenuItem.Click += new System.EventHandler(this.inglesToolStripMenuItem_Click);
            // 
            // espanolToolStripMenuItem
            // 
            this.espanolToolStripMenuItem.Name = "espanolToolStripMenuItem";
            this.espanolToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.espanolToolStripMenuItem.Text = "Español";
            this.espanolToolStripMenuItem.Click += new System.EventHandler(this.espanolToolStripMenuItem_Click);
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // MaestroToolStripMenuItem
            // 
            this.MaestroToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mesasToolStripMenuItem,
            this.productosToolStripMenuItem});
            this.MaestroToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaestroToolStripMenuItem.ForeColor = System.Drawing.Color.Silver;
            this.MaestroToolStripMenuItem.Name = "MaestroToolStripMenuItem";
            this.MaestroToolStripMenuItem.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.MaestroToolStripMenuItem.Size = new System.Drawing.Size(79, 31);
            this.MaestroToolStripMenuItem.Text = "Maestro";
            // 
            // mesasToolStripMenuItem
            // 
            this.mesasToolStripMenuItem.Name = "mesasToolStripMenuItem";
            this.mesasToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.mesasToolStripMenuItem.Text = "Mesas";
            // 
            // productosToolStripMenuItem
            // 
            this.productosToolStripMenuItem.Name = "productosToolStripMenuItem";
            this.productosToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.productosToolStripMenuItem.Text = "Productos";
            // 
            // administracionToolStripMenuItem
            // 
            this.administracionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionDeUsuariosToolStripMenuItem,
            this.rolesYPermisosToolStripMenuItem,
            this.bitácoraDeEventosToolStripMenuItem,
            this.restoreToolStripMenuItem,
            this.backupToolStripMenuItem});
            this.administracionToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.administracionToolStripMenuItem.ForeColor = System.Drawing.Color.Silver;
            this.administracionToolStripMenuItem.Name = "administracionToolStripMenuItem";
            this.administracionToolStripMenuItem.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.administracionToolStripMenuItem.Size = new System.Drawing.Size(124, 31);
            this.administracionToolStripMenuItem.Text = "Administración";
            // 
            // gestionDeUsuariosToolStripMenuItem
            // 
            this.gestionDeUsuariosToolStripMenuItem.Name = "gestionDeUsuariosToolStripMenuItem";
            this.gestionDeUsuariosToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.gestionDeUsuariosToolStripMenuItem.Text = "Gestión de Usuarios";
            this.gestionDeUsuariosToolStripMenuItem.Click += new System.EventHandler(this.gestionDeUsuariosToolStripMenuItem_Click);
            // 
            // rolesYPermisosToolStripMenuItem
            // 
            this.rolesYPermisosToolStripMenuItem.Name = "rolesYPermisosToolStripMenuItem";
            this.rolesYPermisosToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.rolesYPermisosToolStripMenuItem.Text = "Roles y Permisos";
            this.rolesYPermisosToolStripMenuItem.Click += new System.EventHandler(this.rolesYPermisosToolStripMenuItem_Click);
            // 
            // bitácoraDeEventosToolStripMenuItem
            // 
            this.bitácoraDeEventosToolStripMenuItem.Name = "bitácoraDeEventosToolStripMenuItem";
            this.bitácoraDeEventosToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.bitácoraDeEventosToolStripMenuItem.Text = "Bitácora de Eventos";
            this.bitácoraDeEventosToolStripMenuItem.Click += new System.EventHandler(this.bitácoraDeEventosToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // gestionOperativaToolStripMenuItem
            // 
            this.gestionOperativaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reservasToolStripMenuItem,
            this.consumosDeMesaToolStripMenuItem,
            this.cobrosToolStripMenuItem});
            this.gestionOperativaToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gestionOperativaToolStripMenuItem.ForeColor = System.Drawing.Color.Silver;
            this.gestionOperativaToolStripMenuItem.Name = "gestionOperativaToolStripMenuItem";
            this.gestionOperativaToolStripMenuItem.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.gestionOperativaToolStripMenuItem.Size = new System.Drawing.Size(147, 31);
            this.gestionOperativaToolStripMenuItem.Text = "Gestión Operativa";
            // 
            // reservasToolStripMenuItem
            // 
            this.reservasToolStripMenuItem.Name = "reservasToolStripMenuItem";
            this.reservasToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.reservasToolStripMenuItem.Text = "Reservas";
            this.reservasToolStripMenuItem.Click += new System.EventHandler(this.reservasToolStripMenuItem_Click);
            // 
            // consumosDeMesaToolStripMenuItem
            // 
            this.consumosDeMesaToolStripMenuItem.Name = "consumosDeMesaToolStripMenuItem";
            this.consumosDeMesaToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.consumosDeMesaToolStripMenuItem.Text = "Consumos de Mesa";
            // 
            // cobrosToolStripMenuItem
            // 
            this.cobrosToolStripMenuItem.Name = "cobrosToolStripMenuItem";
            this.cobrosToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.cobrosToolStripMenuItem.Text = "Cobros";
            // 
            // ayudaYSoporteToolStripMenuItem
            // 
            this.ayudaYSoporteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualDeUsuarioToolStripMenuItem});
            this.ayudaYSoporteToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayudaYSoporteToolStripMenuItem.ForeColor = System.Drawing.Color.Silver;
            this.ayudaYSoporteToolStripMenuItem.Name = "ayudaYSoporteToolStripMenuItem";
            this.ayudaYSoporteToolStripMenuItem.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.ayudaYSoporteToolStripMenuItem.Size = new System.Drawing.Size(133, 31);
            this.ayudaYSoporteToolStripMenuItem.Text = "Ayuda y Soporte";
            // 
            // manualDeUsuarioToolStripMenuItem
            // 
            this.manualDeUsuarioToolStripMenuItem.Name = "manualDeUsuarioToolStripMenuItem";
            this.manualDeUsuarioToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.manualDeUsuarioToolStripMenuItem.Text = "Manual de Usuario";
            // 
            // FRMMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(914, 741);
            this.ControlBox = false;
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FRMMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C&H - Menú Principal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gestionOperativaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reservasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionDeUsuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rolesYPermisosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaYSoporteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualDeUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consumosDeMesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitácoraDeEventosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MaestroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mesasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarContraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarIdiomaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inglesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espanolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cobrosToolStripMenuItem;
    }
}

