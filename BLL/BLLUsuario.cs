using DAL;
using SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    public class BLLUsuario
    {
        private readonly MP_USUARIO mp = new MP_USUARIO();
        private readonly SERVICESCriptografia cripto = new SERVICESCriptografia();
        private readonly BLLBitacoraEvento bitacora = new BLLBitacoraEvento();
        private readonly BLLDigitoVerificador dv = new BLLDigitoVerificador();

        private const int MAX_INTENTOS_LOGIN = 3;

        public List<SERVICESUsuario> ObtenerTodos()
        {
            List<SERVICESUsuario> usuarios = mp.ObtenerTodos();

            foreach (SERVICESUsuario usuario in usuarios)
            {
                usuario.Mail = cripto.DescifrarAES(usuario.Mail);
            }

            return usuarios;
        }

        public int Guardar(SERVICESUsuario usuario)
        {
            ValidarUsuario(usuario, true);

            if (mp.ExisteDni(usuario.DNI))
                throw new Exception("Ya existe un usuario registrado con ese DNI.");

            if (mp.ExisteLogin(usuario.Login))
                throw new Exception("Ya existe un usuario registrado con ese login.");

            string contraseñaInicial = usuario.DNI.ToString() + usuario.Apellido;
            usuario.Contraseña = cripto.Hashear(contraseñaInicial);

            usuario.Mail = cripto.CifrarAES(usuario.Mail);

            usuario.Bloqueado = false;
            usuario.Activo = true;
            usuario.Contador = 0;
            usuario.Idioma = "es";

            int res = mp.Guardar(usuario);

            if (res > 0)
            {
                dv.RecalcularUsuario();
                bitacora.RegistrarEvento($"{SERVICESSessionManager.ObtenerInstancia.Usuario.Login}", "Usuarios", "Registró usuario: " + usuario.Login, 4);
            }

            return res;
        }

        public int Modificar(SERVICESUsuario usuario)
        {
            ValidarUsuario(usuario, false);

            SERVICESUsuario usuarioExistente = mp.ObtenerPorDni(usuario.DNI);

            if (usuarioExistente == null)
                throw new Exception("El usuario que intenta modificar no existe.");

            if (mp.ExisteLoginEnOtroUsuario(usuario.Login, usuario.DNI))
                throw new Exception("Ya existe otro usuario registrado con ese login.");

            usuario.Mail = cripto.CifrarAES(usuario.Mail);

            int res = mp.Modificar(usuario);

            if (res > 0)
            {
                dv.RecalcularUsuario();
                bitacora.RegistrarEvento($"{SERVICESSessionManager.ObtenerInstancia.Usuario.Login}", "Usuarios", "Modificó usuario: " + usuario.Login, 3);
            }

            return res;
        }

        public int Activar(SERVICESUsuario usuario)
        {
            SERVICESUsuario usuarioExistente = ValidarUsuarioExistente(usuario);

            if (usuarioExistente.Activo)
                throw new Exception("El usuario ya se encuentra activo.");

            int res = mp.Activar(usuario);

            if (res > 0)
            {
                dv.RecalcularUsuario();
                bitacora.RegistrarEvento($"{SERVICESSessionManager.ObtenerInstancia.Usuario.Login}", "Usuarios", "Activó usuario: " + usuarioExistente.Login, 3);

            }
            return res;
        }

        public int Desactivar(SERVICESUsuario usuario)
        {
            SERVICESUsuario usuarioExistente = ValidarUsuarioExistente(usuario);

            if (!usuarioExistente.Activo)
                throw new Exception("El usuario ya se encuentra desactivado.");

            int res = mp.Desactivar(usuario);

            if (res > 0)
            {
                dv.RecalcularUsuario();
                bitacora.RegistrarEvento($"{SERVICESSessionManager.ObtenerInstancia.Usuario.Login}", "Usuarios", "Desactivó usuario: " + usuarioExistente.Login, 4);

            }
            return res;
        }

        public int Desbloquear(SERVICESUsuario usuario)
        {
            SERVICESUsuario usuarioExistente = ValidarUsuarioExistente(usuario);

            if (!usuarioExistente.Bloqueado)
                throw new Exception("El usuario seleccionado no está bloqueado.");

            int res = mp.Desbloquear(usuario);

            if (res > 0)
            {
                dv.RecalcularUsuario();
                bitacora.RegistrarEvento($"{SERVICESSessionManager.ObtenerInstancia.Usuario.Login}", "Usuarios", "Desbloqueó usuario: " + usuarioExistente.Login, 3);

            }
            return res;
        }

        public SERVICESUsuario Login(string login, string contraseña)
        {
            SERVICESUsuario usuario = mp.ObtenerPorLogin(login.Trim());

            if (usuario == null)
                throw new Exception("No existe este usuario en el sistema.");

            if (!usuario.Activo)
                throw new Exception("El usuario se encuentra desactivado.");

            if (usuario.Bloqueado)
                throw new Exception("El usuario se encuentra bloqueado.");

            string contraseñaHasheada = cripto.Hashear(contraseña);

            if (usuario.Contraseña != contraseñaHasheada)
            {
                mp.IncrementarContador(usuario.DNI);
                dv.RecalcularUsuario();

                bitacora.RegistrarEvento(usuario.Login, "Login", "Contraseña incorrecta", 3);

                int nuevoContador = usuario.Contador + 1;

                if (nuevoContador >= MAX_INTENTOS_LOGIN)
                {
                    mp.Bloquear(usuario.DNI);
                    dv.RecalcularUsuario();

                    bitacora.RegistrarEvento(usuario.Login, "Login", "Usuario bloqueado por superar intentos fallidos", 5);
                    throw new Exception("Contraseña incorrecta. El usuario fue bloqueado por superar el máximo de intentos.");
                }

                throw new Exception("Usuario o contraseña incorrectos. Intento " + nuevoContador + " de " + MAX_INTENTOS_LOGIN + ".");
            }

            mp.ReiniciarContador(usuario.DNI);
            dv.RecalcularUsuario();
            usuario.Contador = 0;

            usuario.Mail = cripto.DescifrarAES(usuario.Mail);

            BLLPerfil bllPerfil = new BLLPerfil();

            SERVICESPerfil perfilCompleto = bllPerfil.Listar()
                .FirstOrDefault(p => p.ID == usuario.Rol.ID);

            if (perfilCompleto == null)
                throw new Exception("No se pudo cargar el perfil del usuario.");

            usuario.Rol = perfilCompleto;

            bitacora.RegistrarEvento(usuario.Login, "Login", "Inicio de sesión exitoso", 1);

            return usuario;
        }

        public SERVICESUsuario LoginModoReparacion(string login, string contraseña)
        {
            SERVICESUsuario usuario = mp.ObtenerPorLogin(login.Trim());

            if (usuario == null)
                throw new Exception("No existe este usuario en el sistema.");

            if (!usuario.Activo)
                throw new Exception("El usuario se encuentra desactivado.");

            if (usuario.Bloqueado)
                throw new Exception("El usuario se encuentra bloqueado.");

            string contraseñaHasheada = cripto.Hashear(contraseña);

            if (usuario.Contraseña != contraseñaHasheada)
                throw new Exception("Usuario o contraseña incorrectos.");

            if (usuario.Rol == null ||
                !usuario.Rol.Nombre.Equals(
                    "Gerente",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                    "El sistema se encuentra en mantenimiento. " +
                    "Comuníquese con un Gerente del sistema.");
            }

            return usuario;
        }

        public int CambiarContraseña(int dni, string contraseñaActual, string contraseñaNueva, string confirmacion)
        {
            if (dni <= 0)
                throw new Exception("Usuario inválido.");

            if (string.IsNullOrWhiteSpace(contraseñaActual))
                throw new Exception("Debe ingresar la contraseña actual.");

            if (string.IsNullOrWhiteSpace(contraseñaNueva))
                throw new Exception("Debe ingresar la nueva contraseña.");

            if (string.IsNullOrWhiteSpace(confirmacion))
                throw new Exception("Debe confirmar la nueva contraseña.");

            if (contraseñaNueva != confirmacion)
                throw new Exception("La nueva contraseña y la confirmación no coinciden.");

            contraseñaNueva = cripto.Hashear(contraseñaNueva);

            if (contraseñaActual == contraseñaNueva)
                throw new Exception("La nueva contraseña no puede ser igual a la contraseña actual.");

            SERVICESUsuario usuario = mp.ObtenerPorDni(dni);

            if (usuario == null)
                throw new Exception("El usuario no existe.");

            int res = mp.CambiarContraseña(dni, contraseñaNueva);

            if (res > 0)
                dv.RecalcularUsuario();
                bitacora.RegistrarEvento(usuario.Login, "Usuarios", "Cambió su contraseña", 3);

            return res;

        }

        private SERVICESUsuario ValidarUsuarioExistente(SERVICESUsuario usuario)
        {
            if (usuario == null || usuario.DNI <= 0)
                throw new Exception("Debe seleccionar un usuario.");

            SERVICESUsuario usuarioExistente = mp.ObtenerPorDni(usuario.DNI);

            if (usuarioExistente == null)
                throw new Exception("El usuario seleccionado no existe.");

            return usuarioExistente;
        }

        private void ValidarUsuario(SERVICESUsuario usuario, bool esAlta)
        {
            if (usuario == null)
                throw new Exception("El usuario no puede ser nulo.");

            if (usuario.DNI <= 0)
                throw new Exception("El DNI es obligatorio y debe ser numérico.");

            if (usuario.DNI.ToString().Length < 7 || usuario.DNI.ToString().Length > 8)
                throw new Exception("El DNI debe tener entre 7 y 8 dígitos.");

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
                throw new Exception("El apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Login))
                throw new Exception("El login es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Mail))
                throw new Exception("El mail es obligatorio.");

            if (!Regex.IsMatch(usuario.Mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new Exception("El mail ingresado no tiene un formato válido.");

            if (usuario.Rol == null || usuario.Rol.ID <= 0)
                throw new Exception("Debe seleccionar un rol/perfil.");

            usuario.Nombre = usuario.Nombre.Trim();
            usuario.Apellido = usuario.Apellido.Trim();
            usuario.Login = usuario.Login.Trim();
            usuario.Mail = usuario.Mail.Trim();
        }

    }
}