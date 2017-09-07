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
		public FilesLogic(MathSiteDbContext contextManager) 
			: base(contextManager)
		{
		}
		
		public async Task<Guid> CreateFileAsync(string fileName, string filePath, string extension)
		{
			var fileId = Guid.Empty;
			await UseContextAsync(async context =>
			{
				var file = new File(fileName, filePath, extension);

				context.Files.Add(file);
				await context.SaveChangesAsync();

				fileId = file.Id;
			});

			return fileId;
		}

		public async Task UpdateFileAsync(Guid fileId, string fileName, string filePath, string extension)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var file = await TryGetFileByIdAsync(fileId);

				file.FileName = fileName;
				file.FilePath = filePath;
				file.Extension = extension;
			});
		}

		public async Task DeleteFileAsync(Guid fileId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var file = await TryGetFileByIdAsync(fileId);

				context.Files.Remove(file);
			});
		}

		public async Task<File> TryGetFileByIdAsync(Guid id)
		{
			File file = null;

			await UseContextAsync(async context =>
			{
				file = await context.Files.FirstOrDefaultAsync(i => i.Id == id);
			});

			return file;
		}
	}
}