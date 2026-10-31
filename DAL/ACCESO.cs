using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    internal class ACCESO
    {
        private SqlConnection conexion;
        private SqlTransaction transaccion;
        private readonly string StrConexion;

        public ACCESO()
        {
            StrConexion = DetectorServidorSQL.ObtenerCadenaConexion();
        }

        public void Abrir()
        {
            conexion = new SqlConnection(StrConexion);
            conexion.Open();
        }

        public void IniciarTransaccion()
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión debe estar abierta para iniciar una transacción.");

            if (transaccion != null)
                throw new InvalidOperationException("Ya existe una transacción activa.");

            transaccion = conexion.BeginTransaction();
        }

        public void ConfirmarTransaccion()
        {
            if (transaccion == null)
                throw new InvalidOperationException("No existe una transacción activa.");

            transaccion.Commit();
            transaccion.Dispose();
            transaccion = null;
        }

        public void RevertirTransaccion()
        {
            if (transaccion == null)
                return;

            try
            {
                transaccion.Rollback();
            }
            finally
            {
                transaccion.Dispose();
                transaccion = null;
            }
        }


        public void Cerrar()
        {
            try
            {
                if (transaccion != null)
                {
                    RevertirTransaccion();
                }
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                    conexion.Dispose();
                    conexion = null;
                }
            }
        }



        private SqlCommand CrearComando(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = new SqlCommand(sql, conexion);

            cmd.CommandType = CommandType.Text;

            if (transaccion != null)
            {
                cmd.Transaction = transaccion;
            }

            if (parametros != null)
            {
                foreach (SqlParameter p in parametros)
                {
                    cmd.Parameters.Add(p);
                }
            }

            return cmd;
        }

        public int Escribir(string sql, List<SqlParameter> parametros = null)
        {
            using (SqlCommand cmd = CrearComando(sql, parametros))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public DataTable Leer(string sql, List<SqlParameter> parametros = null)
        {
            using (SqlCommand cmd = CrearComando(sql, parametros))
            using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                return dt;
            }
        }

        public SqlParameter CrearParametro(string nombre, int valor)
        {
            return new SqlParameter(nombre, SqlDbType.Int) { Value = valor };
        }

        public SqlParameter CrearParametro(string nombre, string valor)
        {
            return new SqlParameter(nombre, SqlDbType.NVarChar) { Value = valor ?? "" };
        }

        public SqlParameter CrearParametro(string nombre, bool valor)
        {
            return new SqlParameter(nombre, SqlDbType.Bit) { Value = valor };
        }

        public SqlParameter CrearParametro(string nombre, DateTime valor)
        {
            return new SqlParameter(nombre, SqlDbType.DateTime) { Value = valor };
        }

        public SqlParameter CrearParametro(string nombre, TimeSpan valor)
        {
            return new SqlParameter(nombre, SqlDbType.Time) { Value = valor };
        }
    }
}