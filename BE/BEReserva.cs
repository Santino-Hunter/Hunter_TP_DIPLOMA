using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEReserva
    {
        public int NumeroReserva { get; set; }

        public BECliente Cliente { get; set; }

        public BEMesa Mesa { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public int CantidadComensales { get; set; }

        public string Observaciones { get; set; }

        public string Estado { get; set; }

        public void ActualizarDatos(DateTime fecha,TimeSpan horaInicio,BEMesa mesa)
        {
            Fecha = fecha;
            HoraInicio = horaInicio;
            Mesa = mesa;
        }

        public void CambiarEstado(string nuevoEstado)
        {
            Estado = nuevoEstado;
        }

    }
}
