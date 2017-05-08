using System;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Models;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Groups
{
	public class GroupsLogic : LogicBase, IGroupsLogic
	{
		private const string CurrentUserNotFoundFormat = "Текущий пользователь с Id='{0}' не найден";

		private const string CurrentUserDoNotHaveAdminRightsFormat =
			"Пользователь с Id='{0}' не имеет прав администратора на выполнение данной операции";

		private const string GroupNotFoundFormat = "Группа с Id='{0}' не найдена";
		private const string GroupTypeNotFoundFormat = "Тип группы с Id='{0}' не найден";

		public GroupsLogic(IMathSiteDbContext contextManager) : base(contextManager)
		{
		}

		Task<Guid> IGroupsLogic.CreateGroupAsync(Guid currentUserId, string name, string description, Guid groupTypeId,
			Guid? parentGroupId)
		{
			return CreateGroupAsync(currentUserId, name, description, groupTypeId, parentGroupId);
		}

		Task IGroupsLogic.UpdateGroupAsync(Guid currentUserId, Guid groupId, string name, string description,
			Guid groupTypeId, Guid? parentGroupId)
		{
			return UpdateGroupAsync(currentUserId, groupId, name, description, groupTypeId, parentGroupId);
		}

		Task IGroupsLogic.DeleteGroupAsync(Guid currentUserId, Guid groupId)
		{
			return DeleteGroupAsync(currentUserId, groupId);
		}

		TResult IGroupsLogic.GetFromGroups<TResult>(Func<IQueryable<Group>, TResult> getResult)
		{
			return GetFromGroups(getResult);
		}

		Task<TResult> IGroupsLogic.GetFromGroupsAsync<TResult>(Func<IQueryable<Group>, Task<TResult>> getResult)
		{
			return GetFromGroupsAsync(getResult);
		}

		/// <summary>
		/// Асинхронно создает группу.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="name">Наименование группы.</param>
		/// <param name="description">Описание группы.</param>
		/// <param name="groupTypeId">Идентификатор типа группы.</param>
		/// <param name="parentGroupId">Идентификатор родительской группы.</param>
		private async Task<Guid> CreateGroupAsync(Guid currentUserId, string name, string description, Guid groupTypeId,
			Guid? parentGroupId)
		{
			if (string.IsNullOrEmpty(currentUserId.ToString()))
				throw new SecurityException(string.Format(CurrentUserNotFoundFormat, currentUserId));

			var groupId = Guid.Empty;
			await UseContextAsync(async context =>
			{
				await CheckCurrentUserRightsAsync(context, currentUserId);

				var group = new Group(name, description, groupTypeId, parentGroupId);

				context.Groups.Add(group);
				await context.SaveChangesAsync();

				groupId = group.Id;
			});

			return groupId;
		}

		/// <summary>
		/// Асинхронно обновляет группу.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <param name="name">Наименование группы.</param>
		/// <param name="description">Описание группы.</param>
		/// <param name="groupTypeId">Идентификатор типа группы.</param>
		/// <param name="parentGroupId">Идентификатор родительской группы.</param>
		private async Task UpdateGroupAsync(Guid currentUserId, Guid groupId, string name, string description,
			Guid groupTypeId, Guid? parentGroupId)
		{
			if (string.IsNullOrEmpty(currentUserId.ToString()))
				throw new SecurityException(string.Format(CurrentUserNotFoundFormat, currentUserId));

			await UseContextAsync(async context =>
			{
				await CheckCurrentUserRightsAsync(context, currentUserId);

				var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == groupId);
				if (group == null)
					throw new Exception(string.Format(GroupNotFoundFormat, groupId));

				var groupType = await context.GroupTypes.FirstOrDefaultAsync(i => i.Id == groupTypeId);
				if (groupType == null)
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
		/// Асинхронно удаляет группу.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		private async Task DeleteGroupAsync(Guid currentUserId, Guid groupId)
		{
			if (string.IsNullOrEmpty(currentUserId.ToString()))
				throw new SecurityException(string.Format(CurrentUserNotFoundFormat, currentUserId));

			await UseContextAsync(async context =>
			{
				await CheckCurrentUserRightsAsync(context, currentUserId);

				var group = await context.Groups.FirstOrDefaultAsync(i => i.Id == groupId);
				if (group == null)
					throw new Exception(string.Format(GroupNotFoundFormat, groupId));

				context.Groups.Remove(group);
				await context.SaveChangesAsync();
			});

		}

		/// <summary>
		/// Асинхронно выполняет проверку прав текущего пользователя.
		/// </summary>
		/// <returns></returns>
		private static async Task CheckCurrentUserRightsAsync(IMathSiteDbContext context, Guid currentUserId)
		{
			var isCurrentUserHaveAdminRights = await context.UsersRights
				.Where(i => i.UserId == currentUserId)
				.AnyAsync(i => i.Right.Alias == "admin");

			if (!isCurrentUserHaveAdminRights)
				throw new Exception(string.Format(CurrentUserDoNotHaveAdminRightsFormat, currentUserId));
		}

		/// <summary>
		/// Возвращает результат из перечня групп.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		private TResult GetFromGroups<TResult>(Func<IQueryable<Group>, TResult> getResult)
		{
			return GetFromItems(i => i.Groups, getResult);
		}

		/// <summary>
		/// Асинхронно возвращает результат из перечня групп.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		private Task<TResult> GetFromGroupsAsync<TResult>(Func<IQueryable<Group>, Task<TResult>> getResult)
		{
			return GetFromItems(i => i.Groups, getResult);
		}
	}
}