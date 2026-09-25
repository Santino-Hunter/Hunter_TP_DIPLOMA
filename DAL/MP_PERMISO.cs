using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MP_PERMISO : MAPPER<SERVICES.SERVICESPermiso>
    {
        public int Eliminar(SERVICES.SERVICESPermiso obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", obj.ID));

            int res = acceso.Escribir(@"DELETE FROM PERFILxPERMISO WHERE ID_PM = @id;
                                        DELETE FROM PERMISOxFAMILIA WHERE ID_P = @id;
                                        DELETE FROM Permiso WHERE ID = @id;", parametros);

            acceso.Cerrar();
            return res;
        }

        public override int Guardar(SERVICES.SERVICESPermiso obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@nom", obj.Nombre));
            parametros.Add(acceso.CrearParametro("@desc", obj.Descripcion));

            int res = acceso.Escribir(@"INSERT INTO Permiso (Nombre, Descripcion)
                                        VALUES (@nom, @desc)", parametros);

            acceso.Cerrar();
            return res;
        }

        public override List<SERVICES.SERVICESPermiso> ObtenerTodos()
        {
            List<SERVICES.SERVICESPermiso> permisos = new List<SERVICES.SERVICESPermiso>();

            acceso = new ACCESO();
            acceso.Abrir();
            DataTable tabla = acceso.Leer(@"SELECT ID, Nombre, Descripcion FROM Permiso ORDER BY Nombre");
            acceso.Cerrar();

            foreach (DataRow row in tabla.Rows)
            {
                SERVICES.SERVICESPermiso permiso = new SERVICES.SERVICESPermiso(
                    row["Nombre"].ToString(),
                    row["Descripcion"].ToString()
                );

                permiso.ID = int.Parse(row["ID"].ToString());
                permisos.Add(permiso);
            }

            return permisos;
        }

        public override int Modificar(SERVICES.SERVICESPermiso obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", obj.ID));
            parametros.Add(acceso.CrearParametro("@nom", obj.Nombre));
            parametros.Add(acceso.CrearParametro("@desc", obj.Descripcion));

            int res = acceso.Escribir(@"UPDATE Permiso
                                        SET Nombre = @nom,
                                            Descripcion = @desc
                                        WHERE ID = @id", parametros);

            acceso.Cerrar();
            return res;
        }
    }
}
