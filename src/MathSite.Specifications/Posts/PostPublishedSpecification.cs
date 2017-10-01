using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Posts
{
    public class PostPublishedSpecification : Specification<Post>
    {
        public override Expression<Func<Post, bool>> ToExpression()
        {
            return post => post.Published;
        }
    }
}