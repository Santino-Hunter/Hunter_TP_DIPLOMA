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

        public bool ExisteTabla(string nombreTabla)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));

            DataTable tabla = acceso.Leer(@"SELECT 1
                                            FROM INFORMATION_SCHEMA.TABLES
                                            WHERE TABLE_NAME = @NombreTabla", parametros);

            acceso.Cerrar();
            return tabla.Rows.Count > 0;
        }

        public bool ExisteColumna(string nombreTabla, string nombreColumna)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));
            parametros.Add(acceso.CrearParametro("@NombreColumna", nombreColumna));

            DataTable tabla = acceso.Leer(@"SELECT 1
                                            FROM INFORMATION_SCHEMA.COLUMNS
                                            WHERE TABLE_NAME = @NombreTabla
                                            AND COLUMN_NAME = @NombreColumna", parametros);

            acceso.Cerrar();
            return tabla.Rows.Count > 0;
        }

        public DataTable ObtenerTablaParaDV(string nombreTabla, List<string> columnas, List<string> columnasOrden)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            string columnasSelect = string.Join(", ", columnas.Select(x => "[" + x + "]"));
            string orden = string.Join(", ", columnasOrden.Select(x => "[" + x + "]"));

            string sql = "SELECT " + columnasSelect + ", [DVH] FROM [dbo].[" + nombreTabla + "] ORDER BY " + orden;

            DataTable tabla = acceso.Leer(sql);

            acceso.Cerrar();
            return tabla;
        }

        public int ActualizarDVHFila(string nombreTabla, List<string> columnasPK, DataRow fila, long dvh)
        {
            acceso = new ACCESO();
            acceso.Abrir();

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

            int res = acceso.Escribir(sql, parametros);

            acceso.Cerrar();
            return res;
        }

        public DataTable ObtenerDigitosGuardados(string nombreTabla)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));

            DataTable tabla = acceso.Leer(@"SELECT NombreTabla, NombreColumna, Tipo, Valor
                                            FROM DigitoVerificador
                                            WHERE NombreTabla = @NombreTabla", parametros);

            acceso.Cerrar();
            return tabla;
        }

        public int EliminarDigitosTabla(string nombreTabla)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));

            int res = acceso.Escribir(@"DELETE FROM DigitoVerificador
                                        WHERE NombreTabla = @NombreTabla", parametros);

            acceso.Cerrar();
            return res;
        }

        public int GuardarDigito(string nombreTabla, string nombreColumna, string tipo, long valor)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));
            parametros.Add(acceso.CrearParametro("@NombreColumna", nombreColumna));
            parametros.Add(acceso.CrearParametro("@Tipo", tipo));
            parametros.Add(new SqlParameter("@Valor", SqlDbType.BigInt) { Value = valor });

            int res = acceso.Escribir(@"INSERT INTO DigitoVerificador
                                        (NombreTabla, NombreColumna, Tipo, Valor)
                                        VALUES
                                        (@NombreTabla, @NombreColumna, @Tipo, @Valor)", parametros);

            acceso.Cerrar();
            return res;
        }

        public DataTable ObtenerDetallesGuardados(string nombreTabla)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));

            DataTable tabla = acceso.Leer(@"SELECT NombreTabla, ClaveFila, DVH
                                    FROM DigitoVerificadorDetalle
                                    WHERE NombreTabla = @NombreTabla", parametros);

            acceso.Cerrar();
            return tabla;
        }

        public int EliminarDetallesTabla(string nombreTabla)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));

            int res = acceso.Escribir(@"DELETE FROM DigitoVerificadorDetalle
                                WHERE NombreTabla = @NombreTabla", parametros);

            acceso.Cerrar();
            return res;
        }

        public int GuardarDetalleFila(string nombreTabla, string claveFila, long dvh)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@NombreTabla", nombreTabla));
            parametros.Add(acceso.CrearParametro("@ClaveFila", claveFila));
            parametros.Add(new SqlParameter("@DVH", SqlDbType.BigInt) { Value = dvh });

            int res = acceso.Escribir(@"INSERT INTO DigitoVerificadorDetalle
                                (NombreTabla, ClaveFila, DVH)
                                VALUES
                                (@NombreTabla, @ClaveFila, @DVH)", parametros);

            acceso.Cerrar();
            return res;
        }

    }
}
