using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Posts
{
    public class PostWithTypeAliasSpecification : Specification<Post>
    {
        private readonly string _postTypeAlias;

        public PostWithTypeAliasSpecification(string postTypeAlias)
        {
            _postTypeAlias = postTypeAlias;
        }

        public override Expression<Func<Post, bool>> ToExpression()
        {
            return post => post.PostType.Alias == _postTypeAlias;
        }
    }
}