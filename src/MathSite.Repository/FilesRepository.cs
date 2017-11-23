using System.Linq;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

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

        public override IQueryable<File> GetAll()
        {
            return base.GetAll()
                .Include(file => file.Directory).ThenInclude(d => d.Directories)
                .Include(file => file.Directory).ThenInclude(d => d.RootDirectory)
                .Include(file => file.Directory).ThenInclude(d => d.Files);
        }
    }
}