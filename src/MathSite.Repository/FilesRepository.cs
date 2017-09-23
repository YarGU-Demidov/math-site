using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IFilesRepository : IRepository<File>
    {
    }

    public class FilesRepository : EfCoreRepositoryBase<File>, IFilesRepository
    {
        public FilesRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}