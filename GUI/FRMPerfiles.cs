using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SERVICES.SERVICESCambioIdioma;


namespace GUI
{
    public partial class FRMPerfiles : Form, IIdiomaObservador
    {
        private SERVICES.SERVICESPerfil perfil;
        private BLL.BLLPerfil gestorPerfil = new BLL.BLLPerfil();
        private BLL.BLLFamilia gestorFamilia = new BLL.BLLFamilia();
        private BLL.BLLPermiso gestorPermiso = new BLL.BLLPermiso();

        public FRMPerfiles()
        {
            InitializeComponent();
            GestorIdioma.Instancia.Suscribir(this);
            ActualizarIdioma();

        }

        private void FRMPerfiles_Load(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            tvPerfiles.Nodes.Clear();
            tvFamilias.Nodes.Clear();

            lstPermisos.DataSource = null;
            cmbPermisos.DataSource = null;
            cmbFamilias.DataSource = null;
            cmbPerfiles.DataSource = null;

            List<SERVICES.SERVICESFamilia> perfiles = gestorPerfil.Listar();
            List<SERVICES.SERVICESFamilia> familias = gestorFamilia.Listar();
            List<SERVICES.SERVICESPermiso> permisos = gestorPermiso.Listar();

            foreach (SERVICES.SERVICESPerfil perfil in perfiles)
            {
                AgregarNodo(tvPerfiles.Nodes, perfil, new HashSet<string>());
            }

            foreach (SERVICES.SERVICESFamilia familia in familias)
            {
                AgregarNodo(tvFamilias.Nodes, familia, new HashSet<string>());
            }

            ConfigurarCombo(cmbPermisos, permisos);
            ConfigurarCombo(cmbFamilias, familias);
            ConfigurarCombo(cmbPerfiles, perfiles);

            lstPermisos.DataSource = permisos;
            lstPermisos.DisplayMember = "Nombre";
        }

        private void AgregarNodo(TreeNodeCollection nodos, SERVICES.SERVICESPerfil componente, HashSet<string> visitados)
        {
            if (componente == null)
                return;

            string clave = componente.GetType().Name + "-" + componente.ID;

            TreeNode nodo = new TreeNode(componente.Nombre);
            nodo.Tag = componente;
            nodos.Add(nodo);

            if (visitados.Contains(clave))
            {
                nodo.Nodes.Add("Relación circular detectada");
                return;
            }

            visitados.Add(clave);

            if (componente is SERVICES.SERVICESFamilia)
            {
                foreach (SERVICES.SERVICESPerfil hijo in componente.Permisos)
                {
                    AgregarNodo(nodo.Nodes, hijo, new HashSet<string>(visitados));
                }
            }
        }

        private void ConfigurarCombo<T>(ComboBox combo, List<T> datos)
        {
            combo.DataSource = null;
            combo.DisplayMember = "Nombre";
            combo.ValueMember = "ID";
            combo.DataSource = datos;
            combo.SelectedIndex = -1;
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            cmbPermisos.SelectedIndex = -1;
            cmbFamilias.SelectedIndex = -1;
            cmbPerfiles.SelectedIndex = -1;
        }

        private bool DatosTextoValidos()
        {
            return !string.IsNullOrWhiteSpace(txtNombre.Text) &&
                   !string.IsNullOrWhiteSpace(txtDescripcion.Text);
        }

        private T ObtenerSeleccion<T>(ComboBox combo, string mensaje) where T : class
        {
            T seleccionado = combo.SelectedItem as T;

            if (seleccionado == null)
                throw new Exception(mensaje);

            return seleccionado;
        }

        private void MostrarResultado(int resultado, string claveOk, string mensajeOk, string claveSinCambios, string mensajeSinCambios)
        {
            if (resultado > 0)
            {
                MensajeIdioma.Info(
                    claveOk,
                    mensajeOk,
                    "FRMPerfiles.MsgExito.Titulo",
                    "Éxito");
            }
            else if (resultado == 0)
            {
                MensajeIdioma.Info(
                    claveSinCambios,
                    mensajeSinCambios,
                    "FRMPerfiles.MsgSinCambios.Titulo",
                    "Sin cambios");
            }
            else
            {
                MensajeIdioma.Error(
                    "FRMPerfiles.MsgErrorBD.Text",
                    "No se pudo realizar la operación en la base de datos.",
                    "FRMPerfiles.MsgError.Titulo",
                    "Error");
            }
        }

        private void MsgDatosIncompletos()
        {
            MensajeIdioma.Advertencia(
                "FRMPerfiles.MsgDatosIncompletos.Text",
                "Debe completar nombre y descripción.",
                "FRMPerfiles.MsgAdvertencia.Titulo",
                "Advertencia");
        }

        private void MsgError(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                MensajeIdioma.T("FRMPerfiles.MsgError.Titulo", "Error"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void btnCrearPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DatosTextoValidos())
                {
                    MsgDatosIncompletos();
                    return;
                }

                perfil = new SERVICES.SERVICESPermiso(txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
                int res = gestorPermiso.Insertar((SERVICES.SERVICESPermiso)perfil);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgPermisoCreado.Text",
                    "Se creó el permiso correctamente.",
                    "FRMPerfiles.MsgPermisoNoCreado.Text",
                    "No se creó ningún permiso.");
            }
            catch (Exception ex)
            {
                MsgError(ex);            
            }
        }

        private void btnModificarPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DatosTextoValidos())
                {
                    MsgDatosIncompletos(); return;
                }

                SERVICES.SERVICESPermiso permiso = ObtenerSeleccion<SERVICES.SERVICESPermiso>(cmbPermisos, "Debe seleccionar un permiso.");
                permiso.Nombre = txtNombre.Text.Trim();
                permiso.Descripcion = txtDescripcion.Text.Trim();

                int res = gestorPermiso.Modificar(permiso);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgPermisoModificado.Text",
                    "Se modificó el permiso correctamente.",
                    "FRMPerfiles.MsgPermisoNoModificado.Text",
                    "No se modificó ningún permiso.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnEliminarPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                SERVICES.SERVICESPermiso permiso = ObtenerSeleccion<SERVICES.SERVICESPermiso>(cmbPermisos, "Debe seleccionar un permiso.");
                int res = gestorPermiso.Eliminar(permiso);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgPermisoEliminado.Text",
                    "Se eliminó el permiso correctamente.",
                    "FRMPerfiles.MsgPermisoNoEliminado.Text",
                    "No se eliminó ningún permiso.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnCrearFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DatosTextoValidos())
                {
                    MsgDatosIncompletos();
                    return;
                }

                perfil = new SERVICES.SERVICESFamilia(txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
                int res = gestorFamilia.Insertar((SERVICES.SERVICESFamilia)perfil);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgFamiliaCreada.Text",
                    "Se creó la familia correctamente.",
                    "FRMPerfiles.MsgFamiliaNoCreada.Text",
                    "No se creó ninguna familia.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnModificarFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DatosTextoValidos())
                {
                    MsgDatosIncompletos();
                    return;
                }

                SERVICES.SERVICESFamilia familia = ObtenerSeleccion<SERVICES.SERVICESFamilia>(cmbFamilias, "Debe seleccionar una familia.");
                familia.Nombre = txtNombre.Text.Trim();
                familia.Descripcion = txtDescripcion.Text.Trim();

                int res = gestorFamilia.Modificar(familia);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgFamiliaModificada.Text",
                    "Se modificó la familia correctamente.",
                    "FRMPerfiles.MsgFamiliaNoModificada.Text",
                    "No se modificó ninguna familia.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnEliminarFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                SERVICES.SERVICESFamilia familia = ObtenerSeleccion<SERVICES.SERVICESFamilia>(cmbFamilias, "Debe seleccionar una familia.");
                int res = gestorFamilia.Eliminar(familia);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgFamiliaEliminada.Text",
                    "Se eliminó la familia correctamente.",
                    "FRMPerfiles.MsgFamiliaNoEliminada.Text",
                    "No se eliminó ninguna familia.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnAsignarPermisoAFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                SERVICES.SERVICESFamilia familia = ObtenerSeleccion<SERVICES.SERVICESFamilia>(cmbFamilias, "Debe seleccionar una familia.");
                SERVICES.SERVICESPermiso permiso = ObtenerSeleccion<SERVICES.SERVICESPermiso>(cmbPermisos, "Debe seleccionar un permiso.");

                int res = gestorFamilia.AgregarPermiso(familia, permiso);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgPermisoAsignadoFamilia.Text",
                    "Se asignó el permiso a la familia correctamente.",
                    "FRMPerfiles.MsgPermisoYaAsignadoFamilia.Text",
                    "La familia ya tenía ese permiso asignado.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnQuitarPermisoAFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                SERVICES.SERVICESFamilia familia = ObtenerSeleccion<SERVICES.SERVICESFamilia>(cmbFamilias, "Debe seleccionar una familia.");
                SERVICES.SERVICESPermiso permiso = ObtenerSeleccion<SERVICES.SERVICESPermiso>(cmbPermisos, "Debe seleccionar un permiso.");

                int res = gestorFamilia.QuitarPermiso(familia, permiso);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgPermisoQuitadoFamilia.Text",
                    "Se quitó el permiso de la familia correctamente.",
                    "FRMPerfiles.MsgPermisoNoAsignadoFamilia.Text",
                    "La familia no tenía ese permiso asignado.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnCrearPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DatosTextoValidos())
                {
                    MsgDatosIncompletos();
                    return;
                }

                perfil = new SERVICES.SERVICESFamilia(txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
                int res = gestorPerfil.Insertar(perfil);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgPerfilCreado.Text",
                    "Se creó el perfil correctamente.",
                    "FRMPerfiles.MsgPerfilNoCreado.Text",
                    "No se creó ningún perfil.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnModificarPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DatosTextoValidos())
                {
                    MsgDatosIncompletos();
                    return;
                }

                SERVICES.SERVICESPerfil perfilSeleccionado = ObtenerSeleccion<SERVICES.SERVICESPerfil>(cmbPerfiles, "Debe seleccionar un perfil.");
                perfilSeleccionado.Nombre = txtNombre.Text.Trim();
                perfilSeleccionado.Descripcion = txtDescripcion.Text.Trim();

                int res = gestorPerfil.Modificar(perfilSeleccionado);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgPerfilModificado.Text",
                    "Se modificó el perfil correctamente.",
                    "FRMPerfiles.MsgPerfilNoModificado.Text",
                    "No se modificó ningún perfil.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnEliminarPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                SERVICES.SERVICESPerfil perfilSeleccionado = ObtenerSeleccion<SERVICES.SERVICESPerfil>(cmbPerfiles, "Debe seleccionar un perfil.");
                int res = gestorPerfil.Eliminar(perfilSeleccionado);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgPerfilEliminado.Text",
                    "Se eliminó el perfil correctamente.",
                    "FRMPerfiles.MsgPerfilNoEliminado.Text",
                    "No se eliminó ningún perfil.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnAsignarPermisoAPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                SERVICES.SERVICESPerfil perfilSeleccionado = ObtenerSeleccion<SERVICES.SERVICESPerfil>(cmbPerfiles, "Debe seleccionar un perfil.");
                SERVICES.SERVICESPermiso permiso = ObtenerSeleccion<SERVICES.SERVICESPermiso>(cmbPermisos, "Debe seleccionar un permiso.");

                int res = gestorPerfil.AgregarPermisoFamilia(perfilSeleccionado, permiso);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgPermisoAsignadoPerfil.Text",
                    "Se asignó el permiso al perfil correctamente.",
                    "FRMPerfiles.MsgPermisoYaAsignadoPerfil.Text",
                    "El perfil ya tenía ese permiso asignado.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnQuitarPermisoAPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                SERVICES.SERVICESPerfil perfilSeleccionado = ObtenerSeleccion<SERVICES.SERVICESPerfil>(cmbPerfiles, "Debe seleccionar un perfil.");
                SERVICES.SERVICESPermiso permiso = ObtenerSeleccion<SERVICES.SERVICESPermiso>(cmbPermisos, "Debe seleccionar un permiso.");

                int res = gestorPerfil.QuitarPermisoFamilia(perfilSeleccionado, permiso);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgPermisoQuitadoPerfil.Text",
                    "Se quitó el permiso del perfil correctamente.",
                    "FRMPerfiles.MsgPermisoNoAsignadoPerfil.Text",
                    "El perfil no tenía ese permiso asignado.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnAsignarFamiliaAPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                SERVICES.SERVICESPerfil perfilSeleccionado = ObtenerSeleccion<SERVICES.SERVICESPerfil>(cmbPerfiles, "Debe seleccionar un perfil.");
                SERVICES.SERVICESFamilia familia = ObtenerSeleccion<SERVICES.SERVICESFamilia>(cmbFamilias, "Debe seleccionar una familia.");

                int res = gestorPerfil.AgregarPermisoFamilia(perfilSeleccionado, familia);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgFamiliaAsignadaPerfil.Text",
                    "Se asignó la familia al perfil correctamente.",
                    "FRMPerfiles.MsgFamiliaYaAsignadaPerfil.Text",
                    "El perfil ya tenía esa familia asignada.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
        }

        private void btnQuitarFamiliaAPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                SERVICES.SERVICESPerfil perfilSeleccionado = ObtenerSeleccion<SERVICES.SERVICESPerfil>(cmbPerfiles, "Debe seleccionar un perfil.");
                SERVICES.SERVICESFamilia familia = ObtenerSeleccion<SERVICES.SERVICESFamilia>(cmbFamilias, "Debe seleccionar una familia.");

                int res = gestorPerfil.QuitarPermisoFamilia(perfilSeleccionado, familia);

                Actualizar();
                Limpiar();
                MostrarResultado(
                    res,
                    "FRMPerfiles.MsgFamiliaQuitadaPerfil.Text",
                    "Se quitó la familia del perfil correctamente.",
                    "FRMPerfiles.MsgFamiliaNoAsignadaPerfil.Text",
                    "El perfil no tenía esa familia asignada.");
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }
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
