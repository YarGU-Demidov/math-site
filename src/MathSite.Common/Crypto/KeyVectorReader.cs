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

        private static readonly object SyncRoot = new object();

        public KeyVectorReader() 
            : this($"{Environment.CurrentDirectory}/KeyVectorPair")
        {
        }

        public KeyVectorReader(string path)
        {
            _path = path;
        }

        public async Task<KeyVectorPair> GetKeyVectorAsync()
        {
            lock (SyncRoot)
            {
                using (var reader = File.OpenText(_path))
                {
                    var fileText =  reader.ReadToEndAsync().Result;
                    var obj = JsonConvert.DeserializeObject<KeyVectorPair>(fileText);
                    return new KeyVectorPair { Key = obj.Key, Vector = obj.Vector };
                }
            }
        }
    }
}
