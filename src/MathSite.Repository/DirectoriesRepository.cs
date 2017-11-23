using System.Linq;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

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

        public override IQueryable<Directory> GetAll()
        {
            return base.GetAll()
                .Include(d => d.RootDirectory)
                .Include(d => d.Files)
                .Include(d => d.Directories);
        }
    }
}