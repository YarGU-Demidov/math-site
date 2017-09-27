using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Facades.Posts
{
    public interface IPostsFacade
    {
        Task<int> GetNewsPagesCountAsync();
        Task<Post> GetNewsPostByUrlAsync(string url);
        Task<Post> GetStaticPageByUrlAsync(string url);
        Task<IEnumerable<Post>> GetNewsAsync(int page);
        Task<IEnumerable<Post>> GetLastSelectedForMainPagePostsAsync(int count);
        Task<IEnumerable<Post>> GetAllNewsAsync(int page, int perPage, bool includeDeleted = false, bool onlyDeleted = false);
        Task<Guid> CreatePostAsync(Post post, PostSeoSetting seoSettings, PostSetting settings = null);
    }
}