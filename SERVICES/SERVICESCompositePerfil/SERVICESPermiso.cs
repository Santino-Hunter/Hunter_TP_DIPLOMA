using System;

namespace SERVICES
{
    public class SERVICESPermiso : SERVICESPerfil
    {
        public SERVICESPermiso(string nombre, string descripcion) : base(nombre, descripcion)
        {
        }

        public SERVICESPermiso(int id, string descripcion, string nombre) : base(nombre, descripcion)
        {
            ID = id;
        }

        public override void Agregar(SERVICESPerfil permiso)
        {
            throw new Exception("No se puede agregar un permiso a un permiso simple.");
        }

        public override void Quitar(SERVICESPerfil permiso)
        {
            throw new Exception("No se puede quitar un permiso desde un permiso simple.");
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
