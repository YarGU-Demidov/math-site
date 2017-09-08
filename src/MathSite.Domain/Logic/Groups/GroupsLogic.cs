using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Groups
{
	public class GroupsLogic : LogicBase<Group>, IGroupsLogic
	{
		public GroupsLogic(MathSiteDbContext contextManager)
			: base(contextManager)
		{
		}

		public async Task<Guid> CreateGroupAsync(string name, string description, string alias, string groupTypeAlias,
			Guid? parentGroupId)
		{
			var groupId = Guid.Empty;
			await UseContextAsync(async context =>
			{
				var group = new Group(name, description, alias, groupTypeAlias, parentGroupId);

				context.Groups.Add(group);
				await context.SaveChangesAsync();

				groupId = group.Id;
			});

			return groupId;
		}

		public async Task UpdateGroupAsync(Guid groupId, string name, string description, string groupTypeAlias,
			Guid? parentGroupId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var group = await TryGetByIdAsync(groupId);

				group.Name = name;
				group.Description = description;
				group.ParentGroupId = parentGroupId;
			});
		}

		public async Task DeleteGroupAsync(Guid groupId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var group = await TryGetByIdAsync(groupId);

				context.Groups.Remove(group);
			});
		}

		public async Task<Group> TryGetByIdAsync(Guid id)
		{
			Group group = null;
			await UseContextAsync(async context =>
			{
				group = await GetFromItemsAsync(groups => groups.FirstOrDefaultAsync(g => g.Id == id));
			});

			return group;
		}

		public async Task<Group> TryGetByAliasAsync(string alias)
		{
			Group group = null;
			await UseContextAsync(async context =>
			{
				group = await GetFromItemsAsync(groups => groups.FirstOrDefaultAsync(g => g.Alias == alias));
			});

			return group;
		}

		public async Task<Group> TryGetGroupWithUsersByIdAsync(Guid id)
		{
			return await TryGetGroup(
				groups => groups.Include(group => group.Users).FirstOrDefaultAsync(group => group.Id == id)
			);
		}

		public async Task<Group> TryGetGroupWithUsersByAliasAsync(string alias)
		{
			return await TryGetGroup(
				groups => groups.Include(group => group.Users).FirstOrDefaultAsync(group => group.Alias == alias)
			);
		}

		public async Task<Group> TryGetGroupWithUsersWithRightsByIdAsync(Guid id)
		{
			return await TryGetGroup(
				groups => groups
					.Include(group => group.Users).ThenInclude(user => user.UserRights).ThenInclude(right => right.Right)
					.Include(group => group.GroupsRights).ThenInclude(right => right.Right)
					.FirstOrDefaultAsync(group => group.Id == id)
			);
		}

		public async Task<Group> TryGetGroupWithUsersWithRightsByAliasAsync(string alias)
		{
			return await TryGetGroup(
				groups => groups
					.Include(group => group.Users).ThenInclude(user => user.UserRights).ThenInclude(right => right.Right)
					.Include(group => group.GroupsRights).ThenInclude(right => right.Right)
					.FirstOrDefaultAsync(group => group.Alias == alias)
			);
		}

		private async Task<Group> TryGetGroup(
			Func<IQueryable<Group>, Task<Group>> getResultAsync
		)
		{
			return await GetFromItemsAsync(getResultAsync);
		}
	}
}