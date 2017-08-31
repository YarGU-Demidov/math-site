using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Domain.LogicValidation;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Files
{
	public class FilesLogic : LogicBase, IFilesLogic
	{
		private const string PersonNotFoundFormat = "Личность с Id='{0}' не найдена";
		private const string PersonAlreadyHasPhotoFormat = "Личность с Id='{0}' уже имеет фото";
		private const string FileNotFoundFormat = "Файл с Id='{0}' не найден";

		private readonly ICurrentUserAccessValidation _userValidation;

		public FilesLogic(IMathSiteDbContext contextManager,
			ICurrentUserAccessValidation userValidation) : base(contextManager)
		{
			_userValidation = userValidation;
		}

		/// <summary>
		///		Асинхронно создает файл.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileName">Название файла.</param>
		/// <param name="filePath">Путь к файлу.</param>
		/// <param name="extension">Расширение файла.</param>
		/// <param name="personId">Идентификатор личности.</param>
		public async Task<Guid> CreateFileAsync(Guid currentUserId, string fileName, string filePath, string extension,
			Guid personId)
		{
			_userValidation.CheckCurrentUserExistence(currentUserId);
			await _userValidation.CheckCurrentUserRightsAsync(currentUserId);

			var fileId = Guid.Empty;
			await UseContextAsync(async context =>
			{
				var person = context.Persons.FirstOrDefaultAsync(p => p.Id == personId).Result;
				if (person == null)
					throw new Exception(string.Format(PersonNotFoundFormat, personId));
				if (person.PhotoId != null)
					throw new Exception(string.Format(PersonAlreadyHasPhotoFormat, personId));

				var file = new File(fileName, filePath, extension, person);

				context.Files.Add(file);
				await context.SaveChangesAsync();

				fileId = file.Id;
			});

			return fileId;
		}

		/// <summary>
		///		Асинхронно обновляет файл.
		/// </summary>
		/// <param name="fileId">Идентификатор файла.</param>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileName">Название файла.</param>
		/// <param name="filePath">Путь к файлу.</param>
		/// <param name="extension">Расширение файла.</param>
		public async Task UpdateFileAsync(Guid currentUserId, Guid fileId, string fileName, string filePath, string extension)
		{
			_userValidation.CheckCurrentUserExistence(currentUserId);
			await _userValidation.CheckCurrentUserRightsAsync(currentUserId);

			await UseContextAsync(async context =>
			{
				var file = await context.Files.FirstOrDefaultAsync(i => i.Id == fileId);
				if (file == null)
					throw new Exception(string.Format(FileNotFoundFormat, fileId));

				file.FileName = fileName;
				file.FilePath = filePath;
				file.Extension = extension;

				await context.SaveChangesAsync();
			});
		}

		/// <summary>
		///		Асинхронно удаляет файл.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileId">Идентификатор файла.</param>
		public async Task DeleteFileAsync(Guid currentUserId, Guid fileId)
		{
			_userValidation.CheckCurrentUserExistence(currentUserId);
			await _userValidation.CheckCurrentUserRightsAsync(currentUserId);

			await UseContextAsync(async context =>
			{
				var file = await context.Files.FirstOrDefaultAsync(i => i.Id == fileId);
				if (file == null)
					throw new Exception(string.Format(FileNotFoundFormat, fileId));

				context.Files.Remove(file);
				await context.SaveChangesAsync();
			});

		}

		/// <summary>
		///		Возвращает результат из перечня файлов.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		public TResult GetFromFiles<TResult>(Func<IQueryable<File>, TResult> getResult)
		{
			return GetFromItems(getResult);
		}

		/// <summary>
		///		Асинхронно возвращает результат из перечня файлов.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		public async Task<TResult> GetFromFilesAsync<TResult>(Func<IQueryable<File>, Task<TResult>> getResult)
		{
			return await GetFromItems(getResult);
		}
	}
}