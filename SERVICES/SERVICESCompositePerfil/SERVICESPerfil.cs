using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public abstract class SERVICESPerfil
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        private List<SERVICESPerfil> _permisos;

        public List<SERVICESPerfil> Permisos
        {
            get { return _permisos; }
            set { _permisos = value; }
        }

        public abstract void Agregar(SERVICESPerfil permiso);

        public abstract void Quitar(SERVICESPerfil permiso);

        public bool TienePermiso(string nombrePermiso)
        {
            return TienePermiso(nombrePermiso, new HashSet<string>());
        }

        private bool TienePermiso(string nombrePermiso, HashSet<string> visitados)
        {
            if (string.IsNullOrWhiteSpace(nombrePermiso))
                return false;

            string clave = GetType().Name + "_" + ID.ToString();

            if (ID > 0 && visitados.Contains(clave))
                return false;

            if (ID > 0)
                visitados.Add(clave);

            if (this is SERVICESPermiso &&
                string.Equals(Nombre, nombrePermiso, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            foreach (SERVICESPerfil permiso in Permisos)
            {
                if (permiso != null && permiso.TienePermiso(nombrePermiso, visitados))
                    return true;
            }

            return false;
        }

        public SERVICESPerfil(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            _permisos = new List<SERVICESPerfil>();
        }
    }
}
