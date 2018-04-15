using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MathSite.Common.Crypto
{
    public class KeyVectorReader: IKeyVectorReader
    {
        private readonly string _path;

        public KeyVectorReader() 
            : this($"{Environment.CurrentDirectory}/KeyVectorPair")
        {
        }

        public KeyVectorReader(string path)
        {
            _path = path;
        }

        public KeyVectorPair GetKeyVector()
        {
            var fileText = File.ReadAllText(_path);
            var obj = JsonConvert.DeserializeObject<KeyVectorPair>(fileText);
            return new KeyVectorPair { Key = obj.Key, Vector = obj.Vector };
        }
    }
}
