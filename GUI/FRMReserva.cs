using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class FRMReserva : Form
    {
        private readonly BLLCliente bllCliente = new BLLCliente();
        private readonly BLLMesa bllMesa = new BLLMesa();
        private readonly BLLReserva bllReserva = new BLLReserva();

        private BECliente clienteSeleccionado;
        private BEMesa mesaSeleccionada;
        private BEReserva reservaSeleccionada;
        private BEReserva reservaConsultaSeleccionada;
        private BEReserva reservaAsistenciaSeleccionada;

        private List<BEMesa> mesasDisponiblesActuales = new List<BEMesa>();
        private List<BEReserva> reservasConsultaActuales = new List<BEReserva>();
        private List<BEReserva> reservasProgramadasActuales = new List<BEReserva>();

        private bool cargandoCliente;
        private bool cargandoReserva;

        public FRMReserva()
        {
            InitializeComponent();
            ConfigurarGrillas();
        }

        private void FRMReserva_Load(object sender, EventArgs e)
        {
            cmbEstadoConsulta.Items.Clear();
            cmbEstadoConsulta.Items.AddRange(new object[] { "Reservada", "Cancelada", "Cumplida", "Ausente" });
            cmbEstadoConsulta.SelectedIndex = 0;

            dtpFecha.MinDate = DateTime.Today;
            dtpFecha.Value = DateTime.Today;
            dtpHora.Value = DateTime.Now;
            nudComensales.Value = 2;

            chkFechaConsulta.Checked = false;
            chkMesaConsulta.Checked = false;
            chkEstadoConsulta.Checked = true;
            ActualizarEstadoFiltros();

            tabReservas.Appearance = TabAppearance.Normal;
            tabReservas.SizeMode = TabSizeMode.Normal;

            tabReservas.ItemSize = new System.Drawing.Size(150, 25);
            tabReservas.SizeMode = TabSizeMode.Fixed;
            tabReservas.Multiline = false;

            lblSubtituloGestion.Visible = true;
            lblSubtituloConsulta.Visible = true;
            lblSubtituloAsistencia.Visible = true;

            NuevaReserva();
        }

        private void ConfigurarGrillas()
        {
            ConfigurarGrilla(dgvMesasDisponibles);
            ConfigurarGrilla(dgvConsultaReservas);
            ConfigurarGrilla(dgvReservasProgramadas);
        }

        private void ConfigurarGrilla(DataGridView grilla)
        {
            grilla.ReadOnly = true;
            grilla.MultiSelect = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.AllowUserToResizeRows = false;
            grilla.RowHeadersVisible = false;
            grilla.AutoGenerateColumns = true;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                BECliente cliente = bllCliente.BuscarCliente(txtTelefono.Text, txtCorreo.Text);

                if (cliente == null)
                {
                    LimpiarClienteSeleccionado();
                    lblClienteSeleccionado.Text = "Cliente no encontrado";
                    MessageBox.Show(
                        "El cliente no se encuentra registrado. Puede registrarlo con los datos ingresados.",
                        "Cliente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                SeleccionarCliente(cliente);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                BECliente cliente = new BECliente
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Telefono = txtTelefono.Text,
                    CorreoElectronico = txtCorreo.Text
                };

                if (bllCliente.RegistrarCliente(cliente))
                {
                    SeleccionarCliente(cliente);
                    MessageBox.Show(
                        "Cliente registrado correctamente.",
                        "Cliente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void SeleccionarCliente(BECliente cliente)
        {
            cargandoCliente = true;

            clienteSeleccionado = cliente;
            txtTelefono.Text = cliente.Telefono;
            txtCorreo.Text = cliente.CorreoElectronico;
            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;

            txtNombre.ReadOnly = true;
            txtApellido.ReadOnly = true;
            btnRegistrarCliente.Enabled = false;

            lblClienteSeleccionado.Text = "Cliente seleccionado: " + cliente.Nombre + " " + cliente.Apellido;

            cargandoCliente = false;
        }

        private void DatosBusquedaCliente_TextChanged(object sender, EventArgs e)
        {
            if (cargandoCliente || clienteSeleccionado == null)
                return;

            bool cambioTelefono = !string.Equals(
                txtTelefono.Text.Trim(),
                clienteSeleccionado.Telefono ?? "",
                StringComparison.OrdinalIgnoreCase);

            bool cambioCorreo = !string.Equals(
                txtCorreo.Text.Trim(),
                clienteSeleccionado.CorreoElectronico ?? "",
                StringComparison.OrdinalIgnoreCase);

            if (cambioTelefono || cambioCorreo)
                LimpiarClienteSeleccionado();
        }

        private void LimpiarClienteSeleccionado()
        {
            clienteSeleccionado = null;

            txtNombre.ReadOnly = false;
            txtApellido.ReadOnly = false;
            btnRegistrarCliente.Enabled = true;

            txtNombre.Clear();
            txtApellido.Clear();

            lblClienteSeleccionado.Text = "Cliente no seleccionado";
        }

        private void btnConsultarDisponibilidad_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarMesaSeleccionada();

                if (reservaSeleccionada != null)
                {
                    mesasDisponiblesActuales = bllMesa.ConsultarDisponibilidadParaModificacion(
                        dtpFecha.Value.Date,
                        dtpHora.Value.TimeOfDay,
                        Convert.ToInt32(nudComensales.Value),
                        reservaSeleccionada.NumeroReserva);
                }
                else
                {
                    mesasDisponiblesActuales = bllMesa.ConsultarDisponibilidad(
                        dtpFecha.Value.Date,
                        dtpHora.Value.TimeOfDay,
                        Convert.ToInt32(nudComensales.Value));
                }

                CargarGrillaMesas();

                if (mesasDisponiblesActuales.Count == 0)
                {
                    MessageBox.Show(
                        "No hay mesas disponibles con capacidad suficiente para los datos seleccionados.",
                        "Disponibilidad",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void CargarGrillaMesas()
        {
            dgvMesasDisponibles.DataSource = null;
            dgvMesasDisponibles.DataSource = mesasDisponiblesActuales.Select(x => new
            {
                NumeroMesa = x.NumeroMesa,
                Capacidad = x.Capacidad,
                Estado = "Disponible"
            }).ToList();

            if (dgvMesasDisponibles.Columns["NumeroMesa"] != null)
                dgvMesasDisponibles.Columns["NumeroMesa"].HeaderText = "Mesa";

            dgvMesasDisponibles.ClearSelection();
        }

        private void btnSeleccionarMesa_Click(object sender, EventArgs e)
        {
            SeleccionarMesaDesdeGrilla();
        }

        private void dgvMesasDisponibles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SeleccionarMesaDesdeGrilla();
        }

        private void SeleccionarMesaDesdeGrilla()
        {
            if (dgvMesasDisponibles.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione una mesa disponible.",
                    "Mesa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int numeroMesa = Convert.ToInt32(
                dgvMesasDisponibles.CurrentRow.Cells["NumeroMesa"].Value);

            mesaSeleccionada = mesasDisponiblesActuales
                .FirstOrDefault(x => x.NumeroMesa == numeroMesa);

            if (mesaSeleccionada == null)
                return;

            lblMesaSeleccionada.Text =
                "Mesa seleccionada: " +
                mesaSeleccionada.NumeroMesa +
                " - Capacidad " +
                mesaSeleccionada.Capacidad;
        }

        private void btnLimpiarMesa_Click(object sender, EventArgs e)
        {
            LimpiarMesaSeleccionada();
        }

        private void LimpiarMesaSeleccionada()
        {
            mesaSeleccionada = null;
            lblMesaSeleccionada.Text = "Mesa seleccionada: Ninguna";
            dgvMesasDisponibles.ClearSelection();
        }

        private void DatosReserva_ValueChanged(object sender, EventArgs e)
        {
            if (cargandoReserva)
                return;

            if (mesaSeleccionada != null)
                LimpiarMesaSeleccionada();
        }

        private BEReserva ConstruirReservaDesdeFormulario()
        {
            if (clienteSeleccionado == null)
                throw new Exception("Debe buscar o registrar un cliente antes de continuar.");

            if (mesaSeleccionada == null)
                throw new Exception("Debe consultar disponibilidad y seleccionar una mesa.");

            return new BEReserva
            {
                NumeroReserva = reservaSeleccionada != null ? reservaSeleccionada.NumeroReserva : 0,
                Cliente = clienteSeleccionado,
                Mesa = mesaSeleccionada,
                Fecha = dtpFecha.Value.Date,
                HoraInicio = dtpHora.Value.TimeOfDay,
                CantidadComensales = Convert.ToInt32(nudComensales.Value),
                Observaciones = txtObservaciones.Text,
                Estado = "Reservada"
            };
        }

        private void btnRegistrarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                if (reservaSeleccionada != null)
                    throw new Exception("Actualmente está modificando una reserva. Use 'Modificar reserva' o presione 'Nueva reserva'.");

                BEReserva reserva = ConstruirReservaDesdeFormulario();

                if (MessageBox.Show(
                    "¿Confirma el registro de la reserva?",
                    "Registrar reserva",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                if (bllReserva.RegistrarReserva(reserva))
                {
                    MessageBox.Show(
                        "Reserva registrada correctamente. Número de reserva: " + reserva.NumeroReserva,
                        "Reserva",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    NuevaReserva();
                    RefrescarConsultaSiCorresponde();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnNuevaReserva_Click(object sender, EventArgs e)
        {
            NuevaReserva();
        }

        private void NuevaReserva()
        {
            cargandoCliente = true;
            cargandoReserva = true;

            reservaSeleccionada = null;
            clienteSeleccionado = null;
            mesaSeleccionada = null;
            mesasDisponiblesActuales.Clear();

            txtTelefono.Clear();
            txtCorreo.Clear();
            txtNombre.Clear();
            txtApellido.Clear();

            txtNombre.ReadOnly = false;
            txtApellido.ReadOnly = false;
            btnRegistrarCliente.Enabled = true;

            dtpFecha.Value = DateTime.Today;
            dtpHora.Value = DateTime.Now;
            nudComensales.Value = 2;
            txtObservaciones.Clear();

            dgvMesasDisponibles.DataSource = null;

            lblClienteSeleccionado.Text = "Cliente no seleccionado";
            lblMesaSeleccionada.Text = "Mesa seleccionada: Ninguna";
            lblModoReserva.Text = "Modo: Nueva reserva";

            btnRegistrarReserva.Enabled = true;
            btnModificarReserva.Enabled = false;
            btnCancelarReserva.Enabled = false;

            cargandoReserva = false;
            cargandoCliente = false;
        }

        private void btnModificarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                if (reservaSeleccionada == null)
                    throw new Exception("Seleccione una reserva desde la pestaña 'Consultar Reservas'.");

                BEReserva reserva = ConstruirReservaDesdeFormulario();

                if (MessageBox.Show(
                    "¿Confirma la modificación de la reserva N° " + reserva.NumeroReserva + "?",
                    "Modificar reserva",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                if (bllReserva.ModificarReserva(reserva))
                {
                    MessageBox.Show(
                        "Reserva modificada correctamente.",
                        "Reserva",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    NuevaReserva();
                    RefrescarConsultaSiCorresponde();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            if (reservaSeleccionada == null)
            {
                MessageBox.Show(
                    "Seleccione una reserva desde la pestaña 'Consultar Reservas'.",
                    "Cancelar reserva",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                tabReservas.SelectedTab = tabConsulta;
                return;
            }

            CancelarReserva(reservaSeleccionada);
        }

        private void CancelarReserva(BEReserva reserva)
        {
            try
            {
                if (reserva == null)
                    throw new Exception("Debe seleccionar una reserva.");

                if (MessageBox.Show(
                    "¿Confirma la cancelación de la reserva N° " + reserva.NumeroReserva + "?",
                    "Cancelar reserva",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                if (bllReserva.CancelarReserva(reserva))
                {
                    MessageBox.Show(
                        "Reserva cancelada correctamente.",
                        "Reserva",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    NuevaReserva();
                    RefrescarConsultaSiCorresponde();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnBuscarReservas_Click(object sender, EventArgs e)
        {
            ConsultarReservas();
        }

        private void ConsultarReservas()
        {
            try
            {
                DateTime? fecha = chkFechaConsulta.Checked
                    ? dtpFechaConsulta.Value.Date
                    : (DateTime?)null;

                int? numeroMesa = chkMesaConsulta.Checked
                    ? Convert.ToInt32(nudMesaConsulta.Value)
                    : (int?)null;

                string estado = chkEstadoConsulta.Checked
                    ? Convert.ToString(cmbEstadoConsulta.SelectedItem)
                    : null;

                reservasConsultaActuales = bllReserva.ConsultarReservas(
                    fecha,
                    numeroMesa,
                    estado);

                CargarGrillaConsulta();

                if (reservasConsultaActuales.Count == 0)
                    lblReservaConsultaSeleccionada.Text = "No se encontraron reservas con los criterios seleccionados.";
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void CargarGrillaConsulta()
        {
            dgvConsultaReservas.DataSource = null;
            dgvConsultaReservas.DataSource = reservasConsultaActuales.Select(r => new
            {
                NumeroReserva = r.NumeroReserva,
                Cliente = r.Cliente != null ? r.Cliente.Nombre + " " + r.Cliente.Apellido : "",
                NumeroMesa = r.Mesa != null ? r.Mesa.NumeroMesa : 0,
                Fecha = r.Fecha.ToString("dd/MM/yyyy"),
                Hora = r.HoraInicio.ToString(@"hh\:mm"),
                CantidadComensales = r.CantidadComensales,
                Estado = r.Estado
            }).ToList();

            if (dgvConsultaReservas.Columns["NumeroReserva"] != null)
                dgvConsultaReservas.Columns["NumeroReserva"].HeaderText = "Reserva";

            if (dgvConsultaReservas.Columns["NumeroMesa"] != null)
                dgvConsultaReservas.Columns["NumeroMesa"].HeaderText = "Mesa";

            if (dgvConsultaReservas.Columns["CantidadComensales"] != null)
                dgvConsultaReservas.Columns["CantidadComensales"].HeaderText = "Comensales";

            reservaConsultaSeleccionada = null;
            lblReservaConsultaSeleccionada.Text = "Seleccione una reserva de la grilla.";

            btnCargarParaModificar.Enabled = false;
            btnCancelarConsulta.Enabled = false;

            dgvConsultaReservas.ClearSelection();
        }

        private void dgvConsultaReservas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvConsultaReservas.CurrentRow == null ||
                dgvConsultaReservas.CurrentRow.Cells["NumeroReserva"].Value == null)
                return;

            int numeroReserva = Convert.ToInt32(
                dgvConsultaReservas.CurrentRow.Cells["NumeroReserva"].Value);

            reservaConsultaSeleccionada = reservasConsultaActuales
                .FirstOrDefault(r => r.NumeroReserva == numeroReserva);

            if (reservaConsultaSeleccionada == null)
                return;

            lblReservaConsultaSeleccionada.Text =
                "Reserva seleccionada: N° " +
                reservaConsultaSeleccionada.NumeroReserva +
                " - " +
                reservaConsultaSeleccionada.Estado;

            bool reservada = string.Equals(
                reservaConsultaSeleccionada.Estado,
                "Reservada",
                StringComparison.OrdinalIgnoreCase);

            btnCargarParaModificar.Enabled = reservada;
            btnCancelarConsulta.Enabled = reservada;
        }

        private void btnCargarParaModificar_Click(object sender, EventArgs e)
        {
            if (reservaConsultaSeleccionada == null)
            {
                MessageBox.Show(
                    "Seleccione una reserva en estado Reservada.",
                    "Modificar reserva",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            CargarReservaParaModificar(reservaConsultaSeleccionada);
        }

        private void CargarReservaParaModificar(BEReserva reserva)
        {
            cargandoReserva = true;

            reservaSeleccionada = bllReserva.BuscarReserva(reserva.NumeroReserva);

            SeleccionarCliente(reservaSeleccionada.Cliente);

            dtpFecha.Value = reservaSeleccionada.Fecha;
            dtpHora.Value = DateTime.Today.Add(reservaSeleccionada.HoraInicio);
            nudComensales.Value = reservaSeleccionada.CantidadComensales;
            txtObservaciones.Text = reservaSeleccionada.Observaciones;

            mesaSeleccionada = null;
            mesasDisponiblesActuales.Clear();
            dgvMesasDisponibles.DataSource = null;

            lblMesaSeleccionada.Text = "Mesa actual: " + reservaSeleccionada.Mesa.NumeroMesa;
            lblModoReserva.Text = "Modo: Modificando reserva N° " + reservaSeleccionada.NumeroReserva;

            btnRegistrarReserva.Enabled = false;
            btnModificarReserva.Enabled = true;
            btnCancelarReserva.Enabled = true;

            cargandoReserva = false;

            tabReservas.SelectedTab = tabGestion;
        }

        private void btnCancelarConsulta_Click(object sender, EventArgs e)
        {
            CancelarReserva(reservaConsultaSeleccionada);
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            chkFechaConsulta.Checked = false;
            chkMesaConsulta.Checked = false;
            chkEstadoConsulta.Checked = false;

            ActualizarEstadoFiltros();

            dgvConsultaReservas.DataSource = null;
            reservasConsultaActuales.Clear();
            reservaConsultaSeleccionada = null;

            lblReservaConsultaSeleccionada.Text = "Seleccione al menos un criterio de búsqueda.";

            btnCargarParaModificar.Enabled = false;
            btnCancelarConsulta.Enabled = false;
        }

        private void FiltroConsulta_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoFiltros();
        }

        private void ActualizarEstadoFiltros()
        {
            dtpFechaConsulta.Enabled = chkFechaConsulta.Checked;
            nudMesaConsulta.Enabled = chkMesaConsulta.Checked;
            cmbEstadoConsulta.Enabled = chkEstadoConsulta.Checked;
        }

        private void btnCargarProgramadas_Click(object sender, EventArgs e)
        {
            CargarReservasProgramadas();
        }

        private void CargarReservasProgramadas()
        {
            try
            {
                reservasProgramadasActuales = bllReserva.ListarReservasProgramadas();

                dgvReservasProgramadas.DataSource = null;
                dgvReservasProgramadas.DataSource = reservasProgramadasActuales.Select(r => new
                {
                    NumeroReserva = r.NumeroReserva,
                    Cliente = r.Cliente != null ? r.Cliente.Nombre + " " + r.Cliente.Apellido : "",
                    NumeroMesa = r.Mesa != null ? r.Mesa.NumeroMesa : 0,
                    Fecha = r.Fecha.ToString("dd/MM/yyyy"),
                    Hora = r.HoraInicio.ToString(@"hh\:mm"),
                    CantidadComensales = r.CantidadComensales
                }).ToList();

                if (dgvReservasProgramadas.Columns["NumeroReserva"] != null)
                    dgvReservasProgramadas.Columns["NumeroReserva"].HeaderText = "Reserva";

                if (dgvReservasProgramadas.Columns["NumeroMesa"] != null)
                    dgvReservasProgramadas.Columns["NumeroMesa"].HeaderText = "Mesa";

                if (dgvReservasProgramadas.Columns["CantidadComensales"] != null)
                    dgvReservasProgramadas.Columns["CantidadComensales"].HeaderText = "Comensales";

                reservaAsistenciaSeleccionada = null;
                LimpiarDetalleAsistencia();
                dgvReservasProgramadas.ClearSelection();

                if (reservasProgramadasActuales.Count == 0)
                {
                    MessageBox.Show(
                        "No hay reservas pendientes de asistencia para la fecha y horario actuales.",
                        "Asistencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void dgvReservasProgramadas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReservasProgramadas.CurrentRow == null ||
                dgvReservasProgramadas.CurrentRow.Cells["NumeroReserva"].Value == null)
                return;

            int numeroReserva = Convert.ToInt32(
                dgvReservasProgramadas.CurrentRow.Cells["NumeroReserva"].Value);

            reservaAsistenciaSeleccionada = reservasProgramadasActuales
                .FirstOrDefault(r => r.NumeroReserva == numeroReserva);

            MostrarDetalleAsistencia();
        }

        private void MostrarDetalleAsistencia()
        {
            if (reservaAsistenciaSeleccionada == null)
            {
                LimpiarDetalleAsistencia();
                return;
            }

            lblValorNumeroReserva.Text = reservaAsistenciaSeleccionada.NumeroReserva.ToString();

            lblValorCliente.Text = reservaAsistenciaSeleccionada.Cliente != null
                ? reservaAsistenciaSeleccionada.Cliente.Nombre + " " + reservaAsistenciaSeleccionada.Cliente.Apellido
                : "-";

            lblValorMesa.Text = reservaAsistenciaSeleccionada.Mesa != null
                ? reservaAsistenciaSeleccionada.Mesa.NumeroMesa.ToString()
                : "-";

            lblValorFecha.Text = reservaAsistenciaSeleccionada.Fecha.ToString("dd/MM/yyyy");
            lblValorHora.Text = reservaAsistenciaSeleccionada.HoraInicio.ToString(@"hh\:mm");
            lblValorComensales.Text = reservaAsistenciaSeleccionada.CantidadComensales.ToString();

            btnAsistio.Enabled = true;
            btnAusente.Enabled = true;
        }

        private void LimpiarDetalleAsistencia()
        {
            lblValorNumeroReserva.Text = "-";
            lblValorCliente.Text = "-";
            lblValorMesa.Text = "-";
            lblValorFecha.Text = "-";
            lblValorHora.Text = "-";
            lblValorComensales.Text = "-";

            btnAsistio.Enabled = false;
            btnAusente.Enabled = false;
        }

        private void btnAsistio_Click(object sender, EventArgs e)
        {
            RegistrarAsistencia(true);
        }

        private void btnAusente_Click(object sender, EventArgs e)
        {
            RegistrarAsistencia(false);
        }

        private void RegistrarAsistencia(bool asistio)
        {
            try
            {
                if (reservaAsistenciaSeleccionada == null)
                    throw new Exception("Seleccione una reserva programada.");

                string estado = asistio ? "Cumplida" : "Ausente";

                if (MessageBox.Show(
                    "¿Confirma registrar la reserva N° " +
                    reservaAsistenciaSeleccionada.NumeroReserva +
                    " como " +
                    estado +
                    "?",
                    "Registrar asistencia",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                if (bllReserva.RegistrarAsistencia(reservaAsistenciaSeleccionada, asistio))
                {
                    MessageBox.Show(
                        "Asistencia registrada correctamente.",
                        "Asistencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarReservasProgramadas();
                    RefrescarConsultaSiCorresponde();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void RefrescarConsultaSiCorresponde()
        {
            if (dgvConsultaReservas.DataSource != null &&
                (chkFechaConsulta.Checked ||
                 chkMesaConsulta.Checked ||
                 chkEstadoConsulta.Checked))
            {
                ConsultarReservas();
            }
        }
        private void MostrarError(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Gestión de Reservas",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
