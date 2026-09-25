using DAL;
using SERVICES;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BLLBitacoraEvento
    {
        private MP_BITACORAEVENTO mp = new MP_BITACORAEVENTO();

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

            SERVICESBitacoraEvento nuevoEvento = new SERVICESBitacoraEvento();
            nuevoEvento.Login = login;
            nuevoEvento.FechaHora = DateTime.Now;
            nuevoEvento.Modulo = modulo;
            nuevoEvento.Evento = evento;
            nuevoEvento.Criticidad = criticidad;

            return mp.Guardar(nuevoEvento);
        }

        public List<SERVICESBitacoraEvento> ObtenerUltimosTresDias()
        {
            return mp.ObtenerTodos();
        }

        public List<SERVICESBitacoraEvento> Filtrar(string login, string modulo, int criticidad, DateTime desde, DateTime hasta)
        {
            if (desde > hasta)
                throw new Exception("La fecha desde no puede ser mayor a la fecha hasta.");

            return mp.Filtrar(login, modulo, criticidad, desde, hasta);
        }
    }
}