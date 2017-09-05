using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.PostSettings
{
	public class PostSettingLogic : LogicBase<PostSetting>, IPostSettingLogic
	{
		public PostSettingLogic(MathSiteDbContext context) 
			: base(context)
		{
		}

		public async Task<Guid> CreatePostSettings(bool? isCommentsAllowed, bool? canBeRated, bool? postOnStartPage, Guid? previewImageId,
			Guid postTypeId)
		{
			var id = Guid.Empty;
			
			await UseContextWithSaveAsync(async context =>
			{
				var postSettings = new PostSetting
				{
					CanBeRated = canBeRated,
					IsCommentsAllowed = isCommentsAllowed,
					PostTypeId = postTypeId,
					PreviewImageId = previewImageId,
					PostOnStartPage = postOnStartPage
				};

				await context.PostSettings.AddAsync(postSettings);
				await context.SaveChangesAsync();

				id = postSettings.Id;
			});

			return id;
		}

		public async Task UpdatePostSettings(Guid id, bool? isCommentsAllowed, bool? canBeRated, bool? postOnStartPage, Guid? previewImageId,
			Guid postTypeId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var postSetting = await GetFromItemsAsync(posts => posts.FirstAsync(p => p.Id == id));

				postSetting.CanBeRated = canBeRated;
				postSetting.IsCommentsAllowed = isCommentsAllowed;
				postSetting.PostTypeId = postTypeId;
				postSetting.PreviewImageId = previewImageId;
				postSetting.PostOnStartPage = postOnStartPage;
				
				context.PostSettings.Update(postSetting);
			});
		}

		public async Task DeletePostSettings(Guid id)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var postSetting = await GetFromItemsAsync(ps => ps.FirstAsync(p => p.Id == id));

				context.PostSettings.Remove(postSetting);
			});
		}

		public async Task<PostSetting> TryGetByIdAsync(Guid id)
		{
			PostSetting post = null;
			
			await UseContextAsync(async context =>
			{
				post = await GetFromItemsAsync( 
					ps => ps.Where(p => p.Id == id).FirstOrDefaultAsync()
				);
			});

			return post;
		}
	}
}