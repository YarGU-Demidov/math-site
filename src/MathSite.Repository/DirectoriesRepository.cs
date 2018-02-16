using System.Linq;
using MathSite.Common.Extensions;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IDirectoriesRepository : IRepository<Directory>
    {
        IDirectoriesRepository WithFiles();
        IDirectoriesRepository WithDirectories();
    }

    public class DirectoriesRepository : EfCoreRepositoryBase<Directory>, IDirectoriesRepository
    {
        private IQueryable<Directory> _query;

        public DirectoriesRepository(MathSiteDbContext dbContext)
            : base(dbContext)
        {
        }

        public IDirectoriesRepository WithFiles()
        {
            _query = GetCurrentQuery().Include(directory => directory.Files);
            return this;
        }

        public IDirectoriesRepository WithDirectories()
        {
            _query = GetCurrentQuery().Include(directory => directory.Directories);
            return this;
        }

        public override IQueryable<Directory> GetAll()
        {
            if (_query.IsNull()) 
                return base.GetAll();

            var tmpQuery = _query;
            _query = null;
            return tmpQuery;
        }

        private IQueryable<Directory> GetCurrentQuery()
        {
            return _query ?? GetAll();
        }
    }
}