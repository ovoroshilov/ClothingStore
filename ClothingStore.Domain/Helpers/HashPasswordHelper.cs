using System;
using System.Security.Cryptography;
using System.Text;

namespace ClothingStore.Domain.Helpers
{
    public static class HashPasswordHelper
    {
       public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = BitConverter.ToString(bytes).Replace("-", "").ToLower();
                return hashedPassword;
            };
        }
    }
}
