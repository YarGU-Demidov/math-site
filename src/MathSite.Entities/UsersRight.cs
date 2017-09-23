using System;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    /// <summary>
    /// </summary>
    public class UsersRight : Entity
    {
        /// <summary>
        /// </summary>
        public bool Allowed { get; set; }

        /// <summary>
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// </summary>
        public Guid RightId { get; set; }

        /// <summary>
        /// </summary>
        public Right Right { get; set; }
    }
}