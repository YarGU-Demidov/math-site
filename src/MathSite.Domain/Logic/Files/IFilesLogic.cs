using System;
using System.Threading.Tasks;

namespace MathSite.Domain.Logic.Files
{
	public interface IFilesLogic
	{
		/// <summary>
		///     Асинхронно создает файл.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileName">Название файла.</param>
		/// <param name="filePath">Путь к файлу.</param>
		/// <param name="extension">Расширение файла.</param>
		/// <param name="personId">Идентификатор личности.</param>
		Task<Guid> CreateFileAsync(Guid currentUserId, string fileName, string filePath, string extension, Guid personId);

		/// <summary>
		///     Асинхронно обновляет файл.
		/// </summary>
		/// <param name="fileId">Идентификатор файла.</param>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileName">Название файла.</param>
		/// <param name="filePath">Путь к файлу.</param>
		/// <param name="extension">Расширение файла.</param>
		Task UpdateFileAsync(Guid currentUserId, Guid fileId, string fileName, string filePath, string extension);

		/// <summary>
		///     Асинхронно удаляет файл.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileId">Идентификатор файла.</param>
		Task DeleteFileAsync(Guid currentUserId, Guid fileId);
	}
}