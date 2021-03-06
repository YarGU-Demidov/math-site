﻿using System;
using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.PostSeoSettings;

namespace MathSite.Facades.PostSeoSettings
{
    public interface IPostSeoSettingsFacade
    {
        Task UpdateForPost(Post post, string url, string title, string description);
        Task<Guid> CreateAsync(string url, string title, string description);
        Task RemoveForPostAsync(Guid postId);
    }

    public class PostSeoSettingsFacade : BaseMathFacade<IPostSeoSettingsRepository, PostSeoSetting>, IPostSeoSettingsFacade
    {
        public PostSeoSettingsFacade(IRepositoryManager repositoryManager)
            : base(repositoryManager)
        {
        }

        public async Task UpdateForPost(Post post, string url, string title, string description)
        {
            var spec = new PostSeoSettingForPostSpecification(post);
            var settings = await Repository.WithPost().FirstOrDefaultAsync(spec) ?? new PostSeoSetting();

            settings.Url = url;
            settings.Title = title;
            settings.Description = description;
            settings.Post = settings.Post ?? post;

            await Repository.InsertOrUpdateAsync(settings);
        }

        public async Task<Guid> CreateAsync(string url, string title, string description)
        {
            return await Repository.InsertAndGetIdAsync(new PostSeoSetting
            {
                Url = url,
                Title = title,
                Description = description
            });
        }

        public async Task RemoveForPostAsync(Guid postId)
        {
            var spec = new PostSeoSettingForPostSpecification(postId);
            await Repository.DeleteAsync(spec);
        }
    }
}