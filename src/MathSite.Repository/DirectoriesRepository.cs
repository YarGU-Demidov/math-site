using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IDirectoriesRepository : IRepository<Directory>
    {
    }

    public class DirectoriesRepository : EfCoreRepositoryBase<Directory>, IDirectoriesRepository
    {
        public DirectoriesRepository(MathSiteDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}