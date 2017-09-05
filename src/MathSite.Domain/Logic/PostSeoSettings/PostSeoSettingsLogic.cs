using System;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.PostSeoSettings
{
	public class PostSeoSettingsLogic : LogicBase<PostSeoSetting>, IPostSeoSettingsLogic
	{
		public PostSeoSettingsLogic(MathSiteDbContext context) 
			: base(context)
		{
		}

		public async Task<Guid> CreateSeoSettingsAsync(string url, string title, string description)
		{
			var id = Guid.Empty;
			await UseContextWithSaveAsync(async context =>
			{
				var item = new PostSeoSetting
				{
					Description = description,
					Title = title,
					Url = url
				};

				await context.PostSeoSettings.AddAsync(item);
			});
			return id;
		}

		public async Task UpdateSeoSettingsAsync(Guid id, string url, string title, string description)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var item = await context.PostSeoSettings.FirstAsync(setting => setting.Id == id);
				
				item.Url = url;
				item.Description = description;
				item.Title = title;
				
				context.PostSeoSettings.Update(item);
			});
		}

		public async Task DeleteSeoSettingsAsync(Guid id)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var item = await GetFromItemsAsync(settings => settings.FirstAsync(setting => setting.Id == id));
				context.PostSeoSettings.Remove(item);
			});
		}

		public async Task<PostSeoSetting> TryGetByIdAsync(Guid id)
		{
			PostSeoSetting setting = null;
			await UseContextAsync(async context =>
			{
				setting = await GetFromItemsAsync(settings => settings.FirstOrDefaultAsync(s => s.Id == id));
			});
			return setting;
		}

		public async Task<PostSeoSetting> TryGetByUrlAsync(string url)
		{
			PostSeoSetting setting = null;
			await UseContextAsync(async context =>
			{
				setting = await GetFromItemsAsync(settings => settings.FirstOrDefaultAsync(s => s.Url == url));
			});
			return setting;
		}
	}
}