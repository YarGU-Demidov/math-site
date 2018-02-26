using MathSite.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathSite.Facades.PostCategories
{
    public interface IPostCategoryFacade : IFacade
    {
        Task<IEnumerable<PostCategory>> CreateRelation(Post post, IEnumerable<Category> categories);
    }
}
