using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Conversation
{
    public class ConversationHasUserIdSpecification : Specification<Entities.Conversation>
    {
        private readonly Guid _userId;

        public ConversationHasUserIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<Entities.Conversation, bool>> ToExpression()
        {
            return conversation => conversation.UserConversations.Count(userConversation => userConversation.UserId == _userId) > 0;
        }
    }
}
