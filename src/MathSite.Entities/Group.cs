using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
    /// <summary>
    ///     Группа.
    /// </summary>
    public class Group
    {
        public Group()
        {
        }

        /// <summary>
        ///     Создает сущность.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <param name="description">Описание.</param>
        /// <param name="alias">Alias группы.</param>
        /// <param name="groupTypeAlias">Alias типа группы.</param>
        /// <param name="parentGroupId">Идентификатор родительской группы.</param>
        /// <param name="isAdmin">Является ли группа администраторской в своем типе.</param>
        public Group(string name, string description, string alias, string groupTypeAlias, Guid? parentGroupId,
            bool isAdmin = false)
        {
            Name = name;
            Description = description;
            Alias = alias;
            ParentGroupId = parentGroupId;
            GroupTypeAlias = groupTypeAlias;
            IsAdmin = isAdmin;
        }

        /// <summary>
        ///     Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Алиас.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///     Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Идентификатор родительской группы.
        /// </summary>
        public Guid? ParentGroupId { get; set; }

        /// <summary>
        ///     Родительская группа.
        /// </summary>
        public Group ParentGroup { get; set; }

        /// <summary>
        ///     Идентификатор типа группы.
        /// </summary>
        public string GroupTypeAlias { get; set; }

        /// <summary>
        ///     Тип группы.
        /// </summary>
        public GroupType GroupType { get; set; }

        /// <summary>
        ///     Является ли группа администраторской.
        ///     ОСТОРОЖНО! Позволяет делать всё!
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        ///     Перечень прав группы.
        /// </summary>
        public ICollection<GroupsRight> GroupsRights { get; set; } = new List<GroupsRight>();

        /// <summary>
        ///     Перечень пользователей.
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();

        /// <summary>
        ///     Перечень постов группы.
        /// </summary>
        public ICollection<PostGroupsAllowed> PostGroupsAllowed { get; set; } = new List<PostGroupsAllowed>();

        /// <summary>
        ///     Перечень групп-детей.
        /// </summary>
        public ICollection<Group> ChildGroups { get; set; } = new List<Group>();
    }
}