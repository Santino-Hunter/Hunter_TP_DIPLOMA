using SERVICES.SERVICESCambioIdioma;
using System.Windows.Forms;

namespace GUI
{
    internal static class MensajeIdioma
    {
        public static string T(string clave, string textoOriginal)
        {
            return TraductorIdioma.Texto(clave, textoOriginal);
        }

        public static DialogResult Info(string claveTexto, string textoOriginal, string claveTitulo, string tituloOriginal)
        {
            return MessageBox.Show(
                T(claveTexto, textoOriginal),
                T(claveTitulo, tituloOriginal),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public static DialogResult Advertencia(string claveTexto, string textoOriginal, string claveTitulo, string tituloOriginal)
        {
            return MessageBox.Show(
                T(claveTexto, textoOriginal),
                T(claveTitulo, tituloOriginal),
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        public static DialogResult Error(string claveTexto, string textoOriginal, string claveTitulo, string tituloOriginal)
        {
            return MessageBox.Show(
                T(claveTexto, textoOriginal),
                T(claveTitulo, tituloOriginal),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static DialogResult PreguntaSiNo(string claveTexto, string textoOriginal, string claveTitulo, string tituloOriginal)
        {
            return MessageBox.Show(
                T(claveTexto, textoOriginal),
                T(claveTitulo, tituloOriginal),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
        }
    }
}