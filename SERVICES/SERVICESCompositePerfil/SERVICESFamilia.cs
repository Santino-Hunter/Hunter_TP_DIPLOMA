using System;
using System.Linq;

namespace SERVICES
{
    public class SERVICESFamilia : SERVICESPerfil
    {
        public SERVICESFamilia(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public SERVICESFamilia(int id, string descripcion, string nombre) : base(nombre, descripcion)
        {
            ID = id;
        }

        public override void Agregar(SERVICESPerfil permiso)
        {
            if (permiso == null)
                throw new Exception("No se puede agregar un componente nulo.");

            if (Permisos.Any(x => x.ID == permiso.ID && x.GetType() == permiso.GetType()))
                return;

            Permisos.Add(permiso);
        }

        public override void Quitar(SERVICESPerfil permiso)
        {
            if (permiso == null)
                throw new Exception("No se puede quitar un componente nulo.");

            SERVICESPerfil encontrado = Permisos.FirstOrDefault(x => x.ID == permiso.ID && x.GetType() == permiso.GetType());

            if (encontrado != null)
                Permisos.Remove(encontrado);
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
