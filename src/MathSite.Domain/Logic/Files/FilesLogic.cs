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
		
		public async Task<Guid> CreateAsync(string fileName, string filePath, string extension)
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

		public async Task UpdateAsync(Guid fileId, string fileName, string filePath, string extension)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var file = await TryGetByIdAsync(fileId);

				file.FileName = fileName;
				file.FilePath = filePath;
				file.Extension = extension;
			});
		}

		public async Task DeleteAsync(Guid fileId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var file = await TryGetByIdAsync(fileId);

				context.Files.Remove(file);
			});
		}

		public async Task<File> TryGetByIdAsync(Guid id)
		{
			File file = null;

			await UseContextAsync(async context =>
			{
				file = await GetFromItemsAsync(files => files.FirstOrDefaultAsync(i => i.Id == id));
			});

			return file;
		}
	}
}