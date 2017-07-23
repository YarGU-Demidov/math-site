using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	/// <summary>
	///     Пост.
	/// </summary>
	public class Post
	{
		/// <summary>
		///     Идентификатор.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		///     Заголовок поста.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///     Первые предложения или очень краткое содержание поста.
		/// </summary>
		public string Excerpt { get; set; }

		/// <summary>
		///     Содержимое поста.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		///     Состояние публикации (true - опубликовано, false - нет).
		/// </summary>
		public bool Published { get; set; }

		/// <summary>
		///     Удален ли пост.
		/// </summary>
		public bool? Deleted { get; set; }

		/// <summary>
		///     Дата публикации поста.
		/// </summary>
		public DateTime PublishDate { get; set; }

		/// <summary>
		///     Идентификатор типа поста.
		/// </summary>
		public Guid PostTypeId { get; set; }

		/// <summary>
		///     Идентификатор автора поста.
		/// </summary>
		public Guid AuthorId { get; set; }

		/// <summary>
		///     Автор поста.
		/// </summary>
		public User Author { get; set; }

		/// <summary>
		///     Тип поста.
		/// </summary>
		public PostType PostType { get; set; }

		/// <summary>
		///     Идентификатор настроек поста.
		/// </summary>
		public Guid PostSettingsId { get; set; }

		/// <summary>
		///     Настройки поста.
		/// </summary>
		public PostSettings PostSettings { get; set; }

		/// <summary>
		///     Идентификатор SEO настроек поста.
		/// </summary>
		public Guid PostSeoSettingsId { get; set; }

		/// <summary>
		///     SEO настройки поста.
		/// </summary>
		public PostSeoSettings PostSeoSettings { get; set; }

		/// <summary>
		///     Список категорий поста.
		/// </summary>
		public List<PostCategory> PostCategories { get; set; } = new List<PostCategory>();

		/// <summary>
		///     Список владельцев поста.
		/// </summary>
		public List<PostOwner> PostOwners { get; set; } = new List<PostOwner>();

		/// <summary>
		///     Список пользователей, которым точно разрешен доступ к посту.
		/// </summary>
		public List<PostUserAllowed> UsersAllowed { get; set; } = new List<PostUserAllowed>();

		/// <summary>
		///     Список оценок от пользователей.
		/// </summary>
		public List<PostRating> PostRatings { get; set; } = new List<PostRating>();

		/// <summary>
		///     Список комментариев к посту.
		/// </summary>
		public List<Comment> Comments { get; set; } = new List<Comment>();

		/// <summary>
		///     Список приложений к посту.
		/// </summary>
		public List<PostAttachment> PostAttachments { get; set; } = new List<PostAttachment>();

		/// <summary>
		///     Список групп, которым разрешен доступ к посту.
		/// </summary>
		public List<PostGroupsAllowed> GroupsAllowed { get; set; } = new List<PostGroupsAllowed>();
	}
}