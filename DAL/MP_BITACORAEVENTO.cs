using SERVICES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MP_BITACORAEVENTO : MAPPER<SERVICESBitacoraEvento>
    {
        public override int Guardar(SERVICESBitacoraEvento evento)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@Usuario", evento.Login));
            parametros.Add(acceso.CrearParametro("@FechaHora", evento.FechaHora));
            parametros.Add(acceso.CrearParametro("@Modulo", evento.Modulo));
            parametros.Add(acceso.CrearParametro("@Evento", evento.Evento));
            parametros.Add(acceso.CrearParametro("@Criticidad", evento.Criticidad));

            int res = acceso.Escribir(@"
                INSERT INTO Bitacora (Usuario, FechaHora, Modulo, Evento, Criticidad)
                VALUES (@Usuario, @FechaHora, @Modulo, @Evento, @Criticidad)", parametros);

            acceso.Cerrar();
            return res;
        }

        public override int Modificar(SERVICESBitacoraEvento objeto)
        {
            throw new NotImplementedException();
        }

        public override List<SERVICESBitacoraEvento> ObtenerTodos()
        {
            acceso = new ACCESO();
            acceso.Abrir();

            DataTable tabla = acceso.Leer(@"
                SELECT IdEvento, Usuario, FechaHora, Modulo, Evento, Criticidad
                FROM Bitacora
                ORDER BY FechaHora DESC");

            acceso.Cerrar();

            return MapearLista(tabla);
        }

        public List<SERVICESBitacoraEvento> ObtenerUltimosTresDias()
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@Desde", DateTime.Now.AddDays(-3)));

            DataTable tabla = acceso.Leer(@"
                SELECT IdEvento, Usuario, FechaHora, Modulo, Evento, Criticidad
                FROM Bitacora
                WHERE FechaHora >= @Desde
                ORDER BY FechaHora DESC", parametros);

            acceso.Cerrar();

            return MapearLista(tabla);
        }

        public List<SERVICESBitacoraEvento> Filtrar(string usuario, string modulo, int criticidad, DateTime desde, DateTime hasta)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            string sql = @"
                SELECT IdEvento, Usuario, FechaHora, Modulo, Evento, Criticidad
                FROM Bitacora
                WHERE FechaHora >= @Desde
                AND FechaHora <= @Hasta";

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@Desde", desde));
            parametros.Add(acceso.CrearParametro("@Hasta", hasta));

            if (!string.IsNullOrWhiteSpace(usuario) && usuario != "Todos")
            {
                sql += " AND Usuario = @Usuario";
                parametros.Add(acceso.CrearParametro("@Usuario", usuario));
            }

            if (!string.IsNullOrWhiteSpace(modulo) && modulo != "Todos")
            {
                sql += " AND Modulo = @Modulo";
                parametros.Add(acceso.CrearParametro("@Modulo", modulo));
            }

            if (criticidad > 0)
            {
                sql += " AND Criticidad = @Criticidad";
                parametros.Add(acceso.CrearParametro("@Criticidad", criticidad));
            }

            sql += " ORDER BY FechaHora DESC";

            DataTable tabla = acceso.Leer(sql, parametros);

            acceso.Cerrar();

            return MapearLista(tabla);
        }

        private List<SERVICESBitacoraEvento> MapearLista(DataTable tabla)
        {
            List<SERVICESBitacoraEvento> eventos = new List<SERVICESBitacoraEvento>();

            foreach (DataRow row in tabla.Rows)
            {
                SERVICESBitacoraEvento evento = new SERVICESBitacoraEvento();

                evento.IdEvento = Convert.ToInt32(row["IdEvento"]);
                evento.Login = row["Usuario"].ToString();
                evento.FechaHora = Convert.ToDateTime(row["FechaHora"]);
                evento.Modulo = row["Modulo"].ToString();
                evento.Evento = row["Evento"].ToString();
                evento.Criticidad = Convert.ToInt32(row["Criticidad"]);

                eventos.Add(evento);
            }

            return eventos;
        }
    }
}