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
        private static object SyncRoot;
        public static async Task Main(string[] args)
        {
            if(SyncRoot==null)
                SyncRoot = new object();
            using (Aes myAes = Aes.Create())
            {
                var serializeObject = JsonConvert.SerializeObject(new KeyVectorPair() {Key = myAes.Key,Vector = myAes.IV});

                var path = args.Length == 0
                    ? $"{Environment.CurrentDirectory}/KeyVectorPair"
                    : args[0];
                lock (SyncRoot)
                {
                    File.WriteAllText(path, serializeObject);
                }
            }
        }
    }
}
