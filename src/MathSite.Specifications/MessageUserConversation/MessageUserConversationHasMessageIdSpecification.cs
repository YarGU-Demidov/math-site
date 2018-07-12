using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.MessageUserConversationSpecification
{
    public class MessageUserConversationHasMessageIdSpecification : Specification<MessageUserConversation>
    {
        private readonly Guid _messageId;

        public MessageUserConversationHasMessageIdSpecification(Guid messageId)
        {
            _messageId = messageId;
        }

        public override Expression<Func<MessageUserConversation, bool>> ToExpression()
        {
            return messageUserConversation => messageUserConversation.MessageId == _messageId;
        }
    }
}
