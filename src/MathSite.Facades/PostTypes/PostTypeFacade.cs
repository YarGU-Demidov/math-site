using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.PostTypes;

namespace MathSite.Facades.PostTypes
{
    public interface IPostTypeFacade
    {
        Task<PostType> GetFromPostAsync(Post post);
    }

    public class PostTypeFacade : BaseMathFacade<IPostTypeRepository, PostType>, IPostTypeFacade
    {
        public PostTypeFacade(IRepositoryManager repositoryManager) 
            : base(repositoryManager)
        {
        }

        public async Task<PostType> GetFromPostAsync(Post post)
        {
            var spec = new PostTypeForPostSpecification(post);
            return Repository.WithDefaultPostSettings().FirstOrDefault(spec);
        }
    }
}