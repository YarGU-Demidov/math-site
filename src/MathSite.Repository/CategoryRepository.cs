using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface ICategoryRepository : IMathSiteEfCoreRepository<Category>
    {
    }

    public class CategoryRepository : MathSiteEfCoreRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(MathSiteDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}