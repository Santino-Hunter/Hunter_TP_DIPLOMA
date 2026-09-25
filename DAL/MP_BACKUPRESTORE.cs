using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MP_BACKUPRESTORE
    {
        private string ObtenerNombreBase()
        {
            return DetectorServidorSQL.ObtenerNombreBase();
        }

        private string ObtenerConexionMaster()
        {
            return DetectorServidorSQL.ObtenerCadenaMaster();
        }

        public void RealizarRestore(string rutaBackup)
        {
            string nombreBase = ObtenerNombreBase();

            using (SqlConnection conexion = new SqlConnection(ObtenerConexionMaster()))
            {
                conexion.Open();

                try
                {
                    Ejecutar(conexion,
                        "ALTER DATABASE [" + nombreBase + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;",
                        null);

                    using (SqlCommand cmdRestore = new SqlCommand(
                        "RESTORE DATABASE [" + nombreBase + "] FROM DISK = @RutaBackup WITH REPLACE;",
                        conexion))
                    {
                        cmdRestore.CommandType = CommandType.Text;
                        cmdRestore.CommandTimeout = 0;
                        cmdRestore.Parameters.Add("@RutaBackup", SqlDbType.NVarChar, 500).Value = rutaBackup;
                        cmdRestore.ExecuteNonQuery();
                    }

                    Ejecutar(conexion,
                        "ALTER DATABASE [" + nombreBase + "] SET MULTI_USER;",
                        null);
                }
                catch
                {
                    try
                    {
                        Ejecutar(conexion,
                            "ALTER DATABASE [" + nombreBase + "] SET MULTI_USER;",
                            null);
                    }
                    catch { }

                    throw;
                }
            }
            SqlConnection.ClearAllPools();

        }

        public void RealizarBackup(string rutaBackup)
        {
            string nombreBase = ObtenerNombreBase();

            using (SqlConnection conexion = new SqlConnection(ObtenerConexionMaster()))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "BACKUP DATABASE [" + nombreBase + "] TO DISK = @RutaBackup WITH INIT;",
                    conexion))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@RutaBackup", SqlDbType.NVarChar, 500).Value = rutaBackup;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Ejecutar(SqlConnection conexion, string sql, SqlParameter[] parametros)
        {
            using (SqlCommand cmd = new SqlCommand(sql, conexion))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;

                if (parametros != null)
                    cmd.Parameters.AddRange(parametros);

                cmd.ExecuteNonQuery();
            }
        }
    }
}