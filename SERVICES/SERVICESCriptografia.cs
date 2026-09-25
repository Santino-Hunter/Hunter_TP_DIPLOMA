using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class SERVICESCriptografia
    {

        private const string PREFIJO_AES = "AES:";

        private const string CLAVE_SECRETA = "C&H_Restaurante_Clave_AES_2026";

        private static readonly byte[] SALT = Encoding.UTF8.GetBytes("C&H_SALT_2026");


        public string Hashear(string texto)
        {
            SHA256 sha = SHA256.Create();
            
            byte[] bytes = Encoding.UTF8.GetBytes(texto);
            byte[] hash = sha.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public string CifrarAES(string textoPlano)
        {
            if (string.IsNullOrWhiteSpace(textoPlano))
                return textoPlano;

            if (EstaCifrado(textoPlano))
                return textoPlano;

            byte[] textoBytes = Encoding.UTF8.GetBytes(textoPlano);

            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes derivador = new Rfc2898DeriveBytes(CLAVE_SECRETA, SALT, 10000);

                aes.Key = derivador.GetBytes(32); // 256 bits
                aes.GenerateIV();

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(textoBytes, 0, textoBytes.Length);
                        cs.FlushFinalBlock();
                    }

                    return PREFIJO_AES + Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string DescifrarAES(string textoCifrado)
        {
            if (string.IsNullOrWhiteSpace(textoCifrado))
                return textoCifrado;

            if (!EstaCifrado(textoCifrado))
                return textoCifrado;

            string base64 = textoCifrado.Substring(PREFIJO_AES.Length);
            byte[] datos = Convert.FromBase64String(base64);

            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes derivador = new Rfc2898DeriveBytes(CLAVE_SECRETA, SALT, 10000);

                aes.Key = derivador.GetBytes(32);

                byte[] iv = new byte[16];
                Array.Copy(datos, 0, iv, 0, iv.Length);
                aes.IV = iv;

                int largoTextoCifrado = datos.Length - iv.Length;
                byte[] textoCifradoBytes = new byte[largoTextoCifrado];
                Array.Copy(datos, iv.Length, textoCifradoBytes, 0, largoTextoCifrado);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(textoCifradoBytes, 0, textoCifradoBytes.Length);
                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        public bool EstaCifrado(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto) && texto.StartsWith(PREFIJO_AES);
        }

    }
}
