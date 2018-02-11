using System;
using System.IO;
using Newtonsoft.Json;

namespace MathSite.Common.Crypto
{
    public class KeyVectorReader: IKeyVectorReader
    {
        public KeyVectorPair GetKeyVector()
        {
            var file = File.ReadAllText($"{Environment.CurrentDirectory}/KeyVectorPair");
            var obj = JsonConvert.DeserializeObject<KeyVectorPair>(file);
            return new KeyVectorPair{Key=obj.Key, Vector = obj.Vector};
        }
    }
}
