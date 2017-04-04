using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PostUserAllowed
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Allowed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Post Post { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public User User { get; set; }
    }
}
