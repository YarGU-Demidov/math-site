﻿using System;
using System.IO;
using KeyGenerator;
using MathSite.Common.Crypto;
using MathSite.Common.Extensions;
using Xunit;

namespace MathSite.Tests.Common
{
    public class KeyVectorReaderTests
    {
        private readonly KeyVectorReader _keyVectorReader;

        public KeyVectorReaderTests()
        {
            var path = $"{Environment.CurrentDirectory}/KeyVectorPair";

            if (!File.Exists(path))
                Program.Main(new[] { path }).Wait();

            _keyVectorReader = new KeyVectorReader(path);
        }

        [Fact]
        public void KeyVectorFileExists()
        {
            var result =  _keyVectorReader.GetKeyVectorAsync();
            Assert.NotNull(result);
        }
        [Fact]
        public  void KeyVectorIsNotNullOrEmpty()
        {
            var result =  _keyVectorReader.GetKeyVectorAsync().Result;
            Assert.True(result.Vector.IsNotNullOrEmpty());
            Assert.True(result.Key.IsNotNullOrEmpty());
        }
    }
}
