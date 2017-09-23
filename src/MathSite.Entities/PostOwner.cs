using System;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    public class PostOwner : Entity
    {

        /// <summary>
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// </summary>
        public Post Post { get; set; }

        /// <summary>
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// </summary>
        public User User { get; set; }
    }
}