using System.IO;
using System.Threading.Tasks;

namespace MathSite.Common.FileStorage
{
    public interface IFileStorage
    {
        /// <summary>
        ///     Gets a file name and binary representation of file and returns path id for storaging link from outside.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="data">Binary representation of file.</param>
        /// <returns></returns>
        Task<string> SaveFileAsync(string fileName, byte[] data);
        
        /// <summary>
        ///     Gets a file name and binary representation of file and returns path id for storaging link from outside.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="data">Binary representation of file.</param>
        /// <returns></returns>
        Task<string> SaveFileAsync(string fileName, Stream data);

        /// <summary>
        ///     Trying to get file by given ID and returns binary representation of file.
        ///     When file cannot be fount in storage you'll get null result.
        /// </summary>
        /// <param name="fileId">File Id.</param>
        /// <returns>Binary representation of file.</returns>
        Stream TryGetFileStream(string fileId);

        /// <summary>
        ///     Trying to get file by given ID and returns binary representation of file
        ///     with throwing an exception of type <see cref="System.IO.FileNotFoundException" />
        ///     when file cannot be found.
        /// </summary>
        /// <param name="fileId">File Id.</param>
        /// <returns>Binary representation of file.</returns>
        /// <exception cref="System.IO.FileNotFoundException">Will be thrown when file not found in storage.</exception>
        Stream GetFileStream(string fileId);
    }
}