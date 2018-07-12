using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.MessageUserConversationSpecification
{
    public class MessageUserConversationHasMessageStatusSpecification : Specification<MessageUserConversation>
    {
        private readonly bool _messageStatus;

        public MessageUserConversationHasMessageStatusSpecification(bool messageStatus)
        {
            _messageStatus = messageStatus;
        }

        public override Expression<Func<MessageUserConversation, bool>> ToExpression()
        {
            return messageUserConversation => messageUserConversation.IsRead == _messageStatus;
        }
    }
}
