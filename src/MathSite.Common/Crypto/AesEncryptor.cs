using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MathSite.Common.Crypto
{
    public class AesEncryptor:IEncryptor

    {
        private readonly byte[] _key;
        private readonly byte[] _veсtor;
        public AesEncryptor(IKeyVectorReader keyVectorReader)
        {
            var keyVektorPair = keyVectorReader.GetKeyVector();
            _key = keyVektorPair.Key;
            _veсtor = keyVektorPair.Vector;
        }
        public Task<byte[]> EncryptStringToBytes(string message)
        {
            if (message == null || message.Length <= 0)
                throw new ArgumentNullException("No message to encrypt");
            if (_key == null || _key.Length <= 0)
                throw new ArgumentNullException("Key is 0 length");
            if (_veсtor == null || _veсtor.Length <= 0)
                throw new ArgumentNullException("Vector is zero length");
            byte[] encrypted;
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _veсtor;
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(message);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Task.FromResult(encrypted);
        }

        public string DecryptStringFromBytes(byte[] encryptedMessage)
        {
            if (encryptedMessage == null || encryptedMessage.Length <= 0)
                throw new ArgumentNullException("No message to decrypt");
            if (_key == null || _key.Length <= 0)
                throw new ArgumentNullException("Key is 0 length");
            if (_veсtor == null || _veсtor.Length <= 0)
                throw new ArgumentNullException("Vector is zero length");
            string decrypted = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _veсtor;
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var msDecrypt = new MemoryStream(encryptedMessage))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                                decrypted = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return decrypted;
        }
    }
}
