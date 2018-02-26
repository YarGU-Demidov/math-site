using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.PostCategories
{
    public class PostCategoryFacade : BaseFacade<IPostCategoryRepository, PostCategory>, IPostCategoryFacade
    {
        public PostCategoryFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
            : base(repositoryManager, memoryCache)
        {
        }

        public async Task<IEnumerable<PostCategory>> CreateRelation(Post post, IEnumerable<Category> categories)
        {
            var postCategories = categories.Select(category => new PostCategory
            {
                Id = Guid.NewGuid(),
                CategoryId = category.Id,
                Category = category,
                PostId = post.Id,
                Post = post,
                CreationDate = DateTime.Now
            });

            foreach (var category in postCategories)
            {
                await Repository.InsertAsync(category);
            }

            return postCategories;
        }
    }
}
