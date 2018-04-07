using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.PostTypes;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.PostTypes
{
    public interface IPostTypeFacade
    {
        Task<PostType> GetFromPostAsync(Post post);
    }

    public class PostTypeFacade : BaseFacade<IPostTypeRepository, PostType>, IPostTypeFacade
    {
        public PostTypeFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache) 
            : base(repositoryManager, memoryCache)
        {
        }

        public async Task<PostType> GetFromPostAsync(Post post)
        {
            var spec = new PostTypeForPostSpecification(post);
            return Repository.WithDefaultPostSettings().FirstOrDefault(spec);
        }
    }
}