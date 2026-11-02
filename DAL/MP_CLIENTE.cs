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
    public class MP_CLIENTE : MAPPER<BECliente>
    {
        private readonly bool accesoCompartido;

        public MP_CLIENTE()
        {
            accesoCompartido = false;
        }

        internal MP_CLIENTE(ACCESO accesoExistente)
        {
            if (accesoExistente == null)
                throw new ArgumentNullException("accesoExistente");

            acceso = accesoExistente;
            accesoCompartido = true;
        }


        public override int Guardar(BECliente cliente)
        {
            if (!accesoCompartido)
            {
                acceso = new ACCESO();
                acceso.Abrir();
            }

            try
            {

                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso.CrearParametro("@Nombre", cliente.Nombre));
                parametros.Add(acceso.CrearParametro("@Apellido", cliente.Apellido));
                parametros.Add(acceso.CrearParametro("@Telefono", cliente.Telefono));
                parametros.Add(acceso.CrearParametro("@CorreoElectronico",cliente.CorreoElectronico));

                DataTable tabla = acceso.Leer(@"
                    INSERT INTO Cliente
                    (
                        Nombre,
                        Apellido,
                        Telefono,
                        CorreoElectronico
                    )
                    OUTPUT INSERTED.IdCliente
                    VALUES
                    (
                        @Nombre,
                        @Apellido,
                        @Telefono,
                        @CorreoElectronico
                    )",
                    parametros);

                if (tabla.Rows.Count == 0)
                    return 0;

                cliente.IdCliente = Convert.ToInt32(tabla.Rows[0]["IdCliente"]);

                return 1;
            }
            finally
            {
                if (!accesoCompartido && acceso != null)
                    acceso.Cerrar();
            }
        }

        public override int Modificar(BECliente cliente)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso.CrearParametro("@IdCliente", cliente.IdCliente));
                parametros.Add(acceso.CrearParametro("@Nombre", cliente.Nombre));
                parametros.Add(acceso.CrearParametro("@Apellido", cliente.Apellido));
                parametros.Add(acceso.CrearParametro("@Telefono", cliente.Telefono));
                parametros.Add(acceso.CrearParametro("@CorreoElectronico",cliente.CorreoElectronico));

                return acceso.Escribir(@"UPDATE Cliente SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, CorreoElectronico = @CorreoElectronico WHERE IdCliente = @IdCliente",parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public override List<BECliente> ObtenerTodos()
        {
            List<BECliente> clientes = new List<BECliente>();

            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                DataTable tabla = acceso.Leer(@"
                    SELECT
                        IdCliente,
                        Nombre,
                        Apellido,
                        Telefono,
                        CorreoElectronico
                    FROM Cliente
                    ORDER BY Apellido, Nombre");

                foreach (DataRow row in tabla.Rows)
                    clientes.Add(MapearCliente(row));
            }
            finally
            {
                acceso.Cerrar();
            }

            return clientes;
        }

        public BECliente Buscar(string telefono, string correoElectronico)
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(
                    acceso.CrearParametro("@Telefono",telefono ?? ""));

                parametros.Add(
                    acceso.CrearParametro("@CorreoElectronico",correoElectronico ?? ""));

                DataTable tabla = acceso.Leer(@"
                    SELECT TOP 1
                        IdCliente,
                        Nombre,
                        Apellido,
                        Telefono,
                        CorreoElectronico
                    FROM Cliente
                    WHERE
                        (@Telefono <> ''
                         AND Telefono = @Telefono)

                        OR

                        (@CorreoElectronico <> ''
                         AND CorreoElectronico = @CorreoElectronico)",
                    parametros);

                if (tabla.Rows.Count == 0)
                    return null;

                return MapearCliente(tabla.Rows[0]);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public BECliente Buscar(BECliente cliente)
        {
            if (cliente == null)
                return null;

            return Buscar(cliente.Telefono,cliente.CorreoElectronico);
        }

        private BECliente MapearCliente(DataRow row)
        {
            BECliente cliente = new BECliente();

            cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);

            cliente.Nombre = row["Nombre"].ToString();

            cliente.Apellido = row["Apellido"].ToString();

            cliente.Telefono = row["Telefono"].ToString();

            cliente.CorreoElectronico = row["CorreoElectronico"].ToString();

            return cliente;
        }


        public int GuardarConIntegridad(BECliente cliente, Action<MP_DIGITOVERIFICADOR> actualizarIntegridad)
        {
            if (actualizarIntegridad == null)
                throw new ArgumentNullException("actualizarIntegridad");

            ACCESO accesoLocal = new ACCESO();

            try
            {
                accesoLocal.Abrir();
                accesoLocal.IniciarTransaccion();

                MP_CLIENTE mpCliente = new MP_CLIENTE(accesoLocal);
                MP_DIGITOVERIFICADOR mpDV = new MP_DIGITOVERIFICADOR(accesoLocal);

                int resultado = mpCliente.Guardar(cliente);

                if (resultado == 0)
                {
                    accesoLocal.RevertirTransaccion();
                    return 0;
                }

                actualizarIntegridad(mpDV);
                accesoLocal.ConfirmarTransaccion();

                return resultado;
            }
            finally
            {
                accesoLocal.Cerrar();
            }
        }

    }
}
