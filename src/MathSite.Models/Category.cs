using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	/// <summary>
	///     Категория поста
	/// </summary>
	public class Category
	{
		/// <summary>
		///     Идентификатор категории
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		///     Имя категории
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Описание категории
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		///     Алиас категории
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		///     Список постов этой категории
		/// </summary>
		public List<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
	}
}