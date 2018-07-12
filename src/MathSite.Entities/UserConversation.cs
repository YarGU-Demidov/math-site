using MathSite.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Entities
{
    public class UserConversation : Entity
    {
        public UserConversation(Guid user, Guid conversation)
        {
            UserId = user;
            ConversationId = conversation;
        }
        public UserConversation()
        {
        }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }

        public ICollection<MessageUserConversation> MessageUserConversations { get; set; } = new List<MessageUserConversation>();
    }

}
