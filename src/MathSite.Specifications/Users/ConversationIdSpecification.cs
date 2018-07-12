using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Users
{
    public class ConversationIdSpecification : Specification<User>
    {
        private readonly Guid _conversationId;

        public ConversationIdSpecification(Guid conversationId)
        {
            _conversationId = conversationId;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.UserConversations.Any(userConversation => userConversation.ConversationId == _conversationId);
        }
    }
}


