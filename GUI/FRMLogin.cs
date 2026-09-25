using SERVICES;
using SERVICES.SERVICESCambioIdioma;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FRMLogin : Form
    {
        public FRMLogin()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MensajeIdioma.Advertencia(
                    "FRMLogin.MsgDatosIncompletos.Text",
                    "Debe ingresar un usuario y contraseña.",
                    "FRMLogin.MsgDatosIncompletos.Titulo",
                    "Datos incompletos");

                return;
            }

            try
            {
                BLL.BLLUsuario bllUsuario = new BLL.BLLUsuario();
                BLL.BLLDigitoVerificador bllDV =
                    new BLL.BLLDigitoVerificador();

                ResultadoIntegridad resultadoIntegridad =
                    bllDV.Verificar();

                if (!resultadoIntegridad.Correcto)
                {
                    SERVICESUsuario gerente =
                        bllUsuario.LoginModoReparacion(
                            txtUsuario.Text.Trim(),
                            txtContraseña.Text);

                    if (SERVICESSessionManager.IsLogged)
                    {
                        SERVICESSessionManager.Logout();
                    }

                    SERVICESSessionManager.Login(gerente);

                    MessageBox.Show(
                        "Se detectó una inconsistencia en la integridad " +
                        "de la base de datos.\n\n" +
                        "Será dirigido " +
                        "al módulo de reparación.",
                        "Inconsistencias detectadas!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtUsuario.Clear();
                    txtContraseña.Clear();

                    Hide();

                    DialogResult respuesta = DialogResult.Cancel;

                    try
                    {
                        using (FRMIntegridad frmIntegridad =
                               new FRMIntegridad(resultadoIntegridad))
                        {
                            respuesta = frmIntegridad.ShowDialog();
                        }
                    }
                    finally
                    {
                        if (SERVICESSessionManager.IsLogged)
                        {
                            SERVICESSessionManager.Logout();
                        }
                    }

                    if (respuesta == DialogResult.OK)
                    {
                        ResultadoIntegridad verificacionFinal =
                            bllDV.Verificar();

                        if (verificacionFinal.Correcto)
                        {
                            Show();
                            Activate();

                            txtUsuario.Clear();
                            txtContraseña.Clear();
                            txtUsuario.Focus();

                            MessageBox.Show(
                                "La integridad fue reparada correctamente.\n\n" +
                                "Debe iniciar sesión nuevamente.",
                                "Integridad reparada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            Show();
                            Activate();

                            txtUsuario.Clear();
                            txtContraseña.Clear();
                            txtUsuario.Focus();

                            MessageBox.Show(
                                "La operación terminó, pero la inconsistencia " +
                                "continúa.\n\n" +
                                "El acceso al sistema permanece bloqueado.",
                                "Integridad de datos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "La inconsistencia no fue reparada.\n\n" +
                            "El sistema se cerrará para proteger la información.",
                            "Integridad de datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        Close();
                    }

                    return;
                }
                SERVICESUsuario usuario =
                    bllUsuario.Login(
                        txtUsuario.Text.Trim(),
                        txtContraseña.Text);

                if (SERVICESSessionManager.IsLogged)
                {
                    SERVICESSessionManager.Logout();
                }

                string mensajeSesion =
                    SERVICESSessionManager.Login(usuario);

                MessageBox.Show(
                    mensajeSesion,
                    MensajeIdioma.T(
                        "FRMLogin.MsgLoginOk.Titulo",
                        "Inicio de sesión exitoso"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                string idiomaUsuario =
                    string.IsNullOrWhiteSpace(usuario.Idioma)
                        ? "es"
                        : usuario.Idioma;

                GestorIdioma.Instancia.CambiarIdioma(idiomaUsuario);

                txtUsuario.Clear();
                txtContraseña.Clear();

                if (MdiParent != null)
                {
                    Close();
                    return;
                }

                Hide();

                using (FRMMenu frmMenu = new FRMMenu())
                {
                    frmMenu.ShowDialog();
                }

                Show();
                Activate();
                txtUsuario.Focus();
            }
            catch (Exception ex)
            {
                if (!IsDisposed && !Visible)
                {
                    Show();
                    Activate();
                }

                MessageBox.Show(
                    ex.Message,
                    MensajeIdioma.T(
                        "FRMLogin.MsgErrorLogin.Titulo",
                        "Error de inicio de sesión"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtContraseña.Clear();
                txtContraseña.Focus();
            }
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) 
            {
                txtContraseña.PasswordChar = '\0';
            }
            else
            {
                txtContraseña.PasswordChar = '*';
            }

        }
    }
}
