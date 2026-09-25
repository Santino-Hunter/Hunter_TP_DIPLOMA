using SERVICES;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SERVICES.SERVICESCambioIdioma;


namespace GUI
{
    public partial class FRMUsuario : Form, IIdiomaObservador
    {
        BLLUsuario bllUsuario = new BLLUsuario();
        BLLPerfil bllPerfil = new BLLPerfil();
        string modo = "Modo Consulta.";

        private List<SERVICESUsuario> listaUsuarios = new List<SERVICESUsuario>();

        public FRMUsuario()
        {
            InitializeComponent();

            GestorIdioma.Instancia.Suscribir(this);
            ActualizarIdioma();
        }

        private void FRMUsuario_Load(object sender, EventArgs e)
        {
            DGVUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVUsuarios.MultiSelect = false;
            DGVUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            CargarRoles();
            ActualizarGrilla();
            ModoConsulta();
        }

        private void DGVUsuarios_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (modo == "Modo Modificar." && DGVUsuarios.SelectedRows.Count > 0)
            {
                int dni = int.Parse(DGVUsuarios.SelectedRows[0].Cells[0].Value.ToString());
                SERVICESUsuario usuario = bllUsuario.ObtenerTodos()
                                              .FirstOrDefault(x => x.DNI == dni);
                if (usuario != null)
                {
                    TXTNombre.Text = usuario.Nombre;
                    TXTApellido.Text = usuario.Apellido;
                    TXTMail.Text = usuario.Mail;
                    SeleccionarRol(usuario.Rol);
                }
            }
        }
        private void MsgCamposIncompletos()
        {
            MensajeIdioma.Advertencia(
                "FRMUsuario.MsgCamposIncompletos.Text",
                "Complete todos los campos.",
                "FRMUsuario.MsgAdvertencia.Titulo",
                "Advertencia");
        }

        private void MsgSeleccioneUsuario()
        {
            MensajeIdioma.Advertencia(
                "FRMUsuario.MsgSeleccioneUsuario.Text",
                "Seleccione un usuario.",
                "FRMUsuario.MsgAdvertencia.Titulo",
                "Advertencia");
        }

        private void MsgUsuarioNoEncontrado()
        {
            MensajeIdioma.Error(
                "FRMUsuario.MsgUsuarioNoEncontrado.Text",
                "Usuario no encontrado.",
                "FRMUsuario.MsgError.Titulo",
                "Error");
        }

        private void MsgPerfilInvalido()
        {
            MensajeIdioma.Advertencia(
                "FRMUsuario.MsgPerfilInvalido.Text",
                "Debe seleccionar un perfil válido.",
                "FRMUsuario.MsgAdvertencia.Titulo",
                "Advertencia");
        }

        private void MsgError(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                MensajeIdioma.T("FRMUsuario.MsgError.Titulo", "Error"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        private void CargarRoles()
        {
            CBRol.DataSource = null;
            CBRol.DisplayMember = "Nombre";
            CBRol.ValueMember = "ID";
            CBRol.DataSource = bllPerfil.Listar();
            CBRol.SelectedIndex = -1;
        }

        private void SeleccionarRol(SERVICESPerfil rol)
        {
            if (rol == null)
            {
                CBRol.SelectedIndex = -1;
                return;
            }

            foreach (object item in CBRol.Items)
            {
                SERVICESPerfil perfil = item as SERVICESPerfil;

                if (perfil != null && perfil.ID == rol.ID)
                {
                    CBRol.SelectedItem = item;
                    return;
                }
            }

            CBRol.SelectedIndex = -1;
        }

        private SERVICESPerfil ObtenerRolSeleccionado()
        {
            SERVICESPerfil perfil = CBRol.SelectedItem as SERVICESPerfil;

            if (perfil == null || perfil.ID <= 0)
                return null;
            return perfil;
        }

        private void ActualizarGrilla()
        {
            listaUsuarios = bllUsuario.ObtenerTodos();

            AplicarFiltroActual();

        }

        private void AplicarFiltroActual()
        {
            if (RBActivos.Checked)
            {
                List<SERVICESUsuario> usuariosActivos = listaUsuarios
                    .Where(usu => usu.Activo && !usu.Bloqueado)
                    .ToList();

                CargarGrilla(usuariosActivos);
            }
            else
            {
                CargarGrilla(listaUsuarios);
            }
        }

        private void CargarGrilla(List<SERVICESUsuario> usuarios)
        {
            DGVUsuarios.DataSource = null;

            var lista = from x in usuarios
                        select new
                        {
                            x.DNI,
                            x.Nombre,
                            x.Apellido,
                            Usuario = x.Login,
                            Rol = x.Rol != null ? x.Rol.Nombre : "",
                            x.Mail,
                            x.Activo
                        };

            DGVUsuarios.DataSource = lista.ToList();

            ActualizarIdioma();

            DGVUsuarios.ClearSelection();

            BeginInvoke(new Action(() =>
            {
                PintarUsuariosBloqueados(usuarios);
            }));
        }

        private void MostrarModoActual()
        {
            if (modo == "Modo Consulta.")
                TXTMensaje.Text = TraductorIdioma.Texto("FRMUsuario.ModoConsulta.Text", "Modo Consulta.");
            else if (modo == "Modo Crear.")
                TXTMensaje.Text = TraductorIdioma.Texto("FRMUsuario.ModoCrear.Text", "Modo Crear.");
            else if (modo == "Modo Modificar.")
                TXTMensaje.Text = TraductorIdioma.Texto("FRMUsuario.ModoModificar.Text", "Modo Modificar.");
            else if (modo == "Modo Desbloquear.")
                TXTMensaje.Text = TraductorIdioma.Texto("FRMUsuario.ModoDesbloquear.Text", "Modo Desbloquear.");
            else if (modo == "Modo Act./Des.")
                TXTMensaje.Text = TraductorIdioma.Texto("FRMUsuario.ModoActDes.Text", "Modo Act./Des.");
            else
                TXTMensaje.Text = modo;
        }

        private void PintarUsuariosBloqueados(List<SERVICESUsuario> usuarios)
        {
            foreach (DataGridViewRow fila in DGVUsuarios.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                int dni = Convert.ToInt32(fila.Cells[0].Value);

                SERVICESUsuario usuario = usuarios.FirstOrDefault(x => x.DNI == dni);

                foreach (DataGridViewCell celda in fila.Cells)
                {
                    celda.Style.ForeColor = Color.Black;
                    celda.Style.SelectionForeColor = Color.Black;
                }

                if (usuario != null && usuario.Bloqueado)
                {
                    foreach (DataGridViewCell celda in fila.Cells)
                    {
                        celda.Style.ForeColor = Color.Red;
                        celda.Style.SelectionForeColor = Color.Red;
                    }
                }
            }

            DGVUsuarios.ClearSelection();
        }

        private void ModificarYCrear()
        {
            TXTDNI.Visible = true;
            TXTNombre.Visible = true;
            TXTApellido.Visible = true;
            CBRol.Visible = true;
            TXTMail.Visible = true;

            LBDNI.Visible = true;
            LBNombre.Visible = true;
            LBApellido.Visible = true;
            LBRol.Visible = true;
            LBMail.Visible = true;

            BTNCrear.Enabled = false;
            BTNDesbloquear.Enabled = false;
            BTNModificar.Enabled = false;
            BTNActivarDesactivar.Enabled = false;

            BTNAplicar.Enabled = true;
            BTNCancelar.Enabled = true;
        }

        private void ADU()
        {
            TXTDNI.Visible = false;
            TXTNombre.Visible = false;
            TXTApellido.Visible = false;
            CBRol.Visible = false;
            TXTMail.Visible = false;

            LBDNI.Visible = false;
            LBNombre.Visible = false;
            LBApellido.Visible = false;
            LBRol.Visible = false;
            LBMail.Visible = false;

            BTNCrear.Enabled = false;
            BTNDesbloquear.Enabled = false;
            BTNModificar.Enabled = false;
            BTNActivarDesactivar.Enabled = false;

            BTNAplicar.Enabled = true;
            BTNCancelar.Enabled = true;
        }

        private void ModoConsulta()
        {
            modo = "Modo Consulta.";
            
            MostrarModoActual();

            TXTDNI.Enabled = true;

            TXTDNI.Visible = false;
            TXTNombre.Visible = false;
            TXTApellido.Visible = false;
            CBRol.Visible = false;
            TXTMail.Visible = false;

            LBDNI.Visible = false;
            LBNombre.Visible = false;
            LBApellido.Visible = false;
            LBRol.Visible = false;
            LBMail.Visible = false;

            BTNCrear.Enabled = true;
            BTNDesbloquear.Enabled = true;
            BTNModificar.Enabled = true;
            BTNActivarDesactivar.Enabled = true;

            BTNAplicar.Enabled = false;
            BTNCancelar.Enabled = false;
        }

        private void LimpiarCampos()
        {
            TXTDNI.Clear();
            TXTNombre.Clear();
            TXTApellido.Clear();
            TXTMail.Clear();

            CBRol.SelectedIndex = -1;
        }

        private SERVICESUsuario ObtenerUsuarioSeleccionado()
        {
            if (DGVUsuarios.SelectedRows.Count == 0)
                return null;

            int dni = int.Parse(DGVUsuarios.SelectedRows[0].Cells["DNI"].Value.ToString());

            return bllUsuario.ObtenerTodos().FirstOrDefault(x => x.DNI == dni);
        }

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                if (modo == "Modo Crear.")
                {
                    if (string.IsNullOrWhiteSpace(TXTDNI.Text) || string.IsNullOrWhiteSpace(TXTNombre.Text) || string.IsNullOrWhiteSpace(TXTApellido.Text) ||
                        CBRol.SelectedIndex == -1 || string.IsNullOrWhiteSpace(TXTMail.Text))
                    {
                        MsgCamposIncompletos();
                        return;
                    }

                    SERVICESPerfil perfil = ObtenerRolSeleccionado();

                    if (perfil == null)
                    {
                        MsgPerfilInvalido();
                        return;
                    }

                    SERVICESUsuario usuario = new SERVICESUsuario(
                        int.Parse(TXTDNI.Text.Trim()),
                        TXTNombre.Text.Trim(),
                        TXTApellido.Text.Trim(),
                        TXTDNI.Text.Trim() + TXTNombre.Text.Trim(),
                        "", // La contraseña la genera y hashea BLLUsuario.Guardar()
                        perfil,
                        TXTMail.Text.Trim(),
                        false,
                        true,
                        0
                    );

                    bllUsuario.Guardar(usuario);
                    MensajeIdioma.Info(
                        "FRMUsuario.MsgUsuarioCreado.Text",
                        "Usuario creado correctamente.",
                        "FRMUsuario.MsgOperacionOk.Titulo",
                        "Operación correcta");
                }
                else if (modo == "Modo Modificar.")
                {
                    if (DGVUsuarios.SelectedRows.Count == 0)
                    {
                        MsgSeleccioneUsuario();
                        return;
                    }

                    SERVICESUsuario usuario = ObtenerUsuarioSeleccionado();

                    if (usuario == null)
                    {
                        MsgUsuarioNoEncontrado();
                        return;
                    }

                    usuario.Nombre = TXTNombre.Text.Trim();
                    usuario.Apellido = TXTApellido.Text.Trim();
                    usuario.Mail = TXTMail.Text.Trim();

                    SERVICESPerfil perfil = ObtenerRolSeleccionado();

                    if (perfil == null)
                    {
                        MsgPerfilInvalido();
                        return;
                    }

                    usuario.Rol = perfil;

                    usuario.Login = usuario.DNI.ToString() + usuario.Nombre;

                    bllUsuario.Modificar(usuario);
                    MensajeIdioma.Info(
                        "FRMUsuario.MsgUsuarioModificado.Text",
                        "Usuario modificado correctamente.",
                        "FRMUsuario.MsgOperacionOk.Titulo",
                        "Operación correcta");
                }
                else if (modo == "Modo Desbloquear.")
                {
                    if (DGVUsuarios.SelectedRows.Count == 0)
                    {
                        MsgSeleccioneUsuario();
                        return;
                    }

                    SERVICESUsuario usuario = ObtenerUsuarioSeleccionado();

                    if (usuario == null)
                    {
                        MsgUsuarioNoEncontrado();
                        return;
                    }

                    if (!usuario.Bloqueado)
                    {
                        MensajeIdioma.Advertencia(
                            "FRMUsuario.MsgUsuarioNoBloqueado.Text",
                            "El usuario seleccionado no está bloqueado.",
                            "FRMUsuario.MsgAdvertencia.Titulo",
                            "Advertencia");
                        return;
                    }

                    DialogResult respuesta = MensajeIdioma.PreguntaSiNo(
                         "FRMUsuario.MsgConfirmarDesbloqueo.Text",
                         "¿Está seguro que desea desbloquear este usuario?",
                         "FRMUsuario.MsgConfirmarDesbloqueo.Titulo",
                         "Confirmar desbloqueo");

                    if (respuesta == DialogResult.Yes)
                    {
                        bllUsuario.Desbloquear(usuario);

                        MensajeIdioma.Info(
                            "FRMUsuario.MsgUsuarioDesbloqueado.Text",
                            "Usuario desbloqueado correctamente.",
                            "FRMUsuario.MsgOperacionOk.Titulo",
                            "Operación correcta");
                    }
                }
                else if (modo == "Modo Act./Des.")
                {
                    if (DGVUsuarios.SelectedRows.Count == 0)
                    {
                        MsgSeleccioneUsuario();
                        return;
                    }

                    SERVICESUsuario usuario = ObtenerUsuarioSeleccionado();

                    if (usuario == null)
                    {
                        MsgUsuarioNoEncontrado();
                        return;
                    }

                    if (usuario.Activo)
                    {
                        DialogResult respuesta = MensajeIdioma.PreguntaSiNo(
                             "FRMUsuario.MsgConfirmarDesactivacion.Text",
                             "¿Está seguro que desea desactivar este usuario?",
                             "FRMUsuario.MsgConfirmarDesactivacion.Titulo",
                             "Confirmar desactivación");

                        if (respuesta == DialogResult.Yes)
                        {
                            bllUsuario.Desactivar(usuario);
                            MensajeIdioma.Info(
                                "FRMUsuario.MsgUsuarioDesactivado.Text",
                                "Usuario desactivado correctamente.",
                                "FRMUsuario.MsgOperacionOk.Titulo",
                                "Operación correcta");
                        }
                    }
                    else
                    {
                        DialogResult respuesta = MensajeIdioma.PreguntaSiNo(
                             "FRMUsuario.MsgConfirmarActivacion.Text",
                             "¿Está seguro que desea activar este usuario?",
                             "FRMUsuario.MsgConfirmarActivacion.Titulo",
                             "Confirmar activación");

                        if (respuesta == DialogResult.Yes)
                        {
                            bllUsuario.Activar(usuario);
                            MensajeIdioma.Info(
                                "FRMUsuario.MsgUsuarioActivado.Text",
                                "Usuario activado correctamente.",
                                "FRMUsuario.MsgOperacionOk.Titulo",
                                "Operación correcta");
                        }
                    }
                }
                ActualizarGrilla();
                LimpiarCampos();
                ModoConsulta();
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void BTNCrear_Click(object sender, EventArgs e)
        {
            try
            {
                modo = "Modo Crear.";

                MostrarModoActual();
                
                ModificarYCrear();

                TXTDNI.Enabled = true;
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void BTNDesbloquear_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVUsuarios.SelectedRows.Count == 0)
                {
                    MsgSeleccioneUsuario();
                    return;
                }

                modo = "Modo Desbloquear.";
                
                MostrarModoActual();
                
                ADU();
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void BTNModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVUsuarios.SelectedRows.Count == 0)
                {
                    MsgSeleccioneUsuario();
                    return;
                }

                modo = "Modo Modificar.";
                
                MostrarModoActual();
                
                ModificarYCrear();

                SERVICESUsuario usuario = ObtenerUsuarioSeleccionado();

                if (usuario == null)
                {
                    MsgUsuarioNoEncontrado();
                    return;
                }

                TXTDNI.Text = usuario.DNI.ToString();
                TXTDNI.Enabled = false;

                TXTNombre.Text = usuario.Nombre;
                TXTApellido.Text = usuario.Apellido;
                TXTMail.Text = usuario.Mail;
                SeleccionarRol(usuario.Rol);
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void BTNActivarDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVUsuarios.SelectedRows.Count == 0)
                {
                    MsgSeleccioneUsuario();
                    return;
                }

                modo = "Modo Act./Des.";
                MostrarModoActual();
                
                ADU();
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            ModoConsulta();
            ActualizarGrilla();
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RBActivos_CheckedChanged(object sender, EventArgs e)
        {
            AplicarFiltroActual();
        }

        private void RBTodos_CheckedChanged(object sender, EventArgs e)
        {
            AplicarFiltroActual();
        }

        public void ActualizarIdioma()
        {
            AplicadorIdioma.Aplicar(this);
            MostrarModoActual();

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
