using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BLLPerfil
    {
        MP_PERFIL mp = new MP_PERFIL();
        private readonly BLLDigitoVerificador dv = new BLLDigitoVerificador();

        public int Insertar(SERVICES.SERVICESPerfil perfil)
        {
            ValidarPerfil(perfil);

            int res = mp.Guardar(perfil);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int Modificar(SERVICES.SERVICESPerfil perfil)
        {
            ValidarPerfil(perfil);

            if (perfil.ID <= 0)
                throw new Exception("Debe seleccionar un perfil válido.");

            int res = mp.Modificar(perfil);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int Eliminar(SERVICES.SERVICESPerfil perfil)
        {
            if (perfil == null || perfil.ID <= 0)
                throw new Exception("Debe seleccionar un perfil válido.");

            if (mp.EstaAsignadoAUsuario(perfil.ID))
                throw new Exception("No se puede eliminar un perfil asignado a usuarios.");

            int res = mp.Eliminar(perfil);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public List<SERVICES.SERVICESFamilia> Listar()
        {
            return mp.ListarConPermisos();
        }

        public int AgregarPermisoFamilia(SERVICES.SERVICESPerfil perfil, SERVICES.SERVICESPerfil permiso)
        {
            ValidarRelacion(perfil, permiso);

            int res;

            if (permiso is SERVICES.SERVICESPermiso)
                res = mp.AgregarPermiso(perfil, permiso);
            else if (permiso is SERVICES.SERVICESFamilia)
                res = mp.AgregarFamilia(perfil, permiso);
            else
                throw new Exception("El componente seleccionado no es una familia ni un permiso.");

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int QuitarPermisoFamilia(SERVICES.SERVICESPerfil perfil, SERVICES.SERVICESPerfil permiso)
        {
            ValidarRelacion(perfil, permiso);

            int res;

            if (permiso is SERVICES.SERVICESPermiso)
                res = mp.QuitarPermiso(perfil, permiso);
            else if (permiso is SERVICES.SERVICESFamilia)
                res = mp.QuitarFamilia(perfil, permiso);
            else
                throw new Exception("El componente seleccionado no es una familia ni un permiso.");

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        private void ValidarPerfil(SERVICES.SERVICESPerfil perfil)
        {
            if (perfil == null)
                throw new Exception("El perfil no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(perfil.Nombre))
                throw new Exception("El nombre del perfil es obligatorio.");

            if (string.IsNullOrWhiteSpace(perfil.Descripcion))
                throw new Exception("La descripción del perfil es obligatoria.");

            perfil.Nombre = perfil.Nombre.Trim();
            perfil.Descripcion = perfil.Descripcion.Trim();
        }

        private void ValidarRelacion(SERVICES.SERVICESPerfil perfil, SERVICES.SERVICESPerfil permiso)
        {
            if (perfil == null || perfil.ID <= 0)
                throw new Exception("Debe seleccionar un perfil válido.");

            if (permiso == null || permiso.ID <= 0)
                throw new Exception("Debe seleccionar una familia o permiso válido.");
        }
    }
}
