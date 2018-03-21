using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KeyGenerator;
using MathSite.Common.Crypto;
using MathSite.Common.Extensions;
using Xunit;

namespace MathSite.Tests.Common
{
    public class AesEncryptorTests
    {

        //private readonly AesEncryptor _aesEncryptor;

        //public AesEncryptorTests()
        //{
        //    var path = $"{Environment.CurrentDirectory}/KeyVectorPair";

        //    if (!File.Exists(path))
        //        Program.Main(new[] { path }).Wait();

        //    _aesEncryptor = new AesEncryptor(new KeyVectorReader($"{Environment.CurrentDirectory}/KeyVectorPair"));
        //}
        //[Fact]
        //public void AesEncryptorHasKeyAndVectorToWorkWith()
        //{
        //    Assert.True(_aesEncryptor.Key.IsNotNullOrEmpty());
        //    Assert.True(_aesEncryptor.Vector.IsNotNullOrEmpty());
        //}

        //[Fact]
        //public async void AesEncryptorEncryptsStringIntoArrayOfBytes()
        //{
        //    var rnd = new Random();
        //    var key = new string(Enumerable.Repeat(rnd, 10).Select(x => (char)x.Next(33, 123)).ToArray());
        //    var encrypted = await _aesEncryptor.EncryptStringToBytes(key);
        //    Assert.True(encrypted.IsNotNullOrEmpty());
        //}
        //[Fact]
        //public async void AesEncryptorDecryptsBytesIntoString()
        //{
        //    var rnd = new Random();
        //    var key = new string(Enumerable.Repeat(rnd, 10).Select(x => (char)x.Next(33, 123)).ToArray());
        //    var encrypted = await _aesEncryptor.EncryptStringToBytes(key);
        //    var decrypted = await _aesEncryptor.DecryptStringFromBytes(encrypted);
        //    Assert.NotNull(decrypted);
        //}
        //[Fact]
        //public async void DecryptingEncryptedStringReturnsCorrectValue()
        //{
        //    var rnd = new Random();
        //    var key = new string(Enumerable.Repeat(rnd, 10).Select(x => (char)x.Next(33, 123)).ToArray());
        //    var encrypted = await _aesEncryptor.EncryptStringToBytes(key);
        //    var decrypted = await _aesEncryptor.DecryptStringFromBytes(encrypted);
        //    Assert.Equal(key,decrypted);
        //}
    }
}
