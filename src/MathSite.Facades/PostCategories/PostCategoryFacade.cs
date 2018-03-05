using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MathSite.Facades.PostCategories
{
    public class PostCategoryFacade : BaseFacade<IPostCategoryRepository, PostCategory>, IPostCategoryFacade
    {
        private readonly ILogger<IPostCategoryFacade> _postCategoryFacadeLogger;

        public PostCategoryFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache,
            ILogger<IPostCategoryFacade> postCategoryFacadeLogger)
            : base(repositoryManager, memoryCache)
        {
            _postCategoryFacadeLogger = postCategoryFacadeLogger;
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

        public IEnumerable<PostCategory> CreateRelation(Post post, IEnumerable<Category> categories)
        {
            return categories.Select(category => new PostCategory
            {
                Id = Guid.NewGuid(),
                CategoryId = category.Id,
                Category = category,
                PostId = post.Id,
                Post = post,
                CreationDate = DateTime.UtcNow
            });
        }
    }
}
