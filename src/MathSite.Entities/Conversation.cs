using MathSite.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Entities
{
    public class Conversation : Entity
    {

        public Conversation()
        {
        }

        public Conversation(Guid creatorId, string name, string type)
        {
            CreatorId = creatorId;
            Name = name;
            Type = type;
        }

        public string Name { get; set; }

        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
        public string Type { get; set; }

        public ICollection<UserConversation> UserConversations { get; set; } = new List<UserConversation>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();


    }

}
