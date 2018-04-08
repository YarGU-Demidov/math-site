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
    public interface IPostCategoryFacade : IFacade
    {
        Task<PostCategory> GetPostCategoryAsync(Guid postId, Guid categoryId);
        Task<IEnumerable<PostCategory>> CreateRelation(Post post, IEnumerable<Category> categories);
        Task UpdateRelations(Post post, IEnumerable<Category> categories);
        Task DeleteAllRelations(Post post);
    }

    public class PostCategoryFacade : BaseFacade<IPostCategoryRepository, PostCategory>, IPostCategoryFacade
    {
        public PostCategoryFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
            : base(repositoryManager, memoryCache)
        {
        }

        public async Task<PostCategory> GetPostCategoryAsync(Guid postId, Guid categoryId)
        {
            return await Repository.FirstOrDefaultAsync(c => c.PostId == postId && c.CategoryId == categoryId);
        }

        public async Task<IEnumerable<PostCategory>> CreateRelation(Post post, IEnumerable<Category> categories)
        {
            var postCategories = categories.Select(category => new PostCategory
            {
                CategoryId = category.Id,
                PostId = post.Id
            }).ToArray();

            foreach (var postCategory in postCategories)
            {
                // TODO: придумать метод, который будет массово такие вещи обрабатывать (удаление, обновление, создание)
                // TODO: возможное и вероятное падение производительности тут
                await Repository.InsertOrUpdateAsync(postCategory);
            }

            return postCategories;
        }

        public async Task UpdateRelations(Post post, IEnumerable<Category> categories)
        {
            var spec = new PostCategoryWithPostSpecification(post);
            var oldCategories = await Repository.GetAllListAsync(spec);

            var currentCategories = categories.ToArray();

            var newCategories = currentCategories.Where(category => oldCategories.All(postCategory => postCategory.CategoryId != category.Id));
            var deletedCategories = oldCategories.Where(category => currentCategories.All(category1 => category.Id != category1.Id));

            foreach (var deletedCategory in deletedCategories)
            {
                await Repository.DeleteAsync(deletedCategory);
            }

            foreach (var newCategory in newCategories)
            {
                await Repository.InsertAsync(new PostCategory
                {
                    Category = newCategory,
                    Post = post
                });
            }
        }

        public async Task DeleteAllRelations(Post post)
        {
            var spec = new PostCategoryWithPostSpecification(post);
            var categories = await Repository.GetAllListAsync(spec);

            foreach (var postCategory in categories)
            {
                await Repository.DeleteAsync(postCategory);
            }
        }
    }
}
