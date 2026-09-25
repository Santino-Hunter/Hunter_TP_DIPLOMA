using SERVICES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class MP_USUARIO : MAPPER<SERVICESUsuario>
    {
        public override int Modificar(SERVICESUsuario usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", usuario.DNI));
            parametros.Add(acceso.CrearParametro("@Nombre", usuario.Nombre));
            parametros.Add(acceso.CrearParametro("@Apellido", usuario.Apellido));
            parametros.Add(acceso.CrearParametro("@Login", usuario.Login));
            parametros.Add(acceso.CrearParametro("@Rol", usuario.Rol.ID));
            parametros.Add(acceso.CrearParametro("@Mail", usuario.Mail));

            int res = acceso.Escribir(@"UPDATE Usuario
                SET Nombre = @Nombre,
                    Apellido = @Apellido,
                    Login = @Login,
                    Rol = @Rol,
                    Mail = @Mail
                WHERE DNI = @DNI", parametros);
            acceso.Cerrar();
            return res;
        }

        public override int Guardar(SERVICESUsuario usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", usuario.DNI));
            parametros.Add(acceso.CrearParametro("@Nombre", usuario.Nombre));
            parametros.Add(acceso.CrearParametro("@Apellido", usuario.Apellido));
            parametros.Add(acceso.CrearParametro("@Login", usuario.Login));
            parametros.Add(acceso.CrearParametro("@Contraseña", usuario.Contraseña));
            parametros.Add(acceso.CrearParametro("@Rol", usuario.Rol.ID));
            parametros.Add(acceso.CrearParametro("@Mail", usuario.Mail));
            parametros.Add(acceso.CrearParametro("@Bloqueado", usuario.Bloqueado));
            parametros.Add(acceso.CrearParametro("@Activo", usuario.Activo));
            parametros.Add(acceso.CrearParametro("@Contador", usuario.Contador));
            parametros.Add(acceso.CrearParametro("@Idioma", usuario.Idioma));
            
            int res = acceso.Escribir(@"INSERT INTO Usuario
                (DNI, Nombre, Apellido, Login, Contraseña, Rol, Mail, Bloqueado, Activo, Contador, Idioma)
                VALUES
                (@DNI, @Nombre, @Apellido, @Login, @Contraseña, @Rol, @Mail, @Bloqueado, @Activo, @Contador, @Idioma)", parametros);
            acceso.Cerrar();
            return res;
        }

        public override List<SERVICESUsuario> ObtenerTodos()
        {
            List<SERVICESUsuario> usuarios = new List<SERVICESUsuario>();
            acceso = new ACCESO();
            acceso.Abrir();
            DataTable tabla = acceso.Leer(@"SELECT U.DNI, U.Nombre, U.Apellido, U.Login,
                                           U.Contraseña, U.Mail, U.Bloqueado,
                                           U.Activo, U.Contador, ISNULL(U.Idioma, 'es') AS Idioma,
                                           P.ID, P.Nombre AS PerfilNombre, P.Descripcion
                                    FROM Usuario U
                                    INNER JOIN Perfil P ON U.Rol = P.ID");
            acceso.Cerrar();

            foreach (DataRow row in tabla.Rows)
            {
                usuarios.Add(MapearUsuario(row));
            }

            return usuarios;
        }

        public int Desactivar(SERVICESUsuario usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", usuario.DNI));
            int res = acceso.Escribir(@"UPDATE Usuario SET Activo = 0 WHERE DNI = @DNI", parametros);
            acceso.Cerrar();
            return res;
        }

        public int Activar(SERVICESUsuario usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", usuario.DNI));
            int res = acceso.Escribir(@"UPDATE Usuario SET Activo = 1 WHERE DNI = @DNI", parametros);
            acceso.Cerrar();
            return res;
        }

        public int Desbloquear(SERVICESUsuario usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", usuario.DNI));
            int res = acceso.Escribir(@"UPDATE Usuario
                SET Bloqueado = 0,
                    Contador = 0
                WHERE DNI = @DNI", parametros);
            acceso.Cerrar();
            return res;
        }

        private SERVICESUsuario MapearUsuario(DataRow row)
        {
            SERVICESUsuario u = new SERVICESUsuario();

            u.DNI = int.Parse(row["DNI"].ToString());
            u.Nombre = row["Nombre"].ToString();
            u.Apellido = row["Apellido"].ToString();
            u.Login = row["Login"].ToString();
            u.Contraseña = row["Contraseña"].ToString();
            u.Mail = row["Mail"].ToString();
            u.Bloqueado = Convert.ToBoolean(row["Bloqueado"]);
            u.Activo = Convert.ToBoolean(row["Activo"]);
            u.Contador = int.Parse(row["Contador"].ToString());
            u.Idioma = row.Table.Columns.Contains("Idioma") && row["Idioma"] != DBNull.Value ? row["Idioma"].ToString() : "es";

            u.Rol = new SERVICESFamilia(
                row["PerfilNombre"].ToString(),
                row["Descripcion"].ToString()
            );
            u.Rol.ID = int.Parse(row["ID"].ToString());

            return u;
        }

        public int ActualizarIdioma(int dni, string idioma)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", dni));
            parametros.Add(acceso.CrearParametro("@Idioma", idioma));

            int res = acceso.Escribir(@"UPDATE Usuario
                                SET Idioma = @Idioma
                                WHERE DNI = @DNI", parametros);

            acceso.Cerrar();

            return res;
        }

        public SERVICESUsuario ObtenerPorDni(int dni)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", dni));

            DataTable tabla = acceso.Leer(@"SELECT U.DNI, U.Nombre, U.Apellido, U.Login,
                                           U.Contraseña, U.Mail, U.Bloqueado,
                                           U.Activo, U.Contador, ISNULL(U.Idioma, 'es') AS Idioma,
                                           P.ID, P.Nombre AS PerfilNombre, P.Descripcion
                                    FROM Usuario U
                                    INNER JOIN Perfil P ON U.Rol = P.ID
                                    WHERE U.DNI = @DNI", parametros);

            acceso.Cerrar();

            if (tabla.Rows.Count == 0)
                return null;

            return MapearUsuario(tabla.Rows[0]);
        }

        public SERVICESUsuario ObtenerPorLogin(string login)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@Login", login));

            DataTable tabla = acceso.Leer(@"SELECT U.DNI, U.Nombre, U.Apellido, U.Login,
                                           U.Contraseña, U.Mail, U.Bloqueado,
                                           U.Activo, U.Contador, ISNULL(U.Idioma, 'es') AS Idioma,
                                           P.ID, P.Nombre AS PerfilNombre, P.Descripcion
                                    FROM Usuario U
                                    INNER JOIN Perfil P ON U.Rol = P.ID
                                    WHERE U.Login = @Login", parametros);

            acceso.Cerrar();

            if (tabla.Rows.Count == 0)
                return null;

            return MapearUsuario(tabla.Rows[0]);
        }

        public bool ExisteDni(int dni)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", dni));

            DataTable tabla = acceso.Leer(@"SELECT DNI FROM Usuario WHERE DNI = @DNI", parametros);

            acceso.Cerrar();

            return tabla.Rows.Count > 0;
        }

        public bool ExisteLogin(string login)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@Login", login));

            DataTable tabla = acceso.Leer(@"SELECT Login FROM Usuario WHERE Login = @Login", parametros);

            acceso.Cerrar();

            return tabla.Rows.Count > 0;
        }

        public bool ExisteLoginEnOtroUsuario(string login, int dni)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@Login", login));
            parametros.Add(acceso.CrearParametro("@DNI", dni));

            DataTable tabla = acceso.Leer(@"SELECT Login
                                    FROM Usuario
                                    WHERE Login = @Login
                                    AND DNI <> @DNI", parametros);

            acceso.Cerrar();

            return tabla.Rows.Count > 0;
        }

        public int CambiarContraseña(int dni, string contraseñaHasheada)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", dni));
            parametros.Add(acceso.CrearParametro("@Contraseña", contraseñaHasheada));

            int res = acceso.Escribir(@"UPDATE Usuario
                                SET Contraseña = @Contraseña, Contador = 0, Bloqueado = 0
                                WHERE DNI = @DNI", parametros);

            acceso.Cerrar();

            return res;
        }

        public int IncrementarContador(int dni)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", dni));

            int res = acceso.Escribir(@"UPDATE Usuario
                                SET Contador = Contador + 1
                                WHERE DNI = @DNI", parametros);

            acceso.Cerrar();

            return res;
        }

        public int Bloquear(int dni)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", dni));

            int res = acceso.Escribir(@"UPDATE Usuario
                                SET Bloqueado = 1
                                WHERE DNI = @DNI", parametros);

            acceso.Cerrar();

            return res;
        }

        public int ReiniciarContador(int dni)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@DNI", dni));

            int res = acceso.Escribir(@"UPDATE Usuario
                                SET Contador = 0
                                WHERE DNI = @DNI", parametros);

            acceso.Cerrar();

            return res;
        }

    }
}