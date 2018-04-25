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
        Task UpdateForPostAsync(
            Post post, 
            bool isCommentsAllowed, 
            bool canBeRated, 
            bool postOnStartPage, 
            Guid? previewImageId
        );
        Task UpdateForPostAsync(
            Guid postId,
            DateTime eventDate,
            string eventLocation
        );

        Task<PostSetting> GetForPostAsync(Guid id);
        Task<Guid> CreateAsync(bool isCommentsAllowed, bool canBeRated, bool postOnStartPage, Guid? previewImageId);
        Task RemoveForPostAsync(Guid postId);
    }

    public class PostSettingsFacade : BaseMathFacade<IPostSettingRepository, PostSetting>, IPostSettingsFacade
    {
        public PostSettingsFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
            : base(repositoryManager, memoryCache)
        {
        }

        public async Task UpdateForPostAsync(
            Post post, 
            bool isCommentsAllowed, 
            bool canBeRated, 
            bool postOnStartPage,
            Guid? previewImageId
        )
        {
            var spec = new PostSettingsForPostSpecification(post);
            var settings = await Repository.FirstOrDefaultAsync(spec);

            settings.CanBeRated = canBeRated;
            settings.IsCommentsAllowed = isCommentsAllowed;
            settings.PostOnStartPage = postOnStartPage;
            settings.PreviewImageId = previewImageId;

            await Repository.InsertOrUpdateAsync(settings);
        }

        public async Task UpdateForPostAsync(Guid postId, DateTime eventDate, string eventLocation)
        {
            var spec = new PostSettingsForPostSpecification(postId);
            var settings = await Repository.FirstOrDefaultAsync(spec);

            settings.EventTime = eventDate;
            settings.EventLocation = eventLocation;

            await Repository.InsertOrUpdateAsync(settings);
        }

        public Task<PostSetting> GetForPostAsync(Guid id)
        {
            var spec = new PostSettingsForPostSpecification(id);
            return Repository.FirstOrDefaultAsync(spec);
        }

        public async Task<Guid> CreateAsync(bool isCommentsAllowed, bool canBeRated, bool postOnStartPage, Guid? previewImageId)
        {
            return await Repository.InsertAndGetIdAsync(new PostSetting
            {
                IsCommentsAllowed = isCommentsAllowed,
                CanBeRated = canBeRated,
                PostOnStartPage = postOnStartPage,
                PreviewImageId = previewImageId
            });
        }

        public async Task RemoveForPostAsync(Guid postId)
        {
            var spec = new PostSettingsForPostSpecification(postId);
            await Repository.DeleteAsync(spec);
        }
    }
}