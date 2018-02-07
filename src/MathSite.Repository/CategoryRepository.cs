using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }

    public class CategoryRepository : EfCoreRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(MathSiteDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}