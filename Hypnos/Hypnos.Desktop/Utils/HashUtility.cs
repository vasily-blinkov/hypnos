using System;
using System.Security.Cryptography;
using System.Text;

namespace Hypnos.Desktop.Utils
{
    public static class HashUtility
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            const string salt = "woTdzTfu5VUxUjtnr8fJ";

            using (var sha2_512 = SHA512.Create())
            {
                return BitConverter.ToString(sha2_512.ComputeHash(Encoding.Unicode.GetBytes($"{salt}{password}")))
                    .Replace("-", string.Empty);
            }
        }
    }
}
