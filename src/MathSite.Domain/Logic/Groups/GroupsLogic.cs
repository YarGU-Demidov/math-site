using System;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Groups
{
	public class GroupsLogic : LogicBase<Group>, IGroupsLogic
	{
		private const string GroupNotFoundFormat = "Группа с Id='{0}' не найдена";
		private const string GroupTypeNotFoundFormat = "Тип группы с Id='{0}' не найден";
		
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
				var groupType = await context.GroupTypes.AnyAsync(i => i.Id == groupTypeId);
				if (!groupType)
					throw new Exception(string.Format(GroupTypeNotFoundFormat, groupTypeId));

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
			await UseContextAsync(async context =>
			{
				var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == groupId);
				if (group == null)
					throw new Exception(string.Format(GroupNotFoundFormat, groupId));

				var groupType = await context.GroupTypes.AnyAsync(i => i.Id == groupTypeId);
				if (!groupType)
					throw new Exception(string.Format(GroupTypeNotFoundFormat, groupTypeId));

				group.Name = name;
				group.Description = description;
				group.Alias = name;
				group.GroupTypeId = groupTypeId;
				group.ParentGroupId = parentGroupId;

				await context.SaveChangesAsync();
			});
		}

		/// <summary>
		///     Асинхронно удаляет группу.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		public async Task DeleteGroupAsync(Guid currentUserId, Guid groupId)
		{
			await UseContextAsync(async context =>
			{
				var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == groupId);
				if (group == null)
					throw new Exception(string.Format(GroupNotFoundFormat, groupId));

				context.Groups.Remove(group);
				await context.SaveChangesAsync();
			});
		}
	}
}