using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES.SERVICESCambioIdioma
{
    public sealed class GestorIdioma
    {
        private static readonly GestorIdioma _instancia = new GestorIdioma();
        private readonly List<IIdiomaObservador> _observadores = new List<IIdiomaObservador>();

        private GestorIdioma()
        {
            CodigoActual = "es";
        }

        public static GestorIdioma Instancia
        {
            get { return _instancia; }
        }

        public string CodigoActual { get; private set; }

        public void Suscribir(IIdiomaObservador observador)
        {
            if (observador == null)
                return;

            if (!_observadores.Contains(observador))
                _observadores.Add(observador);
        }

        public void Desuscribir(IIdiomaObservador observador)
        {
            if (observador == null)
                return;

            if (_observadores.Contains(observador))
                _observadores.Remove(observador);
        }

        public void CambiarIdioma(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new Exception("Debe seleccionar un idioma.");

            CodigoActual = codigo.ToLower();
            TraductorIdioma.CargarIdioma(CodigoActual);
            Notificar();
        }

        private void Notificar()
        {
            foreach (IIdiomaObservador observador in _observadores.ToArray())
            {
                observador.ActualizarIdioma();
            }
        }
    }
}
