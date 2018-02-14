using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathSite.Common.Crypto;

namespace MathSite.Tests.CoreThings
{
    public class TestKeyManager: IKeyManager
    {
        public Task<byte[]> CreateEncryptedKey()
        {
            var rnd = new Random();
            return Task.FromResult(Enumerable.Range(0, 12).Select(i => (byte) rnd.Next(0, 255)).ToArray());
        }

        public Task<bool> KeysAreEqual(string keyForVerification, byte[] encryptedUserUniqueKey)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetDecryptedString(byte[] encryptedBytes)
        {
            return Task.FromResult("Hello world");
        }
    }
}
