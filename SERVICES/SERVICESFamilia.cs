using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class SERVICESFamilia : SERVICESPerfil
    {
        private List<SERVICESPerfil> hijos = new List<SERVICESPerfil>();

        public SERVICESFamilia(int idPerfil, string descripcion, string nombre)
            : base(idPerfil, descripcion, nombre)
        {

        }

        public override int IdPerfil { get; set; }

        public override string Descripcion { get; set; }

        public override string Nombre { get; set; }

        public override void AgregarHijo(SERVICESPerfil componente)
        {
            hijos.Add(componente);
        }

        public override SERVICESPerfil ObtenerSiEsHijoFamilia(SERVICESPerfil componente)
        {
            return hijos.Find(x => x.IdPerfil == componente.IdPerfil);
        }

        public override SERVICESPerfil ObtenerHijo(SERVICESPerfil componente)
        {
            return hijos.Find(x => x.IdPerfil == componente.IdPerfil);
        }

        public override void AgregarPermisoSimple(SERVICESPerfil componente)
        {
            hijos.Add(componente);
        }

        public override List<SERVICESPerfil> ObtenerHijos()
        {
            return hijos;
        }

        public override List<SERVICESPerfil> ObtenerPermisosSimples()
        {
            return hijos;
        }

        public override void EliminarHijo(SERVICESPerfil componente)
        {
            hijos.Remove(componente);
        }
    }
}
