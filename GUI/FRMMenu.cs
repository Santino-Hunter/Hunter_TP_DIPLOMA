using SERVICES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using SERVICES.SERVICESCambioIdioma;

namespace GUI
{
    public partial class FRMMenu : Form, IIdiomaObservador
    {
        public FRMMenu()
        {
            InitializeComponent();

            ConfigurarPermisosMenu();

            GestorIdioma.Instancia.Suscribir(this);
            ActualizarIdioma();
            MarcarIdiomaActual();

            AplicarPermisosMenu();
        }

        private BLLBitacoraEvento bllBitacora = new BLLBitacoraEvento();
        private BLLIdioma bllIdioma = new BLLIdioma();

        
        private void ConfigurarPermisosMenu()
        {
            // Maestro
            mesasToolStripMenuItem.Tag = "MAE_MESAS";
            productosToolStripMenuItem.Tag = "MAE_PRODUCTOS";

            // Administración
            gestionDeUsuariosToolStripMenuItem.Tag = "ADM_GESTION_USUARIOS";
            rolesYPermisosToolStripMenuItem.Tag = "ADM_ROLES_PERMISOS";
            bitácoraDeEventosToolStripMenuItem.Tag = "ADM_BITACORA_EVENTOS";
            backupToolStripMenuItem.Tag = "ADM_BACKUP";
            restoreToolStripMenuItem.Tag = "ADM_RESTORE";

            // Gestión operativa
            reservasToolStripMenuItem.Tag = "OP_REGISTRO_PEDIDOS";
            consumosDeMesaToolStripMenuItem.Tag = "OP_FACTURACION_PAGOS";

        }

        private bool TienePermiso(string permiso)
        {
            if (!SERVICESSessionManager.IsLogged)
                return false;

            if (SERVICESSessionManager.ObtenerInstancia == null)
                return false;

            if (SERVICESSessionManager.ObtenerInstancia.Usuario == null)
                return false;

            if (SERVICESSessionManager.ObtenerInstancia.Usuario.Rol == null)
                return false;

            return SERVICESSessionManager.ObtenerInstancia.Usuario.Rol.TienePermiso(permiso);
        }

        private void AplicarPermisosMenu()
        {
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                ToolStripMenuItem menu = item as ToolStripMenuItem;

                if (menu != null)
                    AplicarPermisosRecursivo(menu);
            }
        }

        private bool AplicarPermisosRecursivo(ToolStripMenuItem item)
        {
            if (item == sesionToolStripMenuItem || item == ayudaYSoporteToolStripMenuItem)
            {
                HabilitarSubMenuCompleto(item);
                return true;
            }

            bool tieneHijoHabilitado = false;

            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                ToolStripMenuItem subMenu = subItem as ToolStripMenuItem;

                if (subMenu != null)
                {
                    bool hijoHabilitado = AplicarPermisosRecursivo(subMenu);

                    if (hijoHabilitado)
                        tieneHijoHabilitado = true;
                }
            }

            string permiso = item.Tag as string;

            if (!string.IsNullOrWhiteSpace(permiso))
            {
                item.Enabled = TienePermiso(permiso);
            }
            else if (item.DropDownItems.Count > 0)
            {
                item.Enabled = tieneHijoHabilitado;
            }
            else
            {
                item.Enabled = true;
            }

            return item.Enabled;
        }

        private void HabilitarSubMenuCompleto(ToolStripMenuItem item)
        {
            item.Enabled = true;

            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                ToolStripMenuItem subMenu = subItem as ToolStripMenuItem;

                if (subMenu != null)
                    HabilitarSubMenuCompleto(subMenu);
            }
        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FRMUsuario>().Any())
            {
                MensajeIdioma.Advertencia(
                    "FRMMenu.MsgVentanaAbierta.Text",
                    "Ya hay una ventana abierta.",
                    "FRMMenu.MsgAdvertencia.Titulo",
                    "Advertencia");
                
                return;
            }
            else
            {
                List<FRMLogin> openLoginForms = Application.OpenForms.OfType<FRMLogin>().ToList();
                if(openLoginForms.Count > 1) openLoginForms[1].Close();
            }
            Application.OpenForms.OfType<FRMBitacora>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMCambiarContraseña>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMPerfiles>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMReserva>().ToList().ForEach(form => form.Close());

            FRMUsuario frm = new FRMUsuario();
            
            frm.MdiParent = this;
            frm.Show();

        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MensajeIdioma.PreguntaSiNo("FRMMenu.MsgConfirmarCerrarSesion.Text","¿Está seguro que desea cerrar sesión?","FRMMenu.MsgConfirmarCerrarSesion.Titulo","Cerrar sesión") == DialogResult.Yes)
            {
                if (SERVICESSessionManager.IsLogged)
                {
                    try
                    {
                        string login = SERVICESSessionManager.ObtenerInstancia.Usuario.Login;

                        bllBitacora.RegistrarEvento(login, "Logout", $"Cerró sesión", 1);

                        SERVICESSessionManager.Logout();

                        MensajeIdioma.Info(
                            "FRMMenu.MsgLogoutOk.Text",
                            "Sesión cerrada correctamente.",
                            "FRMMenu.MsgLogoutOk.Titulo",
                            "Cerrar sesión");

                        GestorIdioma.Instancia.CambiarIdioma("es");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                                ex.Message,
                                MensajeIdioma.T("FRMMenu.MsgError.Titulo", "Error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }
                }

                List<FRMLogin> openLoginForms = Application.OpenForms.OfType<FRMLogin>().ToList();

                if (openLoginForms.Count > 0)
                {
                    openLoginForms[0].Show();
                }

                this.Close();
            }
        }

        private void cambiarContraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<FRMUsuario>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMBitacora>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMPerfiles>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMReserva>().ToList().ForEach(form => form.Close());

            FRMCambiarContraseña frm = new FRMCambiarContraseña();
            frm.MdiParent = this;
            frm.Show();
        }

        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FRMLogin>().Any(f => f.MdiParent == this))
            {
                MensajeIdioma.Advertencia(
                    "FRMMenu.MsgLoginAbierto.Text",
                    "Ya hay una ventana de login abierta.",
                    "FRMMenu.MsgAdvertencia.Titulo",
                    "Advertencia"); 
                return;
            }

            Application.OpenForms.OfType<FRMUsuario>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMBitacora>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMCambiarContraseña>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMPerfiles>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMReserva>().ToList().ForEach(form => form.Close());

            if (SERVICESSessionManager.IsLogged)
            {
                MensajeIdioma.Advertencia(
                    "FRMMenu.MsgSesionPrevia.Text",
                    "Debe cerrar la sesión previa para iniciar una nueva.",
                    "FRMMenu.MsgAdvertencia.Titulo",
                    "Advertencia");
            }
        }

        private void bitácoraDeEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FRMBitacora>().Any())
            {
                MensajeIdioma.Advertencia(
                    "FRMMenu.MsgBitacoraAbierta.Text",
                    "Ya hay una ventana de bitácora abierta.",
                    "FRMMenu.MsgAdvertencia.Titulo",
                    "Advertencia");
                return;
            }
            Application.OpenForms.OfType<FRMUsuario>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMCambiarContraseña>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMPerfiles>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMReserva>().ToList().ForEach(form => form.Close());

            FRMBitacora frm = new FRMBitacora();
            frm.MdiParent = this;
            frm.Show();
        }

        private void MarcarIdiomaActual()
        {
            inglesToolStripMenuItem.Checked = GestorIdioma.Instancia.CodigoActual == "en";
            espanolToolStripMenuItem.Checked = GestorIdioma.Instancia.CodigoActual == "es";
        }

        private void CambiarIdiomaDesdeMenu(string codigoIdioma)
        {
            try
            {
                bllIdioma.CambiarIdiomaUsuario(codigoIdioma);
                MarcarIdiomaActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    MensajeIdioma.T("FRMMenu.MsgIdiomas.Titulo", "Idiomas"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void espanolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarIdiomaDesdeMenu("es");
        }

        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarIdiomaDesdeMenu("en");
        }

        private void rolesYPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FRMPerfiles>().Any())
            {
                MensajeIdioma.Advertencia(
                    "FRMMenu.MsgPerfilesAbierta.Text",
                    "Ya hay una ventana de roles y permisos abierta.",
                    "FRMMenu.MsgAdvertencia.Titulo",
                    "Advertencia");
                return;
            }

            Application.OpenForms.OfType<FRMUsuario>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMBitacora>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMCambiarContraseña>().ToList().ForEach(form => form.Close());

            FRMPerfiles frm = new FRMPerfiles();
            frm.MdiParent = this;
            frm.Show();
        }

        public void ActualizarIdioma()
        {
            AplicadorIdioma.Aplicar(this);
            MarcarIdiomaActual();
            AplicarPermisosMenu();

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<FRMUsuario>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMBitacora>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMCambiarContraseña>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMPerfiles>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMReserva>().ToList().ForEach(form => form.Close());

            using (FRMBackupRestore frm = new FRMBackupRestore(FRMBackupRestore.Modo.Restore))
            {
                frm.ShowDialog();

                if (!frm.SeRestauro)
                    return;
            }

            if (SERVICESSessionManager.IsLogged)
                SERVICESSessionManager.Logout();

            GestorIdioma.Instancia.CambiarIdioma("es");

            FRMLogin frmLogin = Application.OpenForms
                .OfType<FRMLogin>()
                .FirstOrDefault();

            if (frmLogin != null)
            {
                frmLogin.Show();
                frmLogin.Activate();
            }

            Close();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<FRMUsuario>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMBitacora>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMCambiarContraseña>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMPerfiles>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMReserva>().ToList().ForEach(form => form.Close());

            using (FRMBackupRestore frm =
            new FRMBackupRestore(FRMBackupRestore.Modo.Backup))
            {
                frm.ShowDialog();
            }
        }
    
        private void reservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRMReserva abierta = Application.OpenForms.OfType<FRMReserva>().FirstOrDefault();

            if (abierta != null)
            {
                abierta.Activate();
                return;
            }

            Application.OpenForms.OfType<FRMUsuario>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMBitacora>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMCambiarContraseña>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMPerfiles>().ToList().ForEach(form => form.Close());
            Application.OpenForms.OfType<FRMReserva>().ToList().ForEach(form => form.Close());

            FRMReserva frm = new FRMReserva();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

    }
}
