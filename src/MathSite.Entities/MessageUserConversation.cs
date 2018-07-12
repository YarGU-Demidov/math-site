using System;
using System.Collections.Generic;
using System.Text;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    public class MessageUserConversation : Entity
    {
        public MessageUserConversation(Guid messageId, Guid userConversationId)
        {
            MessageId = messageId;
            UserConversationId = userConversationId;
        }
        public MessageUserConversation(Guid messageId, Guid userId, bool isRead)
        {
            MessageId = messageId;
            UserConversationId = userId;
            IsRead = isRead;
        }
        public MessageUserConversation()
        {

        }
        public Guid MessageId { get; set; }
        public Message Message { get; set; }

        public Guid UserConversationId { get; set; }
        public UserConversation UserConversation { get; set; }

        public bool IsRead { get; set; }
    }
}
