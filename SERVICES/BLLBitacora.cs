using BE;
using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BLLBitacora
    {
        private MP_EVENTO mp = new MP_EVENTO();

        public int RegistrarEvento(string login, string modulo, string evento, int criticidad)
        {
            if (string.IsNullOrWhiteSpace(login))
                login = "Sistema";

            if (string.IsNullOrWhiteSpace(modulo))
                throw new Exception("Debe indicar el módulo.");

            if (string.IsNullOrWhiteSpace(evento))
                throw new Exception("Debe indicar el evento.");

            if (criticidad < 1 || criticidad > 5)
                throw new Exception("La criticidad debe estar entre 1 y 5.");

            BEEvento nuevoEvento = new BEEvento();
            nuevoEvento.Login = login;
            nuevoEvento.FechaHora = DateTime.Now;
            nuevoEvento.Modulo = modulo;
            nuevoEvento.Evento = evento;
            nuevoEvento.Criticidad = criticidad;

            return mp.Guardar(nuevoEvento);
        }

        public List<BEEvento> ObtenerUltimosTresDias()
        {
            return mp.ObtenerUltimosTresDias();
        }

        public List<BEEvento> Filtrar(string login, string modulo, int criticidad, DateTime desde, DateTime hasta)
        {
            if (desde > hasta)
                throw new Exception("La fecha desde no puede ser mayor a la fecha hasta.");

            return mp.Filtrar(login, modulo, criticidad, desde, hasta);
        }
    }
}