using System;
using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.PostSettings;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.PostSettings
{
    public interface IPostSettingsFacade
    {
        Task UpdateForPost(
            Post post, 
            bool isCommentsAllowed, 
            bool canBeRated, 
            bool postOnStartPage, 
            Guid? previewImageId
        );
    }

    public class PostSettingsFacade : BaseFacade<IPostSettingRepository, PostSetting>, IPostSettingsFacade
    {
        public PostSettingsFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
            : base(repositoryManager, memoryCache)
        {
        }

        public async Task UpdateForPost(
            Post post, 
            bool isCommentsAllowed, 
            bool canBeRated, 
            bool postOnStartPage,
            Guid? previewImageId
        )
        {
            var spec = new PostSettingsForPostSpecification(post);
            var settings = await Repository.FirstOrDefaultAsync(spec) ?? new PostSetting();

            settings.CanBeRated = canBeRated;
            settings.IsCommentsAllowed = isCommentsAllowed;
            settings.PostOnStartPage = postOnStartPage;
            settings.PreviewImageId = previewImageId;

            await Repository.InsertOrUpdateAsync(settings);
        }
    }
}