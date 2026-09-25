using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class SERVICESDigitoVerificador
    {
        public long CalcularDVH(DataRow fila, List<string> columnas)
        {
            long total = 0;

            for (int i = 0; i < columnas.Count; i++)
            {
                string columna = columnas[i];
                string valor = NormalizarValor(fila[columna]);
                int posicionAtributo = i + 1;

                total += CalcularValorCampo(valor, posicionAtributo, 1);
            }

            return total;
        }

        public long CalcularDVV(DataTable tabla, string columna, int posicionAtributo)
        {
            long total = 0;

            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                string valor = NormalizarValor(tabla.Rows[i][columna]);
                int posicionRegistro = i + 1;

                total += CalcularValorCampo(valor, posicionAtributo, posicionRegistro);
            }

            return total;
        }

        private long CalcularValorCampo(string valor, int posicionAtributo, int posicionRegistro)
        {
            if (valor == null)
                valor = string.Empty;

            long total = 0;

            for (int i = 0; i < valor.Length; i++)
            {
                int posicionCaracter = i + 1;
                int codigoCaracter = Convert.ToInt32(valor[i]);

                total += codigoCaracter * posicionCaracter * posicionAtributo * posicionRegistro;
            }

            return total;
        }

        private string NormalizarValor(object valor)
        {
            if (valor == null || valor == DBNull.Value)
                return string.Empty;

            if (valor is bool)
                return ((bool)valor) ? "1" : "0";

            return valor.ToString().Trim();
        }
    }
}
