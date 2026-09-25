using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLBackupRestore
    {
        private readonly MP_BACKUPRESTORE mp = new MP_BACKUPRESTORE();

        public void RealizarRestore(string rutaBackup)
        {
            ValidarRutaRestore(rutaBackup);
            mp.RealizarRestore(rutaBackup);
        }

        public void RealizarBackup(string rutaBackup)
        {
            ValidarRutaBackup(rutaBackup);
            mp.RealizarBackup(rutaBackup);
        }

        private void ValidarRutaRestore(string rutaBackup)
        {
            if (string.IsNullOrWhiteSpace(rutaBackup))
                throw new Exception("Debe seleccionar un archivo de backup.");

            if (!File.Exists(rutaBackup))
                throw new Exception("El archivo de backup seleccionado no existe.");

            if (Path.GetExtension(rutaBackup).ToLower() != ".bak")
                throw new Exception("El archivo seleccionado debe tener extensión .bak.");
        }

        private void ValidarRutaBackup(string rutaBackup)
        {
            if (string.IsNullOrWhiteSpace(rutaBackup))
                throw new Exception("Debe indicar una ruta para generar el backup.");

            string carpeta = Path.GetDirectoryName(rutaBackup);

            if (string.IsNullOrWhiteSpace(carpeta) || !Directory.Exists(carpeta))
                throw new Exception("La carpeta seleccionada no existe.");

            if (Path.GetExtension(rutaBackup).ToLower() != ".bak")
                throw new Exception("El archivo de backup debe tener extensión .bak.");
        }
    }
}
