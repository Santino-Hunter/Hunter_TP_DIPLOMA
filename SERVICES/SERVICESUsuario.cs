using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class SERVICESUsuario
    {
        public SERVICESUsuario()
        {

        }

        public SERVICESUsuario(int dni, string nombre, string apellido, string login, string contraseña,SERVICESPerfil rol, string mail,bool bloqueado, bool activo,int contador)
        {
            _dni = dni;
            _nombre = nombre;
            _apellido = apellido;
            _login = login;
            _cs = contraseña;
            _rol = rol;
            _mail = mail;
            _bloqueado = bloqueado;
            _activo = activo;
            _contador = contador;
        }

        private int _dni;

        public int DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        private string _cs;

        public string Contraseña
        {
            get { return _cs; }
            set { _cs = value; }
        }

        private SERVICESPerfil _rol;

        public SERVICESPerfil Rol
        {
            get { return _rol; }
            set { _rol = value; }
        }

        private string _mail;

        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        private bool _bloqueado;

        public bool Bloqueado
        {
            get { return _bloqueado; }
            set { _bloqueado = value; }
        }

        private bool _activo;

        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        private int _contador;

        public int Contador
        {
            get { return _contador; }
            set { _contador = value; }
        }

        private string _idioma = "es";

        public string Idioma
        {
            get { return _idioma; }
            set { _idioma = value; }
        }

        public override string ToString()
        {
            return Login;
        }
    }
}
