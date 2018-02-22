using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.Groups;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Groups
{
    public interface IGroupsFacade : IFacade
    {
        Task<IEnumerable<Group>> GetGroupsByTypeAsync(string users);
    }

    public class GroupsFacade : BaseFacade<IGroupsRepository, Group>, IGroupsFacade
    {
        public GroupsFacade(
            IRepositoryManager repositoryManager, 
            IMemoryCache memoryCache
        ) : base(repositoryManager, memoryCache)
        {
        }

        public async Task<IEnumerable<Group>> GetGroupsByTypeAsync(string groupAlias)
        {
            var spec = new GroupWithTypeAliasSpecification(groupAlias);

            return await Repository.GetAllListAsync(spec);
        }
    }
}