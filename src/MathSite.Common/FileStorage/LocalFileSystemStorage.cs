using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace MathSite.Common.FileStorage
{
    public class LocalFileSystemStorage : IFileStorage
    {
        private static string _webRootPath;
        private static string SavePath => Path.Combine(_webRootPath, "uploads");

        public LocalFileSystemStorage(IHostingEnvironment hostingEnvironment)
        {
            _webRootPath = _webRootPath ?? hostingEnvironment.WebRootPath;

            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);
        }

        public async Task<string> SaveFileAsync(string fileName, byte[] data)
        {
            var newFileName = $"{GetFileDate()}_{Guid.NewGuid()}_{fileName}";
            var filePath = Path.Combine(SavePath, newFileName);
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await fs.WriteAsync(data, 0, data.Length);

                return newFileName;
            }
        }

        public async Task<string> SaveFileAsync(string fileName, Stream data)
        {
            var newFileName = $"{GetFileDate()}_{Guid.NewGuid()}_{fileName}";
            var filePath = Path.Combine(SavePath, newFileName);
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await data.CopyToAsync(fs);

                return newFileName;
            }
        }

        public Stream TryGetFileStream(string fileId)
        {
            try
            {
                return GetFileStream(fileId);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public Stream GetFileStream(string fileId)
        {
            return new FileStream(Path.Combine(SavePath, fileId), FileMode.Open, FileAccess.Read);
        }

        public Task Remove(string filePath)
        {
            File.Delete(Path.Combine(SavePath, filePath));
            return Task.CompletedTask;
        }

        private static string GetFileDate()
        {
            var now = DateTime.UtcNow;

            return $"{now:dd-MM-yyyy}_{now.Hour}-{now.Minute}-{now.Second}-{now.Millisecond}";
        }
    }
}