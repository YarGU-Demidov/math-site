using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
    /// <summary>
    ///     Ключевые слова.
    /// </summary>
    public class Keyword
    {
        /// <summary>
        ///     Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Само ключевое слово.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Алиас ключевого слова.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///     Список постов, содержащих это ключевое слово.
        /// </summary>
        public ICollection<PostKeyword> Posts { get; set; } = new List<PostKeyword>();
    }
}