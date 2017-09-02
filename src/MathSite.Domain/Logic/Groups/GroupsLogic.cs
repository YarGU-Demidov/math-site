using System;
using System.Collections.Generic;
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
		public GroupsLogic(IMathSiteDbContext contextManager)
			: base(contextManager)
		{
		}

		/// <summary>
		///     Асинхронно создает группу.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="name">Наименование группы.</param>
		/// <param name="description">Описание группы.</param>
		/// <param name="groupTypeId">Идентификатор типа группы.</param>
		/// <param name="parentGroupId">Идентификатор родительской группы.</param>
		public async Task<Guid> CreateGroupAsync(Guid currentUserId, string name, string description, Guid groupTypeId,
			Guid? parentGroupId)
		{
			var groupId = Guid.Empty;
			await UseContextAsync(async context =>
			{
				var group = new Group(name, description, groupTypeId, parentGroupId);

				context.Groups.Add(group);
				await context.SaveChangesAsync();

				groupId = group.Id;
			});

			return groupId;
		}

		/// <summary>
		///     Асинхронно обновляет группу.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <param name="name">Наименование группы.</param>
		/// <param name="description">Описание группы.</param>
		/// <param name="groupTypeId">Идентификатор типа группы.</param>
		/// <param name="parentGroupId">Идентификатор родительской группы.</param>
		public async Task UpdateGroupAsync(Guid currentUserId, Guid groupId, string name, string description,
			Guid groupTypeId, Guid? parentGroupId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == groupId);

				group.Name = name;
				group.Description = description;
				group.Alias = name;
				group.GroupTypeId = groupTypeId;
				group.ParentGroupId = parentGroupId;
			});
		}

		/// <summary>
		///     Асинхронно удаляет группу.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		public async Task DeleteGroupAsync(Guid currentUserId, Guid groupId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == groupId);

				context.Groups.Remove(group);
			});
		}

		public async Task<Group> TryGetByIdAsync(Guid id)
		{
			Group group = null;
			await UseContextAsync(async context =>
			{
				group = await context.Groups.FirstOrDefaultAsync(g => g.Id == id);
			});

			return group;
		}

		public async Task<Group> TryGetByAliasAsync(string alias)
		{
			Group group = null;
			await UseContextAsync(async context =>
			{
				group = await context.Groups.FirstOrDefaultAsync(g => g.Alias == alias);
			});

			return group;
		}

		public async Task<ICollection<User>> GetGroupUsersAsync(Guid id)
		{
			ICollection<User> users = new List<User>();

			await UseContextAsync(async context =>
			{
				var group = await context.Groups
					.Include(g => g.Users)
					.Where(g => g.Id == id)
					.FirstOrDefaultAsync();

				if(group == null)
					return;

				users = group.Users;
			});

			return users;
		}

		public async Task<ICollection<User>> GetGroupUsersAsync(string alias)
		{
			ICollection<User> users = new List<User>();

			await UseContextAsync(async context =>
			{
				var group = await context.Groups
					.Include(g => g.Users)
					.Where(g => g.Alias == alias)
					.FirstOrDefaultAsync();

				if (group == null)
					return;

				users = group.Users;
			});

			return users;
		}

		public async Task<ICollection<User>> GetGroupUsersWithRightsAsync(Guid id)
		{
			ICollection<User> users = new List<User>();

			await UseContextAsync(async context =>
			{
				var group = await context.Groups
					.Include(g => g.Users)
					.ThenInclude(u => u.UserRights)
					.ThenInclude(rights => rights.Right)
					.Where(g => g.Id == id)
					.FirstOrDefaultAsync();

				if (group == null)
					return;

				users = group.Users;
			});

			return users;
		}

		public async Task<ICollection<User>> GetGroupUsersWithRightsAsync(string alias)
		{
			ICollection<User> users = new List<User>();

			await UseContextAsync(async context =>
			{
				var group = await context.Groups
					.Include(g => g.Users)
					.ThenInclude(u => u.UserRights)
					.ThenInclude(rights => rights.Right)
					.Where(g => g.Alias == alias)
					.FirstOrDefaultAsync();

				if (group == null)
					return;

				users = group.Users;
			});

			return users;
		}
	}
}