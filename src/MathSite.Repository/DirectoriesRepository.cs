using System;
using System.Threading.Tasks;
using MathSite.Common.Extensions;
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
        IDirectoriesRepository WithRoot();
        Task ChangeRootAsync(Guid? fromId, Guid? toId);
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

        public IDirectoriesRepository WithRoot()
        {
            SetCurrentQuery(GetCurrentQuery().Include(directory => directory.RootDirectory));
            return this;
        }

        public override Task DeleteAsync(Directory entity)
        {
            return DeleteAsync(entity.Id);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await GetDbContext().ExecuteSqlAsync($"DELETE FROM public.\"Directory\" WHERE \"Id\" = '{id}'");
        }

        public async Task ChangeRootAsync(Guid? fromId, Guid? toId)
        {
            var to = toId.HasValue ? $"'{toId}'" : "NULL";
            var from = fromId.HasValue ? $"'{fromId}'" : "NULL";

            var dirSql = $"UPDATE public.\"Directory\" SET \"RootDirectoryId\" = {to} WHERE \"RootDirectoryId\" = {from}";
            var filesSql = $"UPDATE public.\"File\" SET \"DirectoryId\" = {to} WHERE \"DirectoryId\" = {from}";

            await GetDbContext().ExecuteSqlAsync(dirSql);
            await GetDbContext().ExecuteSqlAsync(filesSql);
        }
    }
}