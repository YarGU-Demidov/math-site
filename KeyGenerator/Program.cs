using System;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using MathSite.Common.Crypto;

namespace KeyGenerator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (Aes myAes = Aes.Create())
            {
                var serializeObject = JsonConvert.SerializeObject(new KeyVectorPair() {Key = myAes.Key,Vector = myAes.IV});
                File.WriteAllText($"{Environment.CurrentDirectory}/KeyVectorPair",serializeObject);
            }
        }
    }
}
