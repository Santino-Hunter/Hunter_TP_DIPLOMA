using BLL;
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

namespace GUI
{
    public partial class FRMIntegridad : Form
    {
        private readonly BLLDigitoVerificador bllDV = new BLLDigitoVerificador();
        private readonly BLLBackupRestore bllBackupRestore = new BLLBackupRestore();
        private readonly BLLBitacoraEvento bllBitacora = new BLLBitacoraEvento();



        public FRMIntegridad(ResultadoIntegridad resultado)
        {
            InitializeComponent();
            CargarResultado(resultado);

            if (resultado != null && !resultado.Correcto)
            {
                string usuario = "Sistema";

                if (SERVICESSessionManager.IsLogged &&
                    SERVICESSessionManager.ObtenerInstancia.Usuario != null)
                {
                    usuario = SERVICESSessionManager.ObtenerInstancia.Usuario.Login;
                }

                string tablas = "No determinado";

                if (resultado.Detalles != null && resultado.Detalles.Count > 0)
                {
                    tablas = string.Join(", ",
                        resultado.Detalles
                            .Select(x => x.Tabla)
                            .Distinct()
                            .ToList());
                }

                bllBitacora.RegistrarEvento(
                    usuario,
                    "Dígito Verificador",
                    "Se detectó una inconsistencia en la integridad de la base de datos. Tablas afectadas: " + tablas,
                    5);
            }
        }

        private void CargarResultado(ResultadoIntegridad resultado)
        {
            if (resultado == null)
            {
                TXTDetalle.Text = "No se pudo obtener el detalle de la inconsistencia.";
                return;
            }

            TXTDetalle.Text = resultado.ObtenerDetalleTexto();
        }

        private void BTNRecalcular_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show(
        "Recalcular acepta el estado actual de la base de datos y genera nuevos DV. No recupera datos perdidos. ¿Desea continuar?",
        "Recalcular Dígitos Verificadores",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                ResultadoIntegridad resultado = bllDV.Recalcular();

                if (!resultado.Correcto)
                {
                    CargarResultado(resultado);
                    MessageBox.Show(resultado.Mensaje, "Dígito Verificador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string usuario = "Sistema";

                if (SERVICESSessionManager.IsLogged &&
                    SERVICESSessionManager.ObtenerInstancia.Usuario != null)
                {
                    usuario = SERVICESSessionManager.ObtenerInstancia.Usuario.Login;
                }

                bllBitacora.RegistrarEvento(
                    usuario,
                    "Dígito Verificador",
                    "Se recalcularon los Dígitos Verificadores durante la reparación de integridad.",
                    5);

                MessageBox.Show(resultado.Mensaje, "Dígito Verificador", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dígito Verificador", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTNRestore_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show(
                "El Restore reemplazará la base de datos actual por una copia de seguridad. Se perderán los cambios realizados después de ese backup. ¿Desea continuar?",
                "Restore BD",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes)
                return;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Seleccionar backup de base de datos";
                openFileDialog.Filter = "Backup SQL Server (*.bak)|*.bak";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    bllBackupRestore.RealizarRestore(openFileDialog.FileName);

                    ResultadoIntegridad resultado = bllDV.Verificar();

                    if (!resultado.Correcto)
                    {
                        CargarResultado(resultado);

                        if (resultado.RequiereInicializacion)
                        {
                            MessageBox.Show(
                                "El restore finalizó correctamente. La base restaurada tiene Dígitos Verificadores de una versión anterior. Presione Recalcular DV una vez para inicializar el detalle por fila.",
                                "Restore BD",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            return;
                        }

                        MessageBox.Show(
                            "El restore finalizó, pero la base restaurada sigue presentando inconsistencias de integridad.",
                            "Restore BD",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    string usuario = "Sistema";

                    if (SERVICESSessionManager.IsLogged &&
                        SERVICESSessionManager.ObtenerInstancia.Usuario != null)
                    {
                        usuario = SERVICESSessionManager.ObtenerInstancia.Usuario.Login;
                    }

                    bllBitacora.RegistrarEvento(
                        usuario,
                        "Backup/Restore",
                        "Se restauró la base de datos desde backup por inconsistencia de integridad.",
                        5);

                    MessageBox.Show(
                        "La base de datos fue restaurada correctamente. El sistema volverá al login.",
                        "Restore BD",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo restaurar la base de datos: " + ex.Message,
                        "Restore BD",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
