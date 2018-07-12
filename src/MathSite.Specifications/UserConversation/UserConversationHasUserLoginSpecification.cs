using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.UserConversationSpecification
{
    public class UserConversationHasUserLoginspecification : Specification<UserConversation>
    {
        private readonly string _userLogin;

        public UserConversationHasUserLoginspecification(string userLogin)
        {
            _userLogin = userLogin;
        }

        public override Expression<Func<UserConversation, bool>> ToExpression()
        {
            return userConversation => userConversation.User.Login == _userLogin;
        }
    }
}
