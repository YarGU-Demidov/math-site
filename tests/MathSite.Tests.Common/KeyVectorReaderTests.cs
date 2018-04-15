using System;
using System.IO;
using KeyGenerator;
using MathSite.Common.Crypto;
using MathSite.Common.Extensions;
using Xunit;

namespace MathSite.Tests.Common
{
    public class KeyVectorReaderTests : EncryptorTestsBase
    {
        private readonly KeyVectorReader _keyVectorReader;

        public KeyVectorReaderTests()
        {
            _keyVectorReader = GetKeyVectorReader();
        }

        [Fact]
        public async void KeyVectorFileExists()
        {
            var result = await _keyVectorReader.GetKeyVectorAsync();
            Assert.NotNull(result);
        }
        [Fact]
        public async void KeyVectorIsNotNullOrEmpty()
        {
            var result = await _keyVectorReader.GetKeyVectorAsync();
            Assert.True(result.Vector.IsNotNullOrEmpty());
            Assert.True(result.Key.IsNotNullOrEmpty());
        }
    }
}
