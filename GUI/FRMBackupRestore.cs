using BLL;
using SERVICES;
using System;
using System.IO;
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
    public partial class FRMBackupRestore : Form
    {
        public FRMBackupRestore()
        {
            InitializeComponent();
            SeRestauro = false;

            ConfigurarModo(Modo.Restore);

        }

        public bool SeRestauro { get; private set; }

        private readonly BLLBackupRestore bllBackupRestore = new BLLBackupRestore();
        private readonly BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
        private readonly BLLBitacoraEvento bllBitacora = new BLLBitacoraEvento();

        public enum Modo
        {
            Restore,
            Backup
        }

        public FRMBackupRestore(Modo modo) : this()
        {
            ConfigurarModo(modo);
        }

        private void ConfigurarModo(Modo modo)
        {
            grpRestore.Visible = true;
            grpBackup.Visible = true;

            if (modo == Modo.Restore)
            {
                grpRestore.Enabled = true;
                grpBackup.Enabled = false;

                Text = "Restaurar base de datos";
            }
            else
            {
                grpRestore.Enabled = false;
                grpBackup.Enabled = true;

                Text = "Generar copia de seguridad";
            }
        }

        private void btnExaminarRestore_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Title = "Seleccionar archivo de backup";
                dialogo.Filter = "Archivos de backup de SQL Server (*.bak)|*.bak";
                dialogo.FilterIndex = 1;
                dialogo.CheckFileExists = true;
                dialogo.CheckPathExists = true;
                dialogo.Multiselect = false;
                dialogo.RestoreDirectory = true;

                if (dialogo.ShowDialog() == DialogResult.OK)
                {
                    txtArchivoBackup.Text = dialogo.FileName;
                }
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArchivoBackup.Text))
            {
                MessageBox.Show(
                    "Debe seleccionar un archivo de backup.",
                    "Restore",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "La restauración reemplazará todos los datos actuales " +
                "por los contenidos en el backup seleccionado.\n\n" +
                "Al finalizar, se cerrará la sesión activa.\n\n" +
                "¿Desea continuar?",
                "Confirmar Restore",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes)
                return;

            string usuario = "Sistema";

            if (SERVICESSessionManager.IsLogged &&
                SERVICESSessionManager.ObtenerInstancia.Usuario != null)
            {
                usuario =
                    SERVICESSessionManager.ObtenerInstancia.Usuario.Login;
            }

            bool restoreRealizado = false;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnRestaurar.Enabled = false;
                btnExaminarRestore.Enabled = false;
                btnCancelar.Enabled = false;

                bllBackupRestore.RealizarRestore(
                    txtArchivoBackup.Text.Trim());

                restoreRealizado = true;

                bllDV.RecalcularSeguridad();

                bllBitacora.RegistrarEvento(
                    usuario,
                    "Backup/Restore",
                    "Se restauró la base de datos desde el archivo: " +
                    txtArchivoBackup.Text.Trim(),
                    1);

                MessageBox.Show(
                    "La base de datos fue restaurada correctamente " +
                    "y sus Dígitos Verificadores fueron recalculados.\n\n" +
                    "La sesión se cerrará por seguridad.",
                    "Restore finalizado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                SeRestauro = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                if (restoreRealizado)
                {
                    MessageBox.Show(
                        "La base de datos fue restaurada, pero ocurrió " +
                        "un error durante las operaciones posteriores:\n\n" +
                        ex.Message +
                        "\n\nLa sesión se cerrará por seguridad.",
                        "Restore realizado con advertencias",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    SeRestauro = true;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo restaurar la base de datos:\n\n" +
                        ex.Message,
                        "Error de Restore",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                Cursor = Cursors.Default;

                if (!IsDisposed)
                {
                    btnRestaurar.Enabled = true;
                    btnExaminarRestore.Enabled = true;
                    btnCancelar.Enabled = true;
                }
            }
        }

        private void btnExaminarBackup_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialogo = new FolderBrowserDialog())
            {
                dialogo.Description =
                    "Seleccione la carpeta donde se guardará el backup";

                dialogo.ShowNewFolderButton = true;

                if (dialogo.ShowDialog() == DialogResult.OK)
                {
                    txtCarpetaDestino.Text = dialogo.SelectedPath;
                }
            }
        }

        private void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCarpetaDestino.Text))
            {
                MessageBox.Show(
                    "Debe seleccionar una carpeta de destino.",
                    "Backup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Desea generar una copia de seguridad de la base de datos?",
                "Confirmar Backup",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnGenerarBackup.Enabled = false;
                btnExaminarBackup.Enabled = false;
                btnCancelarBackup.Enabled = false;

                string nombreArchivo =
                    "CH_Restaurante_" +
                    DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                    ".bak";

                string rutaCompleta = Path.Combine(
                    txtCarpetaDestino.Text.Trim(),
                    nombreArchivo);

                bllBackupRestore.RealizarBackup(rutaCompleta);

                string usuario = "Sistema";

                if (SERVICESSessionManager.IsLogged &&
                    SERVICESSessionManager.ObtenerInstancia.Usuario != null)
                {
                    usuario =
                        SERVICESSessionManager.ObtenerInstancia.Usuario.Login;
                }

                bllBitacora.RegistrarEvento(
                    usuario,
                    "Backup/Restore",
                    "Generó una copia de seguridad en: " + rutaCompleta,
                    1);

                MessageBox.Show(
                    "La copia de seguridad se generó correctamente.\n\n" +
                    "Ubicación:\n" + rutaCompleta,
                    "Backup finalizado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtCarpetaDestino.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo generar la copia de seguridad:\n\n" +
                    ex.Message,
                    "Error de Backup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;

                btnGenerarBackup.Enabled = true;
                btnExaminarBackup.Enabled = true;
                btnCancelarBackup.Enabled = true;
            }
        }
    }
}
