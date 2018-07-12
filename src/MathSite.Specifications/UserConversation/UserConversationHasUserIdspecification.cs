using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.UserConversationSpecification
{
    public class UserConversationHasUserIdspecification : Specification<UserConversation>
    {
        private readonly Guid _userId;

        public UserConversationHasUserIdspecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<UserConversation, bool>> ToExpression()
        {
            return userConversation => userConversation.UserId == _userId;
        }
    }
}
