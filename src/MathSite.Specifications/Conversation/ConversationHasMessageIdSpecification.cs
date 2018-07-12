using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;

namespace MathSite.Specifications.Conversation
{
    public class ConversationHasMessageIdSpecification : Specification<Entities.Conversation>
    {
        private readonly Guid _messageId;

        public ConversationHasMessageIdSpecification(Guid messageId)
        {
            _messageId = messageId;
        }

        public override Expression<Func<Entities.Conversation, bool>> ToExpression()
        {
            return conversation => conversation.Messages.Count(message => message.Id == _messageId) > 0;
        }
    }
}
