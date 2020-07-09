using Microsoft.Extensions.Options;
using PasswordApp.Web.Services.Contracts;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordApp.Web.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly string _secretKey;

        public EncryptionService(IOptions<EncryptionSettings> encryptionSettings)
        {
            _secretKey = encryptionSettings.Value.SecretKey;
        }

        public string Encrypt(string input, string salt)
        {
            using Aes aesAlgorithm = CreateAesAlgorithm(salt);

            var encryptor = aesAlgorithm.CreateEncryptor();
            string encryptedString;
            using (var ms = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(new CryptoStream(ms, encryptor, CryptoStreamMode.Write)))
                {
                    streamWriter.Write(input);
                }
                var bytes = ms.ToArray();
                encryptedString = Convert.ToBase64String(bytes);
            }
            return encryptedString;
        }

        public string Decrypt(string input, string salt)
        {
            using Aes aesAlgorithm = CreateAesAlgorithm(salt);

            var decryptor = aesAlgorithm.CreateDecryptor();

            string decryptedString;
            using (var ms = new MemoryStream(Convert.FromBase64String(input)))
            {
                using var streamReader = new StreamReader(new CryptoStream(ms, decryptor, CryptoStreamMode.Read));
                decryptedString = streamReader.ReadToEnd();
            }
            return decryptedString;
        }

        private Aes CreateAesAlgorithm(string salt)
        {
            Aes aesAlgorithm = Aes.Create();
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(_secretKey, Encoding.Unicode.GetBytes(salt));
            aesAlgorithm.Key = rfc2898DeriveBytes.GetBytes(aesAlgorithm.KeySize / 8);
            aesAlgorithm.IV = rfc2898DeriveBytes.GetBytes(aesAlgorithm.BlockSize / 8);
            return aesAlgorithm;
        }
    }
}