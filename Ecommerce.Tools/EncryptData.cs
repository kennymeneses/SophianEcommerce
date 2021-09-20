using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Tools
{
    public class EncryptData
    {
        public string PasswordToHash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var BytesText = Encoding.UTF8.GetBytes(text);
                var HashBytes = sha256.ComputeHash(BytesText);
                string HashString = BitConverter.ToString(HashBytes).Replace("-", "");

                return HashString;
            }
        }

    }
}
