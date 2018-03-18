using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MathSite.Common.Crypto
{
    public class AesEncryptor: IEncryptor
    {
        public byte[] Vector { get; }

        public byte[] Key { get; }

        public AesEncryptor(IKeyVectorReader keyVectorReader)
        {
            var keyVectorPair = keyVectorReader.GetKeyVectorAsync().Result;
            Key = keyVectorPair.Key;
            Vector = keyVectorPair.Vector;
        }
        public Task<byte[]> EncryptStringToBytes(string message)
        {
            if (message == null || message.Length <= 0)
                throw new ArgumentNullException("No message to encrypt");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key is 0 length");
            if (Vector == null || Vector.Length <= 0)
                throw new ArgumentNullException("Vector is zero length");
                
            byte[] encrypted;
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = Vector;
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

        public Task<string> DecryptStringFromBytes(byte[] encryptedMessage)
        {
            if (encryptedMessage == null || encryptedMessage.Length <= 0)
                throw new ArgumentNullException("No message to decrypt");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key is 0 length");
            if (Vector == null || Vector.Length <= 0)
                throw new ArgumentNullException("Vector is zero length");
                
            string decrypted;
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = Vector;
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

            return Task.FromResult(decrypted);
        }
    }
}
