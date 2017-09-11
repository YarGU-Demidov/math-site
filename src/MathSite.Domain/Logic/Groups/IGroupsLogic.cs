using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.Groups
{
	public interface IGroupsLogic
	{
		/// <summary>
		///     Асинхронно создает группу.
		/// </summary>
		/// <param name="name">Название.</param>
		/// <param name="description">Описание.</param>
		/// <param name="groupTypeAlias">Alias типа группы.</param>
		/// <param name="isAdmin">Является ли группа администраторской в своём типе</param>
		/// <param name="parentGroupId">Идентификатор родительской группы.</param>
		Task<Guid> CreateAsync(string name, string description, string alias, string groupTypeAlias, bool isAdmin, Guid? parentGroupId);

		/// <summary>
		///     Асинхронно обновляет группу.
		/// </summary>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <param name="name">Наименование группы.</param>
		/// <param name="description">Описание группы.</param>
		/// <param name="groupTypeAlias">Alias типа группы.</param>
		/// <param name="parentGroupId">Идентификатор родительской группы.</param>
		Task UpdateAsync(Guid groupId, string name, string description, string groupTypeAlias, bool isAdmin, Guid? parentGroupId);

		/// <summary>
		///     Асинхронно удаляет группу.
		/// </summary>
		/// <param name="groupId">Идентификатор группы.</param>
		Task DeleteAsync(Guid groupId);

		Task<Group> TryGetByIdAsync(Guid id);

		Task<Group> TryGetByAliasAsync(string alias);

		Task<Group> TryGetGroupWithUsersByIdAsync(Guid id);

		Task<Group> TryGetGroupWithUsersByAliasAsync(string alias);

		Task<Group> TryGetGroupWithUsersWithRightsByIdAsync(Guid id);

		Task<Group> TryGetGroupWithUsersWithRightsByAliasAsync(string alias);
	}
}