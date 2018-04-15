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
        public void KeyVectorFileExists()
        {
            var result = _keyVectorReader.GetKeyVector();
            Assert.NotNull(result);
        }
        [Fact]
        public void KeyVectorIsNotNullOrEmpty()
        {
            var result = _keyVectorReader.GetKeyVector();
            Assert.True(result.Vector.IsNotNullOrEmpty());
            Assert.True(result.Key.IsNotNullOrEmpty());
        }
    }
}
