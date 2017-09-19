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
    }
}