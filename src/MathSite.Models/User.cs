using System;
using System.Collections.Generic;

namespace MathSite.Models
{

    /// <summary>
    ///     Пользователь.
    /// </summary>
    public class User
	{

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
		public Guid? PersonId { get; set; }

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
        public DateTime CreationDate { get; set; }
		
		/// <summary>
		/// </summary>
		public List<PostOwner> PostsOwner { get; set; }

		/// <summary>
		/// </summary>
		public List<PostUserAllowed> AllowedPosts { get; set; }

		/// <summary>
		/// </summary>
		public List<PostRating> PostsRatings { get; set; }

		/// <summary>
		/// </summary>
		public List<Comment> Comments { get; set; }

		/// <summary>
		/// </summary>
		public List<UserSettings> Settings { get; set; }
		
		/// <summary>
		///     Права пользователя.
		/// </summary>
		public List<UsersRights> UserRights { get; set; }
	}
}