using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.MessageUserConversationSpecification
{
    public class MessageUserConversationHasUserIdSpecification: Specification<MessageUserConversation>
    {
        private readonly Guid _userId;

        public MessageUserConversationHasUserIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<MessageUserConversation, bool>> ToExpression()
        {
            return messageUserConversation => messageUserConversation.UserConversation.UserId == _userId;
        }
    }
}
