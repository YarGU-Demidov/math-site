using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IDirectoriesRepository : IMathSiteEfCoreRepository<Directory>
    {
        IDirectoriesRepository WithFiles();
        IDirectoriesRepository WithDirectories();
    }

    public class DirectoriesRepository : MathSiteEfCoreRepositoryBase<Directory>, IDirectoriesRepository
    {
        public DirectoriesRepository(MathSiteDbContext dbContext)
            : base(dbContext)
        {
        }

        public IDirectoriesRepository WithFiles()
        {
            SetCurrentQuery(GetCurrentQuery().Include(directory => directory.Files));
            return this;
        }

        public IDirectoriesRepository WithDirectories()
        {
            SetCurrentQuery(GetCurrentQuery().Include(directory => directory.Directories));
            return this;
        }
    }
}