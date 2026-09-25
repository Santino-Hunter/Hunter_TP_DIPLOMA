using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public static class DetectorServidorSQL
    {
        private const string NombreCadena = "CH_Restaurante";
        private const string NombreBase = "CH_Restaurante";

        private static string _cadenaBaseValida;

        public static string ObtenerCadenaConexion()
        {
            if (!string.IsNullOrEmpty(_cadenaBaseValida))
                return _cadenaBaseValida;

            string configurada = ObtenerCadenaConfigurada();

            if (!string.IsNullOrWhiteSpace(configurada) && ProbarConexion(configurada))
            {
                _cadenaBaseValida = configurada;
                return _cadenaBaseValida;
            }

            string[] servidores =
            {
                @".\SQLEXPRESS",
                ".",
                "localhost",
                "(local)",
                @"(localdb)\MSSQLLocalDB"
            };

            foreach (string servidor in servidores)
            {
                string cadena = CrearCadena(servidor, NombreBase);

                if (ProbarConexion(cadena))
                {
                    _cadenaBaseValida = cadena;
                    return _cadenaBaseValida;
                }
            }

            throw new Exception(
                "No se encontró una instancia de SQL Server con la base 'CH_Restaurante'. " +
                "Verifique que SQL Server esté iniciado, que la base exista y que el usuario de Windows tenga permisos.");
        }

        public static string ObtenerCadenaMaster()
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder(ObtenerCadenaConexion());

            builder.InitialCatalog = "master";
            return builder.ConnectionString;
        }

        public static string ObtenerNombreBase()
        {
            return NombreBase;
        }

        private static string ObtenerCadenaConfigurada()
        {
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[NombreCadena];

            if (settings == null)
                return null;

            return settings.ConnectionString;
        }

        private static string CrearCadena(string servidor, string baseDatos)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = servidor;
            builder.InitialCatalog = baseDatos;
            builder.IntegratedSecurity = true;
            builder.ConnectTimeout = 5;

            return builder.ConnectionString;
        }

        private static bool ProbarConexion(string cadena)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}