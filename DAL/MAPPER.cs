using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public abstract class MAPPER <T>
    {
        internal ACCESO acceso;

        public abstract int Guardar(T objeto);
        public abstract int Modificar(T objeto);
        public abstract List<T> ObtenerTodos();
    }
}