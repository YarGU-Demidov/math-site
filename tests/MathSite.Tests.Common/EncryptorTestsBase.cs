using System;
using System.IO;
using KeyGenerator;
using MathSite.Common.Crypto;
using MathSite.Common.Extensions;

namespace MathSite.Tests.Common
{
    public abstract class EncryptorTestsBase
    {
        private static readonly Random Random = new Random(DateTime.UtcNow.Millisecond);
        private string _path;

        public KeyVectorReader GetKeyVectorReader()
        {
            _path = $"{Environment.CurrentDirectory}/KeyVectorPair-{Random.Next(0, 1_000_000)}";

            if (!File.Exists(_path))
                Program.Main(new[] { _path }).Wait();

            return new KeyVectorReader(_path);
        }

        ~EncryptorTestsBase()
        {
            if (_path.IsNullOrEmpty())
            {
                return;
            }

            File.Delete(_path);
        }
    }
}