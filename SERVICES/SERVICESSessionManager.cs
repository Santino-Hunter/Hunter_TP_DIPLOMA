using System;

namespace SERVICES
{
    public class SERVICESSessionManager
    {
        private SERVICESSessionManager() { }

        private static readonly object _lock = new object();
        private static SERVICESSessionManager _session;

        public static SERVICESSessionManager ObtenerInstancia
        {
            get { return _session; }
        }

        public static bool IsLogged
        {
            get { return _session != null; }
        }

        public SERVICESUsuario Usuario { get; private set; }
        public DateTime FechaInicio { get; private set; }

        public static string Login(SERVICESUsuario usuario)
        {
            if (usuario == null)
                throw new Exception("No se puede iniciar sesión con un usuario nulo.");

            lock (_lock)
            {
                if (_session != null)
                    throw new Exception("Ya hay una sesión iniciada.");

                _session = new SERVICESSessionManager();
                _session.Usuario = usuario;
                _session.FechaInicio = DateTime.Now;

                return $"{_session.FechaInicio} - Se inició la sesión de {_session.Usuario.Nombre}";
            }
        }

        public static string Logout()
        {
            lock (_lock)
            {
                if (_session == null)
                    throw new Exception("No hay ninguna sesión iniciada.");

                _session = null;
                return $"{DateTime.Now} - Se cerró la sesión.";
            }
        }
    }
}