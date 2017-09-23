using System.Collections.Generic;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    /// <summary>
    ///     Категория поста
    /// </summary>
    public class Category : EntityWithNameAndAlias
    {
        /// <summary>
        ///     Описание категории
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Список постов этой категории
        /// </summary>
        public ICollection<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
    }
}