using System;
using System.Threading.Tasks;
using MathSite.Domain.Logic.Files;
using Xunit;

namespace MathSite.Tests.Domain.Logic
{
	public class FilesLogicTests : DomainTestsBase
	{
		[Fact]
		public async Task TryGet_Found()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var filesLogic = new FilesLogic(context);

				var id = await CreateFileItemAsync(filesLogic);

				var file = await filesLogic.TryGetByIdAsync(id);

				Assert.NotNull(file);
			});
		}

		[Fact]
		public async Task TryGet_NotFound()
		{
			var id = Guid.NewGuid();

			await ExecuteWithContextAsync(async context =>
			{
				var filesLogic = new FilesLogic(context);
				var file = await filesLogic.TryGetByIdAsync(id);

				Assert.Null(file);
			});
		}

		[Fact]
		public async Task CreateFileTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var filesLogic = new FilesLogic(context);

				const string fileName = "test-file-name";
				const string filePath = "test-file-path";
				const string fileExtension = "test-file-extension";

				var id = await CreateFileItemAsync(filesLogic, fileName, filePath, fileExtension);

				Assert.NotEqual(Guid.Empty, id);

				var file = await filesLogic.TryGetByIdAsync(id);
				Assert.NotNull(file);

				Assert.Equal(fileName,file.FileName);
				Assert.Equal(filePath,file.FilePath);
				Assert.Equal(fileExtension, file.Extension);
				Assert.Equal(DateTime.Today, file.DateAdded.Date);
			});
		}

		[Fact]
		public async Task UpdateFileTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var filesLogic = new FilesLogic(context);

				const string fileName = "test-file-name-new";
				const string filePath = "test-file-path-new";
				const string fileExtension = "test-file-extension-new";

				var id = await CreateFileItemAsync(filesLogic);

				await filesLogic.UpdateFileAsync(id, fileName, filePath, fileExtension);

				var file = await filesLogic.TryGetByIdAsync(id);

				Assert.NotNull(file);

				Assert.Equal(fileName, file.FileName);
				Assert.Equal(filePath, file.FilePath);
				Assert.Equal(fileExtension, file.Extension);
			});
		}

		[Fact]
		public async Task DeleteFileTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var filesLogic = new FilesLogic(context);

				var id = await CreateFileItemAsync(filesLogic);

				await filesLogic.DeleteFileAsync(id);

				var file = await filesLogic.TryGetByIdAsync(id);

				Assert.Null(file);
			});
		}

		private async Task<Guid> CreateFileItemAsync(IFilesLogic logic, string name = null, string path = null, string extension = null)
		{
			var salt = Guid.NewGuid();

			var fileName = name ?? $"test-file-name-{salt}";
			var filePath = path ?? $"test-file-path-{salt}";
			var fileExtension = extension ?? $"test-file-extension-{salt}";

			return await logic.CreateFileAsync(fileName, filePath, fileExtension);
		}
	}
}