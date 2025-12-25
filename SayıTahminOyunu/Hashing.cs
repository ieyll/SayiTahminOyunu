using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SayıTahminOyunu
{
    public static class SecurityHelper
    {
       
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return null;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Şifreyi byte dizisine çevirip hashliyoruz
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Byte dizisini okunabilir 16'lık (hex) stringe dönüştürüyoruz
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
