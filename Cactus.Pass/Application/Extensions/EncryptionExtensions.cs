using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Application.Extensions
{
    public static class EncryptionExtensions
    {
        private const string EncryptionKey = "a1s23ds^*(!_#FSDF2f";

        public static string Encrypt(this string clearText)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);

            using var encryption = Aes.Create();

            var pdb = new Rfc2898DeriveBytes(EncryptionKey,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            encryption.Key = pdb.GetBytes(32);
            encryption.IV = pdb.GetBytes(16);

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryption.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }

            clearText = Convert.ToBase64String(ms.ToArray());

            return clearText;
        }

        public static string Decrypt(this string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");

            var cipherBytes = Convert.FromBase64String(cipherText);
            using var decryption = Aes.Create();

            var pdb = new Rfc2898DeriveBytes(EncryptionKey,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            decryption.Key = pdb.GetBytes(32);
            decryption.IV = pdb.GetBytes(16);

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, decryption.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }

            cipherText = Encoding.Unicode.GetString(ms.ToArray());

            return cipherText;
        }

        public static string EncryptMd5(this string clearText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            var originalBytes = Encoding.Default.GetBytes(clearText);
            var encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }
    }
}
