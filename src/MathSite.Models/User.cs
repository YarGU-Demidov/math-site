using System;
using System.Collections.Generic;

namespace MathSite.Models
{
	/// <summary>
	///     Пользователь.
	/// </summary>
	public class User
	{
		public User() {}

		/// <summary>
		///		Создает сущность.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="passwordHash">Пароль.</param>
		/// <param name="creationDate">Дата регистрации пользователя.</param>
		/// <param name="personId">Идентификатор личности.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		public User(string login, string passwordHash, DateTime creationDate, Guid personId, Guid groupId)
		{
			Login = login;
			PasswordHash = passwordHash;
			CreationDate = creationDate;
			PersonId = personId;
			GroupId = groupId;
		}

		/// <summary>
		///     Идентификатор.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		///     Логин.
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		///     Пароль.
		/// </summary>
		public string PasswordHash { get; set; }

		/// <summary>
		///     Идентификатор личности.
		/// </summary>
		public Guid PersonId { get; set; }

		/// <summary>
		///     Личность.
		/// </summary>
		public Person Person { get; set; }

		/// <summary>
		///     Идентификатор группы.
		/// </summary>
		public Guid GroupId { get; set; }

		/// <summary>
		///     Группа, к которой относится пользователь.
		/// </summary>
		public Group Group { get; set; }

		/// <summary>
		///     Дата создания пользователя.
		/// </summary>
		public DateTime? CreationDate { get; set; }

		/// <summary>
		///		Перечень постов пользователя.
		/// </summary>
		public List<PostOwner> PostsOwner { get; set; }

		/// <summary>
		///		Перечень постов, к которым разрешен доступ.
		/// </summary>
		public List<PostUserAllowed> AllowedPosts { get; set; }

		/// <summary>
		///		Перечень оценок пользователя к постам, которые он оценил.
		/// </summary>
		public List<PostRating> PostsRatings { get; set; }

		/// <summary>
		///		Перечень комментариев пользователя.
		/// </summary>
		public List<Comment> Comments { get; set; }

		/// <summary>
		///		Перечень настроек пользователя.
		/// </summary>
		public List<UserSettings> Settings { get; set; }

		/// <summary>
		///     Перечень прав пользователя.
		/// </summary>
		public List<UsersRights> UserRights { get; set; }
	}
}