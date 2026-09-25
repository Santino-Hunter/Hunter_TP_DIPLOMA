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
    public class MP_RESERVA : MAPPER<BEReserva>
    {
        private const string SELECT_BASE = @"
            SELECT
                R.NumeroReserva,
                R.Fecha,
                R.HoraInicio,
                R.CantidadComensales,
                R.Observaciones,
                R.Estado,

                C.IdCliente,
                C.Nombre AS ClienteNombre,
                C.Apellido AS ClienteApellido,
                C.Telefono AS ClienteTelefono,
                C.CorreoElectronico AS ClienteCorreo,

                M.NumeroMesa,
                M.Capacidad

            FROM Reserva R

            INNER JOIN Cliente C
                ON R.IdCliente = C.IdCliente

            INNER JOIN Mesa M
                ON R.NumeroMesa = M.NumeroMesa
        ";

        public override int Guardar(BEReserva reserva)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros =
                    CrearParametrosReserva(reserva);

                DataTable tabla = acceso.Leer(@"
                    INSERT INTO Reserva
                    (
                        IdCliente,
                        NumeroMesa,
                        Fecha,
                        HoraInicio,
                        CantidadComensales,
                        Observaciones,
                        Estado
                    )

                    OUTPUT INSERTED.NumeroReserva

                    VALUES
                    (
                        @IdCliente,
                        @NumeroMesa,
                        @Fecha,
                        @HoraInicio,
                        @CantidadComensales,
                        @Observaciones,
                        @Estado
                    )",
                    parametros);

                if (tabla.Rows.Count == 0)
                    return 0;

                reserva.NumeroReserva = Convert.ToInt32(tabla.Rows[0]["NumeroReserva"]);

                return 1;
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public override int Modificar(BEReserva reserva)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = CrearParametrosReserva(reserva);

                parametros.Add(acceso.CrearParametro("@NumeroReserva", reserva.NumeroReserva));

                return acceso.Escribir(@"UPDATE Reserva SET IdCliente = @IdCliente, NumeroMesa = @NumeroMesa, Fecha = @Fecha, HoraInicio = @HoraInicio, CantidadComensales = @CantidadComensales, Observaciones = @Observaciones, Estado = @Estado WHERE NumeroReserva = @NumeroReserva", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public override List<BEReserva> ObtenerTodos()
        {
            return EjecutarConsulta(SELECT_BASE + " ORDER BY R.Fecha, R.HoraInicio");
        }

        public List<BEReserva> ListarPorEstado(string estado)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso.CrearParametro("@Estado", estado));

                DataTable tabla = acceso.Leer(SELECT_BASE + @" WHERE R.Estado = @Estado ORDER BY R.Fecha, R.HoraInicio", parametros);

                return MapearReservas(tabla);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public BEReserva BuscarPorId(int numeroReserva)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso.CrearParametro("@NumeroReserva", numeroReserva));

                DataTable tabla = acceso.Leer(SELECT_BASE + @" WHERE R.NumeroReserva = @NumeroReserva", parametros);

                if (tabla.Rows.Count == 0)
                    return null;

                return MapearReserva(tabla.Rows[0]);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public List<BEReserva> ListarReservasPorFecha(DateTime fecha)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(acceso.CrearParametro("@Fecha", fecha.Date));
                DataTable tabla = acceso.Leer(SELECT_BASE + @" WHERE R.Fecha = @Fecha ORDER BY R.HoraInicio", parametros);

                return MapearReservas(tabla);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public List<BEReserva> BuscarPorCriterios(DateTime? fecha, int? numeroMesa, string estado)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                string sql = SELECT_BASE + " WHERE 1 = 1 ";
                List<SqlParameter> parametros = new List<SqlParameter>();

                if (fecha.HasValue)
                {
                    sql += " AND R.Fecha = @Fecha ";
                    parametros.Add(acceso.CrearParametro("@Fecha", fecha.Value.Date));
                }

                if (numeroMesa.HasValue)
                {
                    sql += " AND R.NumeroMesa = @NumeroMesa ";
                    parametros.Add(acceso.CrearParametro("@NumeroMesa", numeroMesa.Value));
                }

                if (!string.IsNullOrWhiteSpace(estado))
                {
                    sql += " AND R.Estado = @Estado ";
                    parametros.Add(acceso.CrearParametro("@Estado", estado));
                }

                sql += " ORDER BY R.Fecha, R.HoraInicio ";

                DataTable tabla = acceso.Leer(sql, parametros);

                return MapearReservas(tabla);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public List<BEReserva> ListarProgramadas(string estado, DateTime fecha, TimeSpan horaActual)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso.CrearParametro("@Estado", estado));
                parametros.Add(acceso.CrearParametro("@Fecha", fecha.Date));
                parametros.Add(acceso.CrearParametro("@HoraActual", horaActual));

                DataTable tabla = acceso.Leer(SELECT_BASE + @" WHERE R.Estado = @Estado AND R.Fecha = @Fecha AND R.HoraInicio <= @HoraActual ORDER BY R.HoraInicio", parametros);

                return MapearReservas(tabla);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        private List<SqlParameter> CrearParametrosReserva(BEReserva reserva)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@IdCliente", reserva.Cliente.IdCliente));
            parametros.Add(acceso.CrearParametro("@NumeroMesa", reserva.Mesa.NumeroMesa));
            parametros.Add(acceso.CrearParametro("@Fecha", reserva.Fecha.Date));
            parametros.Add(acceso.CrearParametro("@HoraInicio", reserva.HoraInicio));
            parametros.Add(acceso.CrearParametro("@CantidadComensales", reserva.CantidadComensales));
            parametros.Add(acceso.CrearParametro("@Observaciones", reserva.Observaciones ?? ""));
            parametros.Add(acceso.CrearParametro("@Estado", reserva.Estado ?? ""));

            return parametros;
        }

        private List<BEReserva> EjecutarConsulta(string sql)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                DataTable tabla =
                    acceso.Leer(sql);

                return MapearReservas(tabla);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        private List<BEReserva> MapearReservas(DataTable tabla)
        {
            List<BEReserva> reservas = new List<BEReserva>();

            foreach (DataRow row in tabla.Rows)
                reservas.Add(MapearReserva(row));

            return reservas;
        }

        private BEReserva MapearReserva(DataRow row)
        {
            BECliente cliente = new BECliente();

            cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
            cliente.Nombre = row["ClienteNombre"].ToString();
            cliente.Apellido = row["ClienteApellido"].ToString();
            cliente.Telefono = row["ClienteTelefono"].ToString();
            cliente.CorreoElectronico = row["ClienteCorreo"].ToString();

            BEMesa mesa = new BEMesa();
            mesa.NumeroMesa = Convert.ToInt32(row["NumeroMesa"]);
            mesa.Capacidad = Convert.ToInt32(row["Capacidad"]);

            BEReserva reserva = new BEReserva();
            reserva.NumeroReserva = Convert.ToInt32(row["NumeroReserva"]);
            reserva.Cliente = cliente;
            reserva.Mesa = mesa;
            reserva.Fecha = Convert.ToDateTime(row["Fecha"]);
            reserva.HoraInicio = (TimeSpan)row["HoraInicio"];
            reserva.CantidadComensales = Convert.ToInt32(row["CantidadComensales"]);
            reserva.Observaciones = row["Observaciones"] == DBNull.Value ? "" : row["Observaciones"].ToString();
            reserva.Estado = row["Estado"].ToString();

            return reserva;
        }
    }
}
