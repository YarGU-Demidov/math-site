using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Messages
{
    public class MessageHasConversationIdSpecification : Specification<Message>
    {
        private readonly Guid _conversationId;

        public MessageHasConversationIdSpecification(Guid conversationId)
        {
            _conversationId = conversationId;
        }

        public override Expression<Func<Message, bool>> ToExpression()
        {
            return message => message.ConversationId == _conversationId;
        }
    }
}
