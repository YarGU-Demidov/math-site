using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MathSite.Common.Crypto;

namespace KeyGenerator
{
    public static class Program
    {
        static ReaderWriterLock locker = new ReaderWriterLock();
        public static async Task Main(string[] args)
        {
            using (Aes myAes = Aes.Create())
            {
                var serializeObject = JsonConvert.SerializeObject(new KeyVectorPair() {Key = myAes.Key,Vector = myAes.IV});

                var path = args.Length == 0
                    ? $"{Environment.CurrentDirectory}/KeyVectorPair"
                    : args[0];

                locker.AcquireWriterLock(int.MaxValue);
                try
                {
                    await File.WriteAllTextAsync(path,serializeObject);
                }
                finally
                {
                    locker.ReleaseWriterLock();
                }
            }
        }
    }
}
