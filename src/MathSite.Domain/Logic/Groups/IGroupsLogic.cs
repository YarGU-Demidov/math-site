using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.Groups
{
	public interface IGroupsLogic
	{
		/// <summary>
		///     Асинхронно создает группу.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="name">Название.</param>
		/// <param name="description">Описание.</param>
		/// <param name="groupTypeId">Идентификатор типа группы.</param>
		/// <param name="parentGroupId">Идентификатор родительской группы.</param>
		Task<Guid> CreateGroupAsync(Guid currentUserId, string name, string description, Guid groupTypeId,
			Guid? parentGroupId);

		/// <summary>
		///     Асинхронно обновляет группу.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <param name="name">Наименование группы.</param>
		/// <param name="description">Описание группы.</param>
		/// <param name="groupTypeId">Идентификатор типа группы.</param>
		/// <param name="parentGroupId">Идентификатор родительской группы.</param>
		Task UpdateGroupAsync(Guid currentUserId, Guid groupId, string name, string description, Guid groupTypeId,
			Guid? parentGroupId);

		/// <summary>
		///     Асинхронно удаляет группу.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		Task DeleteGroupAsync(Guid currentUserId, Guid groupId);

		Task<Group> TryGetByIdAsync(Guid id);

		Task<Group> TryGetByAliasAsync(string alias);

		Task<ICollection<User>> GetGroupUsersAsync(Guid id);

		Task<ICollection<User>> GetGroupUsersAsync(string alias);

		Task<ICollection<User>> GetGroupUsersWithRightsAsync(Guid id);

		Task<ICollection<User>> GetGroupUsersWithRightsAsync(string alias);
	}
}