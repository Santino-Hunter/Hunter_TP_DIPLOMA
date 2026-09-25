using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLMesa
    {
        private readonly MP_MESA mpMesa = new MP_MESA();
        private readonly MP_RESERVA mpReserva = new MP_RESERVA();

        private const int DURACION_RESERVA_MINUTOS = 120;

        public List<BEMesa> ConsultarDisponibilidad(DateTime fecha, TimeSpan horaInicio, int cantidadComensales)
        {
            return ConsultarDisponibilidadInterna(fecha, horaInicio, cantidadComensales, null);
        }

        public List<BEMesa> ConsultarDisponibilidadParaModificacion(DateTime fecha, TimeSpan horaInicio, int cantidadComensales, int numeroReserva)
        {
            if (numeroReserva <= 0)
                throw new Exception("La reserva seleccionada no es válida.");

            return ConsultarDisponibilidadInterna(fecha, horaInicio, cantidadComensales, numeroReserva);
        }

        private List<BEMesa> ConsultarDisponibilidadInterna(DateTime fecha, TimeSpan horaInicio, int cantidadComensales, int? numeroReservaExcluir)
        {
            if (cantidadComensales <= 0)
                throw new Exception("La cantidad de comensales debe ser mayor a cero.");

            List<BEMesa> mesas = mpMesa.ListarMesas();
            List<BEReserva> reservas = mpReserva.ListarReservasPorFecha(fecha.Date);

            if (numeroReservaExcluir.HasValue)
                reservas = reservas.Where(r => r.NumeroReserva != numeroReservaExcluir.Value).ToList();

            List<BEMesa> mesasDisponibles = DeterminarMesasDisponibles(mesas, reservas, horaInicio);

            return FiltrarPorCapacidad(mesasDisponibles, cantidadComensales);
        }

        private List<BEMesa> DeterminarMesasDisponibles(List<BEMesa> mesas, List<BEReserva> reservas, TimeSpan horaInicio)
        {
            TimeSpan nuevaHoraFin = horaInicio.Add(TimeSpan.FromMinutes(DURACION_RESERVA_MINUTOS));

            List<BEReserva> reservasActivas = reservas
                .Where(r => r != null &&
                            r.Mesa != null &&
                            string.Equals(r.Estado, "Reservada", StringComparison.OrdinalIgnoreCase))
                .ToList();

            List<BEMesa> disponibles = new List<BEMesa>();

            foreach (BEMesa mesa in mesas)
            {
                bool tieneSuperposicion = false;

                foreach (BEReserva reserva in reservasActivas)
                {
                    if (reserva.Mesa.NumeroMesa != mesa.NumeroMesa)
                        continue;

                    TimeSpan horaFinExistente = reserva.HoraInicio.Add(TimeSpan.FromMinutes(DURACION_RESERVA_MINUTOS));

                    bool seSuperponen = reserva.HoraInicio < nuevaHoraFin && horaFinExistente > horaInicio;

                    if (seSuperponen)
                    {
                        tieneSuperposicion = true;
                        break;
                    }
                }

                if (!tieneSuperposicion)
                    disponibles.Add(mesa);
            }

            return disponibles;
        }

        private List<BEMesa> FiltrarPorCapacidad(List<BEMesa> mesasDisponibles, int cantidadComensales)
        {
            return mesasDisponibles
                .Where(m => m.Capacidad >= cantidadComensales)
                .OrderBy(m => m.NumeroMesa)
                .ToList();
        }
    }
}
