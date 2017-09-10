using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	/// <summary>
	///     SEO настройки поста.
	/// </summary>
	public class PostSeoSetting
	{
		/// <summary>
		///     Id SEO настроек поста.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		///     URL поста.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		///     Заголовок поста.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///     Описание поста.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		///     Пост
		/// </summary>
		public Post Post { get; set; }

		/// <summary>
		///     Ключевые слова поста.
		/// </summary>
		public ICollection<PostKeyword> PostKeywords { get; set; } = new List<PostKeyword>();
	}
}