using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.UserConversationSpecification
{
    public class UserConversationHasConversationIdSpecification : Specification<UserConversation>
    {
        private readonly Guid _conversationId;

        public UserConversationHasConversationIdSpecification(Guid conversationId)
        {
            _conversationId = conversationId;
        }

        public override Expression<Func<UserConversation, bool>> ToExpression()
        {
            return userConversation => userConversation.ConversationId == _conversationId;
        }
    }
}
