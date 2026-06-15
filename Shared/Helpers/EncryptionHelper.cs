using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Shared.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly string Key =
            "CRM1234567890123456789012345678";

        public static string Encrypt(string text)
        {
            using Aes aes = Aes.Create();

            aes.Key =
                Encoding.UTF8.GetBytes(Key);

            aes.GenerateIV();

            var encryptor =
                aes.CreateEncryptor();

            byte[] textBytes =
                Encoding.UTF8.GetBytes(text);

            byte[] encrypted =
                encryptor.TransformFinalBlock(
                    textBytes,
                    0,
                    textBytes.Length);

            return Convert.ToBase64String(
                aes.IV.Concat(encrypted).ToArray());
        }

        public static string Decrypt(
            string cipherText)
        {
            byte[] fullCipher =
                Convert.FromBase64String(
                    cipherText);

            using Aes aes = Aes.Create();

            aes.Key =
                Encoding.UTF8.GetBytes(Key);

            byte[] iv =
                fullCipher.Take(16).ToArray();

            byte[] cipher =
                fullCipher.Skip(16).ToArray();

            aes.IV = iv;

            var decryptor =
                aes.CreateDecryptor();

            byte[] decrypted =
                decryptor.TransformFinalBlock(
                    cipher,
                    0,
                    cipher.Length);

            return Encoding.UTF8.GetString(
                decrypted);
        }
    }
}
