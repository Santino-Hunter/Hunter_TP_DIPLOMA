using SERVICES;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class MP_PERFIL : MAPPER<SERVICES.SERVICESPerfil>
    {
        public int Eliminar(SERVICES.SERVICESPerfil obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", obj.ID));

            int res = acceso.Escribir(@"DELETE FROM PERFILxFAMILIA WHERE ID_P = @id;
                                        DELETE FROM PERFILxPERMISO WHERE ID_PF = @id;
                                        DELETE FROM Perfil WHERE ID = @id;", parametros);

            acceso.Cerrar();
            return res;
        }

        public bool EstaAsignadoAUsuario(int idPerfil)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", idPerfil));

            DataTable tabla = acceso.Leer(@"SELECT DNI FROM Usuario WHERE Rol = @id", parametros);

            acceso.Cerrar();
            return tabla.Rows.Count > 0;
        }

        public override int Guardar(SERVICES.SERVICESPerfil obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@nom", obj.Nombre));
            parametros.Add(acceso.CrearParametro("@desc", obj.Descripcion));

            int res = acceso.Escribir(@"INSERT INTO Perfil (Nombre, Descripcion)
                                        VALUES (@nom, @desc)", parametros);

            acceso.Cerrar();
            return res;
        }

        public override List<SERVICES.SERVICESPerfil> ObtenerTodos()
        {
            List<SERVICES.SERVICESPerfil> perfiles = new List<SERVICES.SERVICESPerfil>();

            acceso = new ACCESO();
            acceso.Abrir();
            DataTable tabla = acceso.Leer(@"SELECT ID, Nombre, Descripcion FROM Perfil ORDER BY Nombre");
            acceso.Cerrar();

            foreach (DataRow row in tabla.Rows)
            {
                SERVICES.SERVICESPerfil perfil = new SERVICES.SERVICESFamilia(
                    row["Nombre"].ToString(),
                    row["Descripcion"].ToString()
                );

                perfil.ID = int.Parse(row["ID"].ToString());
                perfiles.Add(perfil);
            }

            return perfiles;
        }

        public override int Modificar(SERVICES.SERVICESPerfil obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", obj.ID));
            parametros.Add(acceso.CrearParametro("@nom", obj.Nombre));
            parametros.Add(acceso.CrearParametro("@desc", obj.Descripcion));

            int res = acceso.Escribir(@"UPDATE Perfil
                                        SET Nombre = @nom,
                                            Descripcion = @desc
                                        WHERE ID = @id", parametros);

            acceso.Cerrar();
            return res;
        }

        public int AgregarPermiso(SERVICES.SERVICESPerfil perfil, SERVICES.SERVICESPerfil permiso)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id_pf", perfil.ID));
            parametros.Add(acceso.CrearParametro("@id_pm", permiso.ID));

            int res = acceso.Escribir(@"IF NOT EXISTS (SELECT 1 FROM PERFILxPERMISO WHERE ID_PF = @id_pf AND ID_PM = @id_pm)
                                        BEGIN
                                            INSERT INTO PERFILxPERMISO (ID_PF, ID_PM)
                                            VALUES (@id_pf, @id_pm)
                                        END", parametros);

            acceso.Cerrar();
            return res;
        }

        public int QuitarPermiso(SERVICES.SERVICESPerfil perfil, SERVICES.SERVICESPerfil permiso)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id_pf", perfil.ID));
            parametros.Add(acceso.CrearParametro("@id_pm", permiso.ID));

            int res = acceso.Escribir(@"DELETE FROM PERFILxPERMISO
                                        WHERE ID_PM = @id_pm AND ID_PF = @id_pf", parametros);

            acceso.Cerrar();
            return res;
        }

        public int AgregarFamilia(SERVICES.SERVICESPerfil perfil, SERVICES.SERVICESPerfil familia)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id_p", perfil.ID));
            parametros.Add(acceso.CrearParametro("@id_f", familia.ID));

            int res = acceso.Escribir(@"IF NOT EXISTS (SELECT 1 FROM PERFILxFAMILIA WHERE ID_P = @id_p AND ID_F = @id_f)
                                        BEGIN
                                            INSERT INTO PERFILxFAMILIA (ID_P, ID_F)
                                            VALUES (@id_p, @id_f)
                                        END", parametros);

            acceso.Cerrar();
            return res;
        }

        public int QuitarFamilia(SERVICES.SERVICESPerfil perfil, SERVICES.SERVICESPerfil familia)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id_p", perfil.ID));
            parametros.Add(acceso.CrearParametro("@id_f", familia.ID));

            int res = acceso.Escribir(@"DELETE FROM PERFILxFAMILIA
                                        WHERE ID_P = @id_p AND ID_F = @id_f", parametros);

            acceso.Cerrar();
            return res;
        }

        public List<SERVICES.SERVICESFamilia> ListarConPermisos()
        {
            List<SERVICES.SERVICESFamilia> perfiles = ObtenerTodos().OfType<SERVICES.SERVICESFamilia>().ToList();

            MP_FAMILIA mpFamilia = new MP_FAMILIA();
            List<SERVICES.SERVICESFamilia> familias = mpFamilia.ObtenerTodos();

            acceso = new ACCESO();
            acceso.Abrir();

            DataTable tablaPermisosDirectos = acceso.Leer(@"SELECT pp.ID_PF,
                                                                  p.ID AS PermisoID,
                                                                  p.Nombre AS PermisoNombre,
                                                                  p.Descripcion AS PermisoDescripcion
                                                           FROM PERFILxPERMISO pp
                                                           INNER JOIN Permiso p ON p.ID = pp.ID_PM
                                                           ORDER BY pp.ID_PF, p.Nombre");

            foreach (DataRow row in tablaPermisosDirectos.Rows)
            {
                int idPerfil = int.Parse(row["ID_PF"].ToString());
                SERVICES.SERVICESFamilia perfilEncontrado = perfiles.FirstOrDefault(p => p.ID == idPerfil);

                if (perfilEncontrado == null)
                    continue;

                SERVICES.SERVICESPermiso permiso = new SERVICES.SERVICESPermiso(
                    row["PermisoNombre"].ToString(),
                    row["PermisoDescripcion"].ToString()
                );

                permiso.ID = int.Parse(row["PermisoID"].ToString());
                perfilEncontrado.Agregar(permiso);
            }

            DataTable tablaFamilias = acceso.Leer(@"SELECT ID_P, ID_F
                                                   FROM PERFILxFAMILIA
                                                   ORDER BY ID_P, ID_F");

            foreach (DataRow row in tablaFamilias.Rows)
            {
                int idPerfil = int.Parse(row["ID_P"].ToString());
                int idFamilia = int.Parse(row["ID_F"].ToString());

                SERVICES.SERVICESFamilia perfilEncontrado = perfiles.FirstOrDefault(p => p.ID == idPerfil);
                SERVICES.SERVICESFamilia familiaEncontrada = familias.FirstOrDefault(f => f.ID == idFamilia);

                if (perfilEncontrado != null && familiaEncontrada != null)
                    perfilEncontrado.Agregar(familiaEncontrada);
            }

            acceso.Cerrar();
            return perfiles;
        }
    }
}
