using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class DetalleInconsistencia
    {
        public string Tabla { get; set; }
        public string Campo { get; set; }
        public string Tipo { get; set; }
        public string Operacion { get; set; }
        public long ValorGuardado { get; set; }
        public long ValorCalculado { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Operacion))
            {
                return string.Format("{0} - {1} - {2}. DVH guardado: {3}. DVH calculado: {4}",
                    Tabla, Campo, Operacion, ValorGuardado, ValorCalculado);
            }

            return string.Format("{0} - {1} - {2}. Guardado: {3}. Calculado: {4}",
                Tabla, Campo, Tipo, ValorGuardado, ValorCalculado);
        }

    }
}
