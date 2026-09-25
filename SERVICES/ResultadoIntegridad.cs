using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class ResultadoIntegridad
    {
        public bool Correcto { get; set; }
        public bool InfraestructuraCreada { get; set; }
        public bool RequiereInicializacion { get; set; }
        public string Mensaje { get; set; }
        public List<DetalleInconsistencia> Detalles { get; set; }

        public ResultadoIntegridad()
        {
            Correcto = true;
            InfraestructuraCreada = true;
            RequiereInicializacion = false;
            Mensaje = string.Empty;
            Detalles = new List<DetalleInconsistencia>();
        }

        public string ObtenerDetalleTexto()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(Mensaje))
                sb.AppendLine(Mensaje);

            foreach (DetalleInconsistencia detalle in Detalles)
                sb.AppendLine(detalle.ToString());

            return sb.ToString();
        }
    }
}
