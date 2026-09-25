using DAL;
using SERVICES;
using SERVICES.SERVICESCambioIdioma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLIdioma
    {
        private readonly MP_USUARIO mpUsuario = new MP_USUARIO();
        private readonly BLLBitacoraEvento bitacora = new BLLBitacoraEvento();
        private readonly BLLDigitoVerificador dv = new BLLDigitoVerificador();

        public void CambiarIdiomaUsuario(string codigoIdioma)
        {
            if (!SERVICESSessionManager.IsLogged)
                throw new Exception("Debe haber una sesión iniciada para cambiar el idioma.");

            if (!TraductorIdioma.ExisteIdioma(codigoIdioma))
                throw new Exception("Idioma no válido o no disponible.");

            codigoIdioma = codigoIdioma.ToLower();

            SERVICESUsuario usuario = SERVICESSessionManager.ObtenerInstancia.Usuario;

            int res = mpUsuario.ActualizarIdioma(usuario.DNI, codigoIdioma);

            if (res <= 0)
                throw new Exception("No se pudo guardar el idioma del usuario.");

            dv.RecalcularUsuario();

            usuario.Idioma = codigoIdioma;

            GestorIdioma.Instancia.CambiarIdioma(codigoIdioma);

            bitacora.RegistrarEvento(
                usuario.Login,
                "Idiomas",
                "Cambió idioma a: " + codigoIdioma,
                2
            );
        }
    }
}
