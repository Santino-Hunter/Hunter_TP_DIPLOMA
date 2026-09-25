using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class SERVICESBitacoraEvento
    {
        public int IdEvento { get; set; }
        public string Login { get; set; }
        public DateTime FechaHora { get; set; }
        public string Modulo { get; set; }
        public string Evento { get; set; }
        public int Criticidad { get; set; }

    }
}
