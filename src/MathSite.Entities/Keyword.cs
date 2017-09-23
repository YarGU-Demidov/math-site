using System;
using System.Collections.Generic;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    /// <summary>
    ///     Ключевые слова.
    /// </summary>
    public class Keyword : EntityWithNameAndAlias
    {
        /// <summary>
        ///     Список постов, содержащих это ключевое слово.
        /// </summary>
        public ICollection<PostKeyword> Posts { get; set; } = new List<PostKeyword>();
    }
}