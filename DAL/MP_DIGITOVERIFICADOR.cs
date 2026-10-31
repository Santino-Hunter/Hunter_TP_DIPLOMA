using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_DIGITOVERIFICADOR
    {

        private ACCESO acceso;
        private readonly bool accesoCompartido;

        public MP_DIGITOVERIFICADOR()
        {
            accesoCompartido = false;
        }

        internal MP_DIGITOVERIFICADOR(ACCESO accesoExistente)
        {
            if (accesoExistente == null)
                throw new ArgumentNullException("accesoExistente");

            acceso = accesoExistente;
            accesoCompartido = true;
        }


        public bool ExisteTabla(string nombreTabla)
        {
            if (!accesoCompartido)
            {
                acceso = new ACCESO();
                acceso.Abrir();
            }

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));

                DataTable tabla = acceso.Leer(@"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @NombreTabla", parametros);

                return tabla.Rows.Count > 0;
            }
            finally
            {
                if (!accesoCompartido && acceso != null)
                {
                    acceso.Cerrar();
                }
            }
        }

        public bool ExisteColumna(string nombreTabla, string nombreColumna)
        {
            if (!accesoCompartido)
            {
                acceso = new ACCESO();
                acceso.Abrir();
            }

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));
                parametros.Add(acceso.CrearParametro("@NombreColumna", nombreColumna));

                DataTable tabla = acceso.Leer(@"SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @NombreTabla AND COLUMN_NAME = @NombreColumna", parametros);

                return tabla.Rows.Count > 0;
            }
            finally
            {
                if (!accesoCompartido && acceso != null)
                {
                    acceso.Cerrar();
                }
            }
        }

        public DataTable ObtenerTablaParaDV(string nombreTabla, List<string> columnas, List<string> columnasOrden)
        {
            if (!accesoCompartido)
            {
                acceso = new ACCESO();
                acceso.Abrir();
            }

            try
            {
                string columnasSelect = string.Join(", ", columnas.Select(x => "[" + x + "]"));
                string orden = string.Join(", ", columnasOrden.Select(x => "[" + x + "]"));

                string sql = "SELECT " + columnasSelect + ", [DVH] FROM [dbo].[" + nombreTabla + "] ORDER BY " + orden;

                return acceso.Leer(sql);
            }
            finally
            {
                if (!accesoCompartido && acceso != null)
                    acceso.Cerrar();
            }
        }

        public int ActualizarDVHFila(string nombreTabla, List<string> columnasPK, DataRow fila, long dvh)
        {
            if (!accesoCompartido)
            {
                acceso = new ACCESO();
                acceso.Abrir();
            }

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@DVH", SqlDbType.BigInt) { Value = dvh });

                List<string> condiciones = new List<string>();

                for (int i = 0; i < columnasPK.Count; i++)
                {
                    string parametro = "@PK" + i;
                    string columna = columnasPK[i];

                    condiciones.Add("[" + columna + "] = " + parametro);
                    parametros.Add(new SqlParameter(parametro, fila[columna]));
                }

                string sql = "UPDATE [dbo].[" + nombreTabla + "] SET [DVH] = @DVH WHERE " + string.Join(" AND ", condiciones);

                return acceso.Escribir(sql, parametros);
            }
            finally
            {
                if (!accesoCompartido && acceso != null)
                    acceso.Cerrar();
            }
        }

        public DataTable ObtenerDigitosGuardados(string nombreTabla)
        {
            if (!accesoCompartido)
            {
                acceso = new ACCESO();
                acceso.Abrir();
            }
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));

                return acceso.Leer(@"SELECT NombreTabla, NombreColumna, Tipo, Valor
                             FROM DigitoVerificador
                             WHERE NombreTabla = @NombreTabla", parametros);
            }
            finally
            {
                if (!accesoCompartido && acceso != null)
                    acceso.Cerrar();
            }
        }

        public int EliminarDigitosTabla(string nombreTabla)
        {
            if (!accesoCompartido)
            {
                acceso = new ACCESO();
                acceso.Abrir();
            }

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));

                return acceso.Escribir(@"DELETE FROM DigitoVerificador
                                 WHERE NombreTabla = @NombreTabla", parametros);
            }
            finally
            {
                if (!accesoCompartido && acceso != null)
                    acceso.Cerrar();
            }
        }

        public int GuardarDigito(string nombreTabla, string nombreColumna, string tipo, long valor)
        {
            if (!accesoCompartido)
            {
                acceso = new ACCESO();
                acceso.Abrir();
            }

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));
                parametros.Add(acceso.CrearParametro("@NombreColumna", nombreColumna));
                parametros.Add(acceso.CrearParametro("@Tipo", tipo));
                parametros.Add(new SqlParameter("@Valor", SqlDbType.BigInt) { Value = valor });

                return acceso.Escribir(@"INSERT INTO DigitoVerificador
                                (NombreTabla, NombreColumna, Tipo, Valor)
                                VALUES
                                (@NombreTabla, @NombreColumna, @Tipo, @Valor)", parametros);
            }
            finally
            {
                if (!accesoCompartido && acceso != null)
                    acceso.Cerrar();
            }
        }

    }
}
