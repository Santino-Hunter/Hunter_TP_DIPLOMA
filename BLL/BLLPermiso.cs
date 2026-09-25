using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BLLPermiso
    {
        MP_PERMISO mp = new MP_PERMISO();
        private readonly BLLDigitoVerificador dv = new BLLDigitoVerificador();

        public int Insertar(SERVICES.SERVICESPermiso permiso)
        {
            ValidarPermiso(permiso);

            int res = mp.Guardar(permiso);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int Modificar(SERVICES.SERVICESPermiso permiso)
        {
            ValidarPermiso(permiso);

            if (permiso.ID <= 0)
                throw new Exception("Debe seleccionar un permiso válido.");

            int res = mp.Modificar(permiso);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int Eliminar(SERVICES.SERVICESPermiso permiso)
        {
            if (permiso == null || permiso.ID <= 0)
                throw new Exception("Debe seleccionar un permiso válido.");

            int res = mp.Eliminar(permiso);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public List<SERVICES.SERVICESPermiso> Listar()
        {
            return mp.ObtenerTodos();
        }

        private void ValidarPermiso(SERVICES.SERVICESPermiso permiso)
        {
            if (permiso == null)
                throw new Exception("El permiso no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(permiso.Nombre))
                throw new Exception("El nombre del permiso es obligatorio.");

            if (string.IsNullOrWhiteSpace(permiso.Descripcion))
                throw new Exception("La descripción del permiso es obligatoria.");

            permiso.Nombre = permiso.Nombre.Trim();
            permiso.Descripcion = permiso.Descripcion.Trim();
        }
    }
}
