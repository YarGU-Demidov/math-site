using System;
using System.Threading.Tasks;
using MathSite.Domain.Logic.PostSeoSettings;
using Xunit;

namespace MathSite.Tests.Domain.Logic
{
	public class PostSeoSettingsTests : DomainTestsBase
	{
		[Fact]
		public async Task TryGet_ById_Found()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postSeoSettingsLogic = new PostSeoSettingsLogic(context);

				var id = await CreateSeoSettingAsync(postSeoSettingsLogic);

				var seoSetting = await postSeoSettingsLogic.TryGetByIdAsync(id);

				Assert.NotNull(seoSetting);
			});
		}

		[Fact]
		public async Task TryGet_ById_NotFound()
		{
			var id = Guid.NewGuid();

			await ExecuteWithContextAsync(async context =>
			{
				var postSeoSettingsLogic = new PostSeoSettingsLogic(context);
				var seoSetting = await postSeoSettingsLogic.TryGetByIdAsync(id);

				Assert.Null(seoSetting);
			});
		}

		[Fact]
		public async Task TryGet_ByUrl_Found()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postSeoSettingsLogic = new PostSeoSettingsLogic(context);

				const string url = "test-url-for-by-url-find";

				await CreateSeoSettingAsync(postSeoSettingsLogic, url);

				var seoSetting = await postSeoSettingsLogic.TryGetByUrlAsync(url);

				Assert.NotNull(seoSetting);
			});
		}

		[Fact]
		public async Task TryGet_ByUrl_NotFound()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postSeoSettingsLogic = new PostSeoSettingsLogic(context);
				var seoSetting = await postSeoSettingsLogic.TryGetByUrlAsync("wrong-url-tasdkfasdfaskfjhaslkf");

				Assert.Null(seoSetting);
			});
		}

		[Fact]
		public async Task CreatePersonTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postSeoSettingsLogic = new PostSeoSettingsLogic(context);

				var url = "test-seo-setting-url-new";
				var title = "test-seo-setting-title-new";
				var description = "test-seo-setting-description-new";
				
				var id = await CreateSeoSettingAsync(
					postSeoSettingsLogic,
					url,
					title,
					description
				);

				var seoSetting = await postSeoSettingsLogic.TryGetByIdAsync(id);

				Assert.NotNull(seoSetting);
				Assert.Equal(url, seoSetting.Url);
				Assert.Equal(title, seoSetting.Title);
				Assert.Equal(description, seoSetting.Description);
			});
		}

		[Fact]
		public async Task UpdatePersonTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postSeoSettingsLogic = new PostSeoSettingsLogic(context);

				var url = "test-seo-setting-url-new";
				var title = "test-seo-setting-title-new";
				var description = "test-seo-setting-description-new";

				var id = await CreateSeoSettingAsync(postSeoSettingsLogic);

				await postSeoSettingsLogic.UpdateAsync(id, url, title, description);

				var seoSetting = await postSeoSettingsLogic.TryGetByIdAsync(id);

				Assert.NotNull(seoSetting);
				Assert.Equal(url, seoSetting.Url);
				Assert.Equal(title, seoSetting.Title);
				Assert.Equal(description, seoSetting.Description);
			});
		}

		[Fact]
		public async Task DeletePersonTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postSeoSettingsLogic = new PostSeoSettingsLogic(context);

				var id = await CreateSeoSettingAsync(postSeoSettingsLogic);

				await postSeoSettingsLogic.DeleteAsync(id);

				var seoSetting = await postSeoSettingsLogic.TryGetByIdAsync(id);

				Assert.Null(seoSetting);
			});
		}

		private async Task<Guid> CreateSeoSettingAsync(
			IPostSeoSettingsLogic logic,
			string url = null,
			string title = null,
			string description = null
		)
		{
			var salt = Guid.NewGuid();

			url = url ?? $"test-seo-setting-url-{salt}";
			title = title ?? $"test-seo-setting-title-{salt}";
			description = description ?? $"test-seo-setting-description-{salt}";

			return await logic.CreateAsync(url, title, description);
		}
	}
}