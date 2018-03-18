using System;
using System.IO;
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

        public async Task<KeyVectorPair> GetKeyVectorAsync()
        {
            using (var reader = File.OpenText(_path))
            {
                var fileText = await reader.ReadToEndAsync();
                var obj = JsonConvert.DeserializeObject<KeyVectorPair>(fileText);
                return new KeyVectorPair { Key = obj.Key, Vector = obj.Vector };
            }
        }
    }
}
