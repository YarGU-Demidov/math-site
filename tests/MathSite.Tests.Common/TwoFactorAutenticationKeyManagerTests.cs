using System;
using System.IO;
using KeyGenerator;
using MathSite.Common.Crypto;
using MathSite.Common.Extensions;
using Xunit;

namespace MathSite.Tests.Common
{
    public class TwoFactorAutenticationKeyManagerTests
    {
        private readonly TwoFactorAuthenticationKeyManager _twoFactorAutenticationKeyManager;
        public TwoFactorAutenticationKeyManagerTests()
        {
            var path = $"{Environment.CurrentDirectory}/KeyVectorPair";

            if (!File.Exists(path))
                Program.Main(new[] { path }).Wait();
            _twoFactorAutenticationKeyManager = new TwoFactorAuthenticationKeyManager(new AesEncryptor(new KeyVectorReader($"{Environment.CurrentDirectory}/KeyVectorPair")));
        }
        [Fact]
        public async void CreateEncryptedKeyReturnsBytesThatAreNotNullOrEmpty()
        {
            var result = await _twoFactorAutenticationKeyManager.CreateEncryptedKey();
            Assert.True(result.IsNotNullOrEmpty());
        }
        [Fact]
        public async void CreateEncryptedKeyReturnsUniqueBytes()
        {
            var result1 = await _twoFactorAutenticationKeyManager.CreateEncryptedKey();
            var result2 = await _twoFactorAutenticationKeyManager.CreateEncryptedKey();
            Assert.NotEqual(result1, result2);
        }
    }
}
