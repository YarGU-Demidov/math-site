using System;
using MathSite.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathSite.Facades.PostCategories
{
    public interface IPostCategoryFacade : IFacade
    {
        Task<PostCategory> GetPostCategoryAsync(Guid postId);
        Task<PostCategory> GetPostCategoryAsync(Guid postId, Guid categoryId);
        Task<Guid> CreatePostCategoryAsync(PostCategory postCategory);
        Task<PostCategory> UpdatePostCategoryAsync(PostCategory postCategory);
        Task DeletePostCategoryAsync(Guid postId);
        IEnumerable<PostCategory> CreateRelation(Post post, IEnumerable<Category> categories);
    }
}
