using System;
using System.Threading.Tasks;
using MathSite.Domain.Logic.Files;
using MathSite.Domain.Logic.PostSettings;
using Xunit;

namespace MathSite.Tests.Domain.Logic
{
	public class PostSettingsTests : DomainTestsBase
	{
		[Fact]
		public async Task TryGet_ById_Found()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var settingLogic = new PostSettingLogic(context);
				var filesLogic = new FilesLogic(context);

				var id = await CreateSettingAsync(settingLogic, filesLogic);

				var setting = await settingLogic.TryGetByIdAsync(id);

				Assert.NotNull(setting);
			});
		}

		[Fact]
		public async Task TryGet_ById_NotFound()
		{
			var id = Guid.NewGuid();

			await ExecuteWithContextAsync(async context =>
			{
				var settingLogic = new PostSettingLogic(context);

				var setting = await settingLogic.TryGetByIdAsync(id);

				Assert.Null(setting);
			});
		}

		[Fact]
		public async Task CreatePersonTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var settingLogic = new PostSettingLogic(context);
				var filesLogic = new FilesLogic(context);

				var image = await filesLogic.CreateAsync("test-file-name", "filepath", "ext");

				var id = await CreateSettingAsync(
					settingLogic,
					filesLogic,
					true,
					true,
					true,
					image
				);

				var setting = await settingLogic.TryGetByIdAsync(id);

				Assert.NotNull(setting);
				Assert.True(setting.CanBeRated);
				Assert.True(setting.IsCommentsAllowed);
				Assert.True(setting.PostOnStartPage);
				Assert.Equal(setting.PreviewImageId, image);
			});
		}

		[Fact]
		public async Task UpdatePersonTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var settingLogic = new PostSettingLogic(context);
				var filesLogic = new FilesLogic(context);

				var id = await CreateSettingAsync(settingLogic, filesLogic);

				var image = await filesLogic.CreateAsync("test-file-name", "filepath", "ext");

				await settingLogic.UpdateAsync(id, false, false, false, image);

				var setting = await settingLogic.TryGetByIdAsync(id);

				Assert.NotNull(setting);
				Assert.False(setting.CanBeRated);
				Assert.False(setting.IsCommentsAllowed);
				Assert.False(setting.PostOnStartPage);
				Assert.NotEqual(Guid.Empty, setting.PreviewImageId);
			});
		}

		[Fact]
		public async Task DeletePersonTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var settingLogic = new PostSettingLogic(context);
				var filesLogic = new FilesLogic(context);

				var id = await CreateSettingAsync(settingLogic, filesLogic);

				await settingLogic.DeleteAsync(id);

				var setting = await settingLogic.TryGetByIdAsync(id);

				Assert.Null(setting);
			});
		}
		
		private async Task<Guid> CreateSettingAsync(
			IPostSettingLogic logic,
			IFilesLogic filesLogic,
			bool? canBeRated = null,
			bool? canBeCommented = null,
			bool? onStartPage = null,
			Guid? previewImageId = null
		)
		{
			var rated = canBeRated ?? true;
			var commented = canBeCommented ?? true;
			var startPage = onStartPage ?? true;
			previewImageId = previewImageId ?? await filesLogic.CreateAsync("preview image file", "/home/uploads/preview.png", "png");

			return await logic.CreateAsync(commented, rated, startPage, previewImageId);
		}
	}
}