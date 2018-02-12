using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Authenticator;


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
        
        public async Task<bool> KeysAreEqual(string keyForVerification, byte[] encryptedUserUniqueKey)
        {
            var tfa = new TwoFactorAuthenticator();
            var userUniqueKey = await _aesEncryptor.DecryptStringFromBytes(encryptedUserUniqueKey);
            var isValid = tfa.ValidateTwoFactorPIN(userUniqueKey, keyForVerification);
            return await Task.FromResult(isValid);
        }

        private static string CreateKeyForUser()
        {
            var rnd = new Random();
            var key = new string(Enumerable.Repeat(rnd, 10).Select(x => (char) x.Next(33, 123)).ToArray());
            return key;
        }
    }
}
