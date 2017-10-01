using System;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    /// <summary>
    ///     Настройки пользователя.
    /// </summary>
    public class UserSetting : Entity
    {
        /// <summary>
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// </summary>
        public User User { get; set; }
    }
}