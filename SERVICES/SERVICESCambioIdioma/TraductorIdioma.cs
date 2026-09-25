using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace SERVICES.SERVICESCambioIdioma
{
    public static class TraductorIdioma
    {
        private static readonly Dictionary<string, string> textos =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private static string CarpetaIdiomas
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Idiomas");
            }
        }

        public static void CargarIdioma(string codigo)
        {
            textos.Clear();

            if (string.IsNullOrWhiteSpace(codigo))
                throw new Exception("Debe seleccionar un idioma.");

            codigo = codigo.ToLower();

            // Español queda como idioma base del sistema.
            // No necesita archivo JSON porque se usan los textos originales del formulario.
            if (codigo == "es")
                return;

            string archivo = Path.Combine(CarpetaIdiomas, codigo + ".json");

            if (!File.Exists(archivo))
                throw new Exception("No se encontró el archivo JSON del idioma seleccionado.");

            string contenido = File.ReadAllText(archivo);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Dictionary<string, object> raiz =
                serializer.Deserialize<Dictionary<string, object>>(contenido);

            if (raiz == null || !raiz.ContainsKey("textos"))
                throw new Exception("El archivo de idioma no tiene el formato esperado.");

            Dictionary<string, object> textosJson =
                raiz["textos"] as Dictionary<string, object>;

            if (textosJson == null)
                throw new Exception("El nodo 'textos' del archivo JSON no es válido.");

            foreach (KeyValuePair<string, object> par in textosJson)
            {
                if (!string.IsNullOrWhiteSpace(par.Key))
                {
                    textos[par.Key] = par.Value != null ? par.Value.ToString() : "";
                }
            }
        }

        public static bool ExisteIdioma(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return false;

            codigo = codigo.ToLower();

            if (codigo == "es")
                return true;

            string archivo = Path.Combine(CarpetaIdiomas, codigo + ".json");
            return File.Exists(archivo);
        }

        public static string Texto(string clave, string textoOriginal)
        {
            if (string.IsNullOrWhiteSpace(clave))
                return textoOriginal;

            if (textos.ContainsKey(clave))
                return textos[clave];

            return textoOriginal;
        }
    }
}