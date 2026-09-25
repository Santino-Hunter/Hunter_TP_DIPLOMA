using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class MP_FAMILIA : MAPPER<SERVICES.SERVICESFamilia>
    {
        public int Eliminar(SERVICES.SERVICESFamilia obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", obj.ID));

            int res = acceso.Escribir(@"DELETE FROM PERFILxFAMILIA WHERE ID_F = @id;
                                        DELETE FROM PERMISOxFAMILIA WHERE ID_F = @id;
                                        DELETE FROM FAMILIAxFAMILIA WHERE ID_F = @id OR ID_F2 = @id;
                                        DELETE FROM Familia WHERE ID = @id;", parametros);

            acceso.Cerrar();
            return res;
        }

        public override int Guardar(SERVICES.SERVICESFamilia obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@nom", obj.Nombre));
            parametros.Add(acceso.CrearParametro("@desc", obj.Descripcion));

            int res = acceso.Escribir(@"INSERT INTO Familia (Nombre, Descripcion)
                                        VALUES (@nom, @desc)", parametros);

            acceso.Cerrar();
            return res;
        }

        public override List<SERVICES.SERVICESFamilia> ObtenerTodos()
        {
            Dictionary<int, SERVICES.SERVICESFamilia> familias = new Dictionary<int, SERVICES.SERVICESFamilia>();

            acceso = new ACCESO();
            acceso.Abrir();

            DataTable tablaFamilias = acceso.Leer(@"SELECT ID, Nombre, Descripcion FROM Familia ORDER BY Nombre");

            foreach (DataRow row in tablaFamilias.Rows)
            {
                int id = int.Parse(row["ID"].ToString());
                SERVICES.SERVICESFamilia familia = new SERVICES.SERVICESFamilia(
                    row["Nombre"].ToString(),
                    row["Descripcion"].ToString()
                );

                familia.ID = id;
                familias.Add(id, familia);
            }

            DataTable tablaPermisos = acceso.Leer(@"SELECT pf.ID_F,
                                                           p.ID AS PermisoID,
                                                           p.Nombre AS PermisoNombre,
                                                           p.Descripcion AS PermisoDescripcion
                                                    FROM PERMISOxFAMILIA pf
                                                    INNER JOIN Permiso p ON p.ID = pf.ID_P
                                                    ORDER BY pf.ID_F, p.Nombre");

            foreach (DataRow row in tablaPermisos.Rows)
            {
                int idFamilia = int.Parse(row["ID_F"].ToString());

                if (!familias.ContainsKey(idFamilia))
                    continue;

                SERVICES.SERVICESPermiso permiso = new SERVICES.SERVICESPermiso(
                    row["PermisoNombre"].ToString(),
                    row["PermisoDescripcion"].ToString()
                );

                permiso.ID = int.Parse(row["PermisoID"].ToString());
                familias[idFamilia].Agregar(permiso);
            }

            DataTable tablaFamiliasHijas = acceso.Leer(@"SELECT ff.ID_F,
                                                               ff.ID_F2
                                                        FROM FAMILIAxFAMILIA ff");

            foreach (DataRow row in tablaFamiliasHijas.Rows)
            {
                int idPadre = int.Parse(row["ID_F"].ToString());
                int idHija = int.Parse(row["ID_F2"].ToString());

                if (idPadre == idHija)
                    continue;

                if (familias.ContainsKey(idPadre) && familias.ContainsKey(idHija))
                    familias[idPadre].Agregar(familias[idHija]);
            }

            acceso.Cerrar();

            return familias.Values.ToList();
        }

        public override int Modificar(SERVICES.SERVICESFamilia obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", obj.ID));
            parametros.Add(acceso.CrearParametro("@nom", obj.Nombre));
            parametros.Add(acceso.CrearParametro("@desc", obj.Descripcion));

            int res = acceso.Escribir(@"UPDATE Familia
                                        SET Nombre = @nom,
                                            Descripcion = @desc
                                        WHERE ID = @id", parametros);

            acceso.Cerrar();
            return res;
        }

        public int AgregarPermiso(SERVICES.SERVICESFamilia familia, SERVICES.SERVICESPermiso permiso)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id_f", familia.ID));
            parametros.Add(acceso.CrearParametro("@id_p", permiso.ID));

            int res = acceso.Escribir(@"IF NOT EXISTS (SELECT 1 FROM PERMISOxFAMILIA WHERE ID_P = @id_p AND ID_F = @id_f)
                                        BEGIN
                                            INSERT INTO PERMISOxFAMILIA (ID_P, ID_F)
                                            VALUES (@id_p, @id_f)
                                        END", parametros);

            acceso.Cerrar();
            return res;
        }

        public int QuitarPermiso(SERVICES.SERVICESFamilia familia, SERVICES.SERVICESPermiso permiso)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id_f", familia.ID));
            parametros.Add(acceso.CrearParametro("@id_p", permiso.ID));

            int res = acceso.Escribir(@"DELETE FROM PERMISOxFAMILIA
                                        WHERE ID_P = @id_p AND ID_F = @id_f", parametros);

            acceso.Cerrar();
            return res;
        }

        public int AgregarFamilia(SERVICES.SERVICESFamilia familiaPadre, SERVICES.SERVICESFamilia familiaHija)
        {
            if (familiaPadre.ID == familiaHija.ID)
                throw new Exception("No se puede agregar una familia dentro de sí misma.");

            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id_f", familiaPadre.ID));
            parametros.Add(acceso.CrearParametro("@id_f2", familiaHija.ID));

            int res = acceso.Escribir(@"IF NOT EXISTS (SELECT 1 FROM FAMILIAxFAMILIA WHERE ID_F = @id_f AND ID_F2 = @id_f2)
                                        BEGIN
                                            INSERT INTO FAMILIAxFAMILIA (ID_F, ID_F2)
                                            VALUES (@id_f, @id_f2)
                                        END", parametros);

            acceso.Cerrar();
            return res;
        }

        public int QuitarFamilia(SERVICES.SERVICESFamilia familiaPadre, SERVICES.SERVICESFamilia familiaHija)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id_f", familiaPadre.ID));
            parametros.Add(acceso.CrearParametro("@id_f2", familiaHija.ID));

            int res = acceso.Escribir(@"DELETE FROM FAMILIAxFAMILIA
                                        WHERE ID_F = @id_f AND ID_F2 = @id_f2", parametros);

            acceso.Cerrar();
            return res;
        }
    }
}
