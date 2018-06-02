using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Groups
{
    public class GroupWithTypeAliasSpecification : Specification<Group>
    {
        private readonly string _typeAlias;

        public GroupWithTypeAliasSpecification(string typeAlias)
        {
            _typeAlias = typeAlias;
        }

        public override Expression<Func<Group, bool>> ToExpression()
        {
            return group => group.GroupType.Alias == _typeAlias;
        }
    }
}