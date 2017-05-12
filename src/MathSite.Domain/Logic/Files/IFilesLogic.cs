using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Models;

namespace MathSite.Domain.Logic.Files
{
	public interface IFilesLogic
	{
		/// <summary>
		///		Асинхронно создает файл.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileName">Название файла.</param>
		/// <param name="filePath">Путь к файлу.</param>
		/// <param name="extension">Расширение файла.</param>
		/// <param name="personId">Идентификатор личности.</param>
		Task<Guid> CreateFileAsync(Guid currentUserId, string fileName, string filePath, string extension, Guid personId);

		/// <summary>
		///		Асинхронно обновляет файл.
		/// </summary>
		/// <param name="fileId">Идентификатор файла.</param>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileName">Название файла.</param>
		/// <param name="filePath">Путь к файлу.</param>
		/// <param name="extension">Расширение файла.</param>
		Task UpdateFileAsync(Guid currentUserId, Guid fileId, string fileName, string filePath, string extension);

		/// <summary>
		///		Асинхронно удаляет файл.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileId">Идентификатор файла.</param>
		Task DeleteFileAsync(Guid currentUserId, Guid fileId);

		/// <summary>
		///		Возвращает результат из перечня файлов.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		TResult GetFromFiles<TResult>(Func<IQueryable<File>, TResult> getResult);

		/// <summary>
		///		Асинхронно возвращает результат из перечня файлов.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		Task<TResult> GetFromFilesAsync<TResult>(Func<IQueryable<File>, Task<TResult>> getResult);
	}
}