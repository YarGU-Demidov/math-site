using System;

namespace MathSite.Entities
{
    /// <summary>
    ///     Права групп.
    /// </summary>
    public class GroupsRight
    {
        /// <summary>
        ///     Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Разрешение для группы (true -- разрешено, false -- запрещено).
        /// </summary>
        public bool Allowed { get; set; }

        /// <summary>
        ///     Идентификатор группы, к которому относится это правило.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        ///     Идентификатор правила, с которым настраивается сопоставление.
        /// </summary>
        public string RightAlias { get; set; }

        /// <summary>
        ///     Группа, к которой относится это правило.
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        ///     Правило, с которым настраивается сопоставление.
        /// </summary>
        public Right Right { get; set; }
    }
}