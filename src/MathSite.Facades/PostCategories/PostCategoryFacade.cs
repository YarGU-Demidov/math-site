using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.PostCategories;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.PostCategories
{
    public class PostCategoryFacade : BaseFacade<IPostCategoryRepository, PostCategory>, IPostCategoryFacade
    {
        public PostCategoryFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
            : base(repositoryManager, memoryCache)
        {
        }

        public async Task<PostCategory> GetPostCategoryAsync(Guid postId)
        {
            return await RepositoryManager.PostCategoryRepository.FirstOrDefaultAsync(c => c.PostId == postId);
        }

        public async Task<PostCategory> GetPostCategoryAsync(Guid postId, Guid categoryId)
        {
            return await RepositoryManager.PostCategoryRepository.FirstOrDefaultAsync(c => c.PostId == postId && c.CategoryId == categoryId);
        }

        public async Task<Guid> CreatePostCategoryAsync(PostCategory postCategory)
        {
            return await RepositoryManager.PostCategoryRepository.InsertAndGetIdAsync(postCategory);
        }

        public async Task<PostCategory> UpdatePostCategoryAsync(PostCategory postCategory)
        {
            return await RepositoryManager.PostCategoryRepository.UpdateAsync(postCategory);
        }

        public async Task DeletePostCategoryAsync(Guid postId)
        {
            await RepositoryManager.PostCategoryRepository.DeleteAsync(c => c.PostId == postId);         
        }

        public async Task<IEnumerable<PostCategory>> CreateRelation(Post post, IEnumerable<Category> categories)
        {
            var postCategories = categories.Select(category => new PostCategory
            {
                CategoryId = category.Id,
                Category = category,
                PostId = post.Id,
                Post = post
            }).ToArray();

            foreach (var postCategory in postCategories)
            {
                // TODO: придумать метод, который будет массово такие вещи обрабатывать (удаление, обновление, создание)
                // TODO: возможное и вероятное падение производительности тут
                await Repository.InsertOrUpdateAsync(postCategory);
            }

            return postCategories;
        }

        public async Task DeleteAllRelations(Post post)
        {
            var spec = new PostCategoryWithPost(post);
            var categories = await Repository.GetAllListAsync(spec);

            foreach (var postCategory in categories)
            {
                await Repository.DeleteAsync(postCategory);
            }
        }
    }
}
