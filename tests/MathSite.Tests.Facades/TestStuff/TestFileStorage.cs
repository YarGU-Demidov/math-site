using System.IO;
using System.Threading.Tasks;
using MathSite.Common.FileStorage;

namespace MathSite.Tests.Facades.TestStuff
{
    public class TestFileStorage : IFileStorage
    {
        private readonly string _pathId;

        /// <summary>
        ///     Конструктор для сохранения.
        /// </summary>
        /// <param name="pathId"></param>
        public TestFileStorage(string pathId)
        {
            _pathId = pathId;
        }

        /// <summary>
        ///     Конструктор для загрузки данных.
        /// </summary>
        /// <param name="pathId"></param>
        /// <param name="data"></param>
        public TestFileStorage(string pathId, byte[] data)
        {
            _pathId = pathId;
            Data = data;
        }

        public byte[] Data { get; set; }

        public Task<string> SaveFileAsync(string fileName, byte[] data)
        {
            Data = data;
            return Task.FromResult(_pathId);
        }

        public async Task<string> SaveFileAsync(string fileName, Stream dataStream)
        {
            var data = new byte[dataStream.Length];

            using (dataStream)
            {
                await dataStream.WriteAsync(data, 0, data.Length);
            }

            Data = data;

            return _pathId;
        }

        public Stream TryGetFileStream(string fileId)
        {
            try
            {
                return GetFileStream(fileId);
            }
            catch (DirectoryNotFoundException)
            {
                return null;
            }
        }

        public Stream GetFileStream(string fileId)
        {
            if (fileId != _pathId)
                throw new DirectoryNotFoundException(fileId);

            return new MemoryStream(Data);
        }

        public Task Remove(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
}