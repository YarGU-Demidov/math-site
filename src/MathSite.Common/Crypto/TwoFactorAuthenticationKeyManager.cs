using System;
using System.Linq;
using System.Threading.Tasks;

namespace MathSite.Common.Crypto
{
    public class TwoFactorAuthenticationKeyManager : IKeyManager
    {
        private readonly IEncryptor _aesEncryptor;

        public TwoFactorAuthenticationKeyManager(IEncryptor aesEncryptor)
        {
            _aesEncryptor = aesEncryptor;
        }

        public async Task<byte[]> CreateEncryptedKey()
        {
            var userUniqueKey = CreateKeyForUser();
            return await _aesEncryptor.EncryptStringToBytes(userUniqueKey);
        }

        public async Task<string> GetDecryptedString(byte[] encryptedBytes)
        {
            var keyString = await _aesEncryptor.DecryptStringFromBytes(encryptedBytes);
           return await Task.FromResult(keyString);
        }

        private static string CreateKeyForUser()
        {
            var rnd = new Random();
            var key = new string(Enumerable.Repeat(rnd, 10).Select(x => (char) x.Next(33, 123)).ToArray());
            return key;
        }
    }
}
