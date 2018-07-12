using MathSite.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Entities
{
    public class Message : Entity
    {
        public Message(Guid senderId, Guid conversationId, string body)
        {
            AuthorId = senderId;
            ConversationId = conversationId;
            Body = body;
        }
        public Message(Guid senderId, Guid conversationId, string body, DateTime creationDate)
        {
            AuthorId = senderId;
            ConversationId = conversationId;
            Body = body;
            CreationDate = creationDate;
        }

        public Message(DateTime creationDate)
        {
            CreationDate = creationDate;
        }
        public Message()
        {

        }


        public string Body { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }

        public ICollection<MessageUserConversation> MessageUserConversations { get; set; } = new List<MessageUserConversation>();

    }

}
