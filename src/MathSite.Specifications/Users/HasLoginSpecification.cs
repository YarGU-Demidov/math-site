using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Users
{
    public class HasLoginSpecification : Specification<User>
    {
        private readonly string _login;

        public HasLoginSpecification(string login)
        {
            _login = login;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Login == _login;
        }
    }
}