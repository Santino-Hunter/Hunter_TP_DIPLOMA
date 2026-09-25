using SERVICES;
using BLL;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SERVICES.SERVICESCambioIdioma;

namespace GUI
{
    public partial class FRMBitacora : Form, IIdiomaObservador
    {
        private BLLBitacoraEvento bllBitacora = new BLLBitacoraEvento();
        private BLLUsuario bllUsuario = new BLLUsuario();

        private List<SERVICESUsuario> usuarios = new List<SERVICESUsuario>();

        public FRMBitacora()
        {
            InitializeComponent();

            GestorIdioma.Instancia.Suscribir(this);
            ActualizarIdioma();
        }

        private void FRMBitacora_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarCombos();
            CargarGrilla(bllBitacora.ObtenerUltimosTresDias());
        }

        private void ConfigurarFormulario()
        {
            DGVBitacora.AutoGenerateColumns = true;

            dtpDesde.Value = DateTime.Now.AddDays(-3);
            dtpHasta.Value = DateTime.Now;
        }

        private void CargarCombos()
        {
            usuarios = bllUsuario.ObtenerTodos();

            cbUsuarios.Items.Clear();
            cbUsuarios.Items.Add("Todos");

            foreach (SERVICESUsuario usuario in usuarios)
            {
                cbUsuarios.Items.Add(usuario.Login);
            }

            cbUsuarios.SelectedIndex = 0;

            cbModulo.Items.Clear();
            cbModulo.Items.Add("Todos");
            cbModulo.Items.Add("Login");
            cbModulo.Items.Add("Logout");
            cbModulo.Items.Add("Usuarios");
            cbModulo.Items.Add("Bitácora");
            cbModulo.SelectedIndex = 0;

            cbCriticidad.Items.Clear();
            cbCriticidad.Items.Add("Todas");
            cbCriticidad.Items.Add("1");
            cbCriticidad.Items.Add("2");
            cbCriticidad.Items.Add("3");
            cbCriticidad.Items.Add("4");
            cbCriticidad.Items.Add("5");
            cbCriticidad.SelectedIndex = 0;
        }

        private void CargarGrilla(List<SERVICESBitacoraEvento> eventos)
        {
            var lista = from e in eventos
                        select new
                        {
                            ID = e.IdEvento,
                            Usuario = e.Login,
                            Fecha = e.FechaHora,
                            Modulo = e.Modulo,
                            Evento = e.Evento,
                            Criticidad = e.Criticidad
                        };

            DGVBitacora.DataSource = null;
            DGVBitacora.DataSource = lista.ToList();

            ActualizarIdioma();
        }

        private void BTNFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                string login = cbUsuarios.Text;
                string modulo = cbModulo.Text;

                int criticidad = 0;

                if (cbCriticidad.Text != "Todas")
                    criticidad = Convert.ToInt32(cbCriticidad.Text);

                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

                List<SERVICESBitacoraEvento> eventos = bllBitacora.Filtrar(login, modulo, criticidad, desde, hasta);

                CargarGrilla(eventos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    MensajeIdioma.T("FRMBitacora.MsgTitulo.Text", "Bitácora"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BTNLimpiar_Click(object sender, EventArgs e)
        {
            cbUsuarios.SelectedIndex = 0;
            cbModulo.SelectedIndex = 0;
            cbCriticidad.SelectedIndex = 0;

            dtpDesde.Value = DateTime.Now.AddDays(-3);
            dtpHasta.Value = DateTime.Now;

            txtNombre.Clear();
            txtApellido.Clear();

            CargarGrilla(bllBitacora.ObtenerUltimosTresDias());
        }

        private void BTNReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVBitacora.Rows.Count == 0)
                {
                    MensajeIdioma.Info(
                        "FRMBitacora.MsgSinDatosReporte.Text",
                        "No hay datos en la grilla para generar el reporte.",
                        "FRMBitacora.MsgTitulo.Text",
                        "Bitácora");
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = MensajeIdioma.T("FRMBitacora.DialogGuardarReporte.Text", "Guardar reporte de bitácora");
                saveFileDialog.Filter = MensajeIdioma.T("FRMBitacora.DialogFiltroPdf.Text", "Archivo PDF (*.pdf)|*.pdf");
                saveFileDialog.FileName = "Reporte_Bitacora_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf";

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                Document documento = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                PdfWriter.GetInstance(documento, new FileStream(saveFileDialog.FileName, FileMode.Create));

                documento.Open();

                Paragraph titulo = new Paragraph(MensajeIdioma.T("FRMBitacora.PdfTitulo.Text", "BITÁCORA DE EVENTOS"));
                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.SpacingAfter = 15f;
                documento.Add(titulo);

                Paragraph fecha = new Paragraph(
                    MensajeIdioma.T("FRMBitacora.PdfFechaGeneracion.Text", "Fecha de generación: ") +
                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                fecha.SpacingAfter = 10f;
                documento.Add(fecha);

                PdfPTable tabla = new PdfPTable(DGVBitacora.Columns.Count);
                tabla.WidthPercentage = 100;

                foreach (DataGridViewColumn columna in DGVBitacora.Columns)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(columna.HeaderText));
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda);
                }

                foreach (DataGridViewRow fila in DGVBitacora.Rows)
                {
                    if (fila.IsNewRow)
                        continue;

                    foreach (DataGridViewCell celdaGrid in fila.Cells)
                    {
                        string texto = "";

                        if (celdaGrid.Value != null)
                            texto = celdaGrid.Value.ToString();

                        PdfPCell celda = new PdfPCell(new Phrase(texto));
                        celda.HorizontalAlignment = Element.ALIGN_LEFT;
                        tabla.AddCell(celda);
                    }
                }

                documento.Add(tabla);

                documento.Close();

                MensajeIdioma.Info(
                    "FRMBitacora.MsgReporteOk.Text",
                    "Reporte PDF generado correctamente.",
                    "FRMBitacora.MsgTitulo.Text",
                    "Bitácora");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    MensajeIdioma.T("FRMBitacora.MsgErrorPdf.Titulo", "Error al generar PDF"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DGVBitacora_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string login = DGVBitacora.Rows[e.RowIndex].Cells["Usuario"].Value.ToString();

            SERVICESUsuario usuario = usuarios.FirstOrDefault(x => x.Login == login);

            if (usuario != null)
            {
                txtNombre.Text = usuario.Nombre;
                txtApellido.Text = usuario.Apellido;
            }
            else
            {
                txtNombre.Clear();
                txtApellido.Clear();
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