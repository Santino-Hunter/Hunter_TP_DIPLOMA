using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BLLFamilia
    {
        MP_FAMILIA mp = new MP_FAMILIA();
        private readonly BLLDigitoVerificador dv = new BLLDigitoVerificador();

        public List<SERVICES.SERVICESFamilia> Listar()
        {
            return mp.ObtenerTodos();
        }

        public int Insertar(SERVICES.SERVICESFamilia familia)
        {
            ValidarFamilia(familia);

            int res = mp.Guardar(familia);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int Modificar(SERVICES.SERVICESFamilia familia)
        {
            ValidarFamilia(familia);

            if (familia.ID <= 0)
                throw new Exception("Debe seleccionar una familia válida.");

            int res = mp.Modificar(familia);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int Eliminar(SERVICES.SERVICESFamilia familia)
        {
            if (familia == null || familia.ID <= 0)
                throw new Exception("Debe seleccionar una familia válida.");

            int res = mp.Eliminar(familia);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int AgregarPermiso(SERVICES.SERVICESFamilia familia, SERVICES.SERVICESPermiso permiso)
        {
            if (familia == null || familia.ID <= 0)
                throw new Exception("Debe seleccionar una familia válida.");

            if (permiso == null || permiso.ID <= 0)
                throw new Exception("Debe seleccionar un permiso válido.");

            int res = mp.AgregarPermiso(familia, permiso);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int QuitarPermiso(SERVICES.SERVICESFamilia familia, SERVICES.SERVICESPermiso permiso)
        {
            if (familia == null || familia.ID <= 0)
                throw new Exception("Debe seleccionar una familia válida.");

            if (permiso == null || permiso.ID <= 0)
                throw new Exception("Debe seleccionar un permiso válido.");

            int res = mp.QuitarPermiso(familia, permiso);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int AgregarFamilia(SERVICES.SERVICESFamilia familiaPadre, SERVICES.SERVICESFamilia familiaHija)
        {
            if (familiaPadre == null || familiaPadre.ID <= 0)
                throw new Exception("Debe seleccionar una familia padre válida.");

            if (familiaHija == null || familiaHija.ID <= 0)
                throw new Exception("Debe seleccionar una familia hija válida.");

            int res = mp.AgregarFamilia(familiaPadre, familiaHija);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        public int QuitarFamilia(SERVICES.SERVICESFamilia familiaPadre, SERVICES.SERVICESFamilia familiaHija)
        {
            if (familiaPadre == null || familiaPadre.ID <= 0)
                throw new Exception("Debe seleccionar una familia padre válida.");

            if (familiaHija == null || familiaHija.ID <= 0)
                throw new Exception("Debe seleccionar una familia hija válida.");

            int res = mp.QuitarFamilia(familiaPadre, familiaHija);

            if (res > 0)
                dv.RecalcularSeguridad();

            return res;
        }

        private void ValidarFamilia(SERVICES.SERVICESFamilia familia)
        {
            if (familia == null)
                throw new Exception("La familia no puede ser nula.");

            if (string.IsNullOrWhiteSpace(familia.Nombre))
                throw new Exception("El nombre de la familia es obligatorio.");

            if (string.IsNullOrWhiteSpace(familia.Descripcion))
                throw new Exception("La descripción de la familia es obligatoria.");

            familia.Nombre = familia.Nombre.Trim();
            familia.Descripcion = familia.Descripcion.Trim();
        }
    }
}
