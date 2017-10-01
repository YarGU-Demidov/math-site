using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Posts
{
    public class PostWithSpecifiedUrlSpecification : Specification<Post>
    {
        private readonly string _url;

        public PostWithSpecifiedUrlSpecification(string url)
        {
            _url = url;
        }

        public override Expression<Func<Post, bool>> ToExpression()
        {
            return post => post.PostSeoSetting.Url == _url;
        }
    }
}