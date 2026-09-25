using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_MESA : MAPPER<BEMesa>
    {
        public override int Guardar(BEMesa mesa)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso.CrearParametro("@NumeroMesa",mesa.NumeroMesa));
                parametros.Add(acceso.CrearParametro("@Capacidad",mesa.Capacidad));

                return acceso.Escribir(@"INSERT INTO Mesa(NumeroMesa,Capacidad) VALUES (@NumeroMesa,@Capacidad)",parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public override int Modificar(BEMesa mesa)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso.CrearParametro("@NumeroMesa",mesa.NumeroMesa));
                parametros.Add(acceso.CrearParametro("@Capacidad",mesa.Capacidad));

                return acceso.Escribir(@"UPDATE Mesa SET Capacidad = @Capacidad WHERE NumeroMesa = @NumeroMesa",parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public override List<BEMesa> ObtenerTodos()
        {
            List<BEMesa> mesas = new List<BEMesa>();

            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                DataTable tabla = acceso.Leer(@"SELECT NumeroMesa,Capacidad FROM Mesa ORDER BY NumeroMesa");

                foreach (DataRow row in tabla.Rows)
                    mesas.Add(MapearMesa(row));
            }
            finally
            {
                acceso.Cerrar();
            }

            return mesas;
        }

        public List<BEMesa> ListarMesas()
        {
            return ObtenerTodos();
        }

        private BEMesa MapearMesa(DataRow row)
        {
            BEMesa mesa = new BEMesa();

            mesa.NumeroMesa = Convert.ToInt32(row["NumeroMesa"]);
            mesa.Capacidad = Convert.ToInt32(row["Capacidad"]);

            return mesa;
        }

    }
}
