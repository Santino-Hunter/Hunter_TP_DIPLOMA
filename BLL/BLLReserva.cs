using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLReserva
    {
        private readonly MP_RESERVA mp = new MP_RESERVA();

        private const string ESTADO_RESERVADA = "Reservada";
        private const string ESTADO_CANCELADA = "Cancelada";
        private const string ESTADO_CUMPLIDA = "Cumplida";
        private const string ESTADO_AUSENTE = "Ausente";

        public bool RegistrarReserva(BEReserva reserva)
        {
            if (reserva != null)
                reserva.Estado = ESTADO_RESERVADA;

            ValidarReserva(reserva);

            return mp.Guardar(reserva) > 0;
        }

        public List<BEReserva> ListarReservasReservadas()
        {
            return mp.ListarPorEstado(ESTADO_RESERVADA);
        }

        public BEReserva BuscarReserva(int numeroReserva)
        {
            if (numeroReserva <= 0)
                throw new Exception("La reserva seleccionada no es válida.");

            BEReserva reserva = mp.BuscarPorId(numeroReserva);

            if (reserva == null)
                throw new Exception("La reserva no existe.");

            return reserva;
        }

        public bool ModificarReserva(BEReserva reserva)
        {
            if (reserva == null || reserva.NumeroReserva <= 0)
                throw new Exception("Debe seleccionar una reserva válida.");

            BEReserva existente = mp.BuscarPorId(reserva.NumeroReserva);

            if (existente == null)
                throw new Exception("La reserva no existe.");

            if (!string.Equals(existente.Estado, ESTADO_RESERVADA, StringComparison.OrdinalIgnoreCase))
                throw new Exception("Solo pueden modificarse reservas en estado Reservada.");

            reserva.Estado = ESTADO_RESERVADA;

            ValidarReserva(reserva);

            return mp.Modificar(reserva) > 0;
        }

        public bool CancelarReserva(BEReserva reserva)
        {
            BEReserva existente = ValidarCancelacion(reserva);

            existente.CambiarEstado(ESTADO_CANCELADA);

            return mp.Modificar(existente) > 0;
        }

        public List<BEReserva> ConsultarReservas(DateTime? fecha, int? numeroMesa, string estado)
        {
            estado = string.IsNullOrWhiteSpace(estado) ? null : estado.Trim();

            ValidarCriterios(fecha, numeroMesa, estado);

            return mp.BuscarPorCriterios(fecha, numeroMesa, estado);
        }

        public List<BEReserva> ListarReservasProgramadas()
        {
            DateTime ahora = DateTime.Now;

            return mp.ListarProgramadas(ESTADO_RESERVADA, ahora.Date, ahora.TimeOfDay);
        }

        public bool RegistrarAsistencia(BEReserva reserva, bool asistio)
        {
            BEReserva existente = ValidarAsistencia(reserva);

            existente.CambiarEstado(asistio ? ESTADO_CUMPLIDA : ESTADO_AUSENTE);

            return mp.Modificar(existente) > 0;
        }

        private bool ValidarReserva(BEReserva reserva)
        {
            if (reserva == null)
                throw new Exception("La reserva no puede ser nula.");

            if (reserva.Cliente == null || reserva.Cliente.IdCliente <= 0)
                throw new Exception("Debe seleccionar un cliente válido.");

            if (reserva.Mesa == null || reserva.Mesa.NumeroMesa <= 0)
                throw new Exception("Debe seleccionar una mesa válida.");

            if (reserva.CantidadComensales <= 0)
                throw new Exception("La cantidad de comensales debe ser mayor a cero.");

            if (reserva.Mesa.Capacidad < reserva.CantidadComensales)
                throw new Exception("La mesa seleccionada no posee capacidad suficiente.");

            if (string.IsNullOrWhiteSpace(reserva.Estado))
                throw new Exception("La reserva debe poseer un estado.");

            reserva.Observaciones = (reserva.Observaciones ?? "").Trim();

            return true;
        }

        private BEReserva ValidarCancelacion(BEReserva reserva)
        {
            if (reserva == null || reserva.NumeroReserva <= 0)
                throw new Exception("Debe seleccionar una reserva.");

            BEReserva existente = mp.BuscarPorId(reserva.NumeroReserva);

            if (existente == null)
                throw new Exception("La reserva no existe.");

            if (!string.Equals(existente.Estado, ESTADO_RESERVADA, StringComparison.OrdinalIgnoreCase))
                throw new Exception("Solo puede cancelarse una reserva en estado Reservada.");

            return existente;
        }

        private bool ValidarCriterios(DateTime? fecha, int? numeroMesa, string estado)
        {
            if (!fecha.HasValue && !numeroMesa.HasValue && string.IsNullOrWhiteSpace(estado))
                throw new Exception("Debe seleccionar al menos un criterio de búsqueda.");

            if (numeroMesa.HasValue && numeroMesa.Value <= 0)
                throw new Exception("El número de mesa no es válido.");

            return true;
        }

        private BEReserva ValidarAsistencia(BEReserva reserva)
        {
            if (reserva == null || reserva.NumeroReserva <= 0)
                throw new Exception("Debe seleccionar una reserva.");

            BEReserva existente = mp.BuscarPorId(reserva.NumeroReserva);

            if (existente == null)
                throw new Exception("La reserva no existe.");

            if (!string.Equals(existente.Estado, ESTADO_RESERVADA, StringComparison.OrdinalIgnoreCase))
                throw new Exception("La reserva ya no se encuentra en estado Reservada.");

            DateTime fechaHoraReserva = existente.Fecha.Date.Add(existente.HoraInicio);

            if (DateTime.Now < fechaHoraReserva)
                throw new Exception("Todavía no llegó la fecha y horario de la reserva.");

            return existente;
        }
    }
}
