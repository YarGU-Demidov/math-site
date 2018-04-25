using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MathSite.Common.Crypto;

namespace KeyGenerator
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using (Aes myAes = Aes.Create())
            {
                var serializeObject = JsonConvert.SerializeObject(new KeyVectorPair() {Key = myAes.Key,Vector = myAes.IV});

                var path = args.Length == 0
                    ? $"{Environment.CurrentDirectory}/KeyVectorPair"
                    : args[0];
                File.WriteAllText(path, serializeObject);
            }
        }
    }
}
