using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;

namespace MathSite.Specifications.Conversation
{
    public class ConversationHasConversationIdSpecification : Specification<Entities.Conversation>
    {
        private readonly Guid _conversationId;

        public ConversationHasConversationIdSpecification(Guid conversationId)
        {
            _conversationId = conversationId;
        }

        public override Expression<Func<Entities.Conversation, bool>> ToExpression()
        {
            return conversation => conversation.Id == _conversationId;
        }
    }
}
