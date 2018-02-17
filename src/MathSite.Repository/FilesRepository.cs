using System.Linq;
using MathSite.Common.Extensions;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IFilesRepository : IRepository<File>
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
            if (QueryBuilder.IsNull()) 
                return base.GetAll()
                    .Include(file => file.Directory).ThenInclude(d => d.Directories)
                    .Include(file => file.Directory).ThenInclude(d => d.RootDirectory)
                    .Include(file => file.Directory).ThenInclude(d => d.Files);

            var tmpQuery = QueryBuilder;
            QueryBuilder = null;
            return tmpQuery;
        }

        public IFilesRepository WithPerson()
        {
            QueryBuilder = GetCurrentQuery().Include(file => file.Person);
            return this;
        }

        public IFilesRepository WithPostSetting()
        {
            QueryBuilder = GetCurrentQuery().Include(file => file.PostSettings);
            return this;
        }

        public IFilesRepository WithPostAttachments()
        {
            QueryBuilder = GetCurrentQuery().Include(file => file.PostAttachments);
            return this;
        }
    }
}