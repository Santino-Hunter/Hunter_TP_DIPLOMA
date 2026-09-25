using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCliente
    {
        private readonly MP_CLIENTE mp = new MP_CLIENTE();

        public BECliente BuscarCliente(string telefono, string correoElectronico)
        {
            telefono = (telefono ?? "").Trim();
            correoElectronico = (correoElectronico ?? "").Trim();

            if (string.IsNullOrWhiteSpace(telefono) && string.IsNullOrWhiteSpace(correoElectronico))
                throw new Exception("Debe ingresar un teléfono o correo electrónico para buscar al cliente.");

            return mp.Buscar(telefono, correoElectronico);
        }

        public bool RegistrarCliente(BECliente cliente)
        {
            ValidarCliente(cliente);

            BECliente clienteExistente = mp.Buscar(cliente);

            if (clienteExistente != null)
                throw new Exception("El cliente ya se encuentra registrado.");

            return mp.Guardar(cliente) > 0;
        }

        private bool ValidarCliente(BECliente cliente)
        {
            if (cliente == null)
                throw new Exception("El cliente no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new Exception("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Apellido))
                throw new Exception("El apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Telefono))
                throw new Exception("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.CorreoElectronico))
                throw new Exception("El correo electrónico es obligatorio.");

            if (!Regex.IsMatch(cliente.CorreoElectronico, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new Exception("El correo electrónico no tiene un formato válido.");

            cliente.Nombre = cliente.Nombre.Trim();
            cliente.Apellido = cliente.Apellido.Trim();
            cliente.Telefono = cliente.Telefono.Trim();
            cliente.CorreoElectronico = cliente.CorreoElectronico.Trim();

            return true;
        }
    }
}
