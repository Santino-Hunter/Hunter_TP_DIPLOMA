using BLL;
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
    public partial class FRMCambiarContraseña : Form, IIdiomaObservador
    {
        public FRMCambiarContraseña()
        {
            InitializeComponent();

            GestorIdioma.Instancia.Suscribir(this);
            ActualizarIdioma();
        }

        BLLUsuario bllUsuario = new BLLUsuario();

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNuevaContraseña.Text) &&
                    !string.IsNullOrWhiteSpace(txtConfirmarContraseña.Text))
                {
                    if (txtNuevaContraseña.Text == txtConfirmarContraseña.Text)
                    {
                        if (txtNuevaContraseña.Text.Length < 6)
                        {
                            MessageBox.Show(
                                T("MsgContraseñaDebil.Text", "La contraseña debe tener al menos 6 caracteres."),
                                T("MsgContraseñaDebil.Titulo", "Contraseña débil"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }

                        bllUsuario.CambiarContraseña(
                            SERVICESSessionManager.ObtenerInstancia.Usuario.DNI,
                            SERVICESSessionManager.ObtenerInstancia.Usuario.Contraseña,
                            txtNuevaContraseña.Text,
                            txtConfirmarContraseña.Text);

                        SERVICESSessionManager.Logout();

                        MessageBox.Show(
                            T("MsgCambioExitoso.Text", "Contraseña cambiada exitosamente."),
                            T("MsgCambioExitoso.Titulo", "Cambio de contraseña"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        this.Close();

                        Application.OpenForms.OfType<FRMMenu>().ToList().ForEach(form => form.Close());

                        List<FRMLogin> openLoginForms = Application.OpenForms.OfType<FRMLogin>().ToList();

                        if (openLoginForms.Count > 1)
                            openLoginForms[1].Close();

                        openLoginForms[0].Show();
                    }
                    else
                    {
                        MessageBox.Show(
                            T("MsgNoCoinciden.Text", "Las contraseñas no coinciden. Por favor, inténtelo de nuevo."),
                            T("MsgError.Titulo", "Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(
                        T("MsgCamposVacios.Text", "Debe completar ambos campos de contraseña."),
                        T("MsgError.Titulo", "Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    T("MsgError.Titulo", "Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
        private string T(string clave, string textoOriginal)
        {
            return TraductorIdioma.Texto("FRMCambiarContraseña." + clave, textoOriginal);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ActualizarIdioma()
        {
            AplicadorIdioma.Aplicar(this);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}