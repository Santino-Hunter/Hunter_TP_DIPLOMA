using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public abstract class SERVICESPerfil
    {
        public SERVICESPerfil(int idPerfil)
        {
            IdPerfil = idPerfil;
        }

        public SERVICESPerfil(int idPerfil, string descripcion, string nombre) : this(idPerfil)
        {
            Descripcion = descripcion;
            Nombre = nombre;
        }

        public abstract int IdPerfil { get; set; }
        public abstract string Descripcion { get; set; }
        public abstract string Nombre { get; set; }

        public abstract void AgregarHijo(SERVICESPerfil componente);
        public abstract SERVICESPerfil ObtenerSiEsHijoFamilia(SERVICESPerfil componente);
        public abstract SERVICESPerfil ObtenerHijo(SERVICESPerfil componente);
        public abstract void AgregarPermisoSimple(SERVICESPerfil componente);
        public abstract List<SERVICESPerfil> ObtenerHijos();
        public abstract List<SERVICESPerfil> ObtenerPermisosSimples();
        public abstract void EliminarHijo(SERVICESPerfil componente);

        public override string ToString()
        {
            return Nombre;
        }
    }
}
