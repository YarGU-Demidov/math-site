using System;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Files
{
	public class FilesLogic : LogicBase<File>, IFilesLogic
	{
		private const string PersonNotFoundFormat = "Личность с Id='{0}' не найдена";
		private const string PersonAlreadyHasPhotoFormat = "Личность с Id='{0}' уже имеет фото";
		private const string FileNotFoundFormat = "Файл с Id='{0}' не найден";
		
		public FilesLogic(IMathSiteDbContext contextManager) 
			: base(contextManager)
		{
		}

		/// <summary>
		///     Асинхронно создает файл.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileName">Название файла.</param>
		/// <param name="filePath">Путь к файлу.</param>
		/// <param name="extension">Расширение файла.</param>
		/// <param name="personId">Идентификатор личности.</param>
		public async Task<Guid> CreateFileAsync(Guid currentUserId, string fileName, string filePath, string extension,
			Guid personId)
		{
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
		///     Асинхронно обновляет файл.
		/// </summary>
		/// <param name="fileId">Идентификатор файла.</param>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileName">Название файла.</param>
		/// <param name="filePath">Путь к файлу.</param>
		/// <param name="extension">Расширение файла.</param>
		public async Task UpdateFileAsync(Guid currentUserId, Guid fileId, string fileName, string filePath, string extension)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var file = await context.Files.FirstOrDefaultAsync(i => i.Id == fileId);
				if (file == null)
					throw new Exception(string.Format(FileNotFoundFormat, fileId));

				file.FileName = fileName;
				file.FilePath = filePath;
				file.Extension = extension;
			});
		}

		/// <summary>
		///     Асинхронно удаляет файл.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="fileId">Идентификатор файла.</param>
		public async Task DeleteFileAsync(Guid currentUserId, Guid fileId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var file = await context.Files.FirstOrDefaultAsync(i => i.Id == fileId);
				if (file == null)
					throw new Exception(string.Format(FileNotFoundFormat, fileId));

				context.Files.Remove(file);
			});
		}
	}
}