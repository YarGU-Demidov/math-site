using System.Linq;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IFilesRepository : IMathSiteEfCoreRepository<File>
    {
        IFilesRepository WithPerson();
        IFilesRepository WithPostSetting();
        IFilesRepository WithPostAttachments();
    }

    public class FilesRepository : MathSiteEfCoreRepositoryBase<File>, IFilesRepository
    {
        public FilesRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<File> GetAll()
        {
            if (!QueryInitialized) 
                return base.GetAll()
                    .Include(file => file.Directory).ThenInclude(d => d.Directories)
                    .Include(file => file.Directory).ThenInclude(d => d.RootDirectory)
                    .Include(file => file.Directory).ThenInclude(d => d.Files);

            var tmpQuery = GetCurrentQuery();
            SetCurrentQuery(null);
            return tmpQuery;
        }

        public IFilesRepository WithPerson()
        {
            SetCurrentQuery(GetCurrentQuery().Include(file => file.Person));
            return this;
        }

        public IFilesRepository WithPostSetting()
        {
            SetCurrentQuery(GetCurrentQuery().Include(file => file.PostSettings));
            return this;
        }

        public IFilesRepository WithPostAttachments()
        {
            SetCurrentQuery(GetCurrentQuery().Include(file => file.PostAttachments));
            return this;
        }
    }
}