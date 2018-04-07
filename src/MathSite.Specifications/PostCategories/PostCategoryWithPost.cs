using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.PostCategories
{
    public class PostCategoryWithPost : Specification<PostCategory>
    {
        private readonly Post _post;

        public PostCategoryWithPost(Post post)
        {
            _post = post;
        }

        public override Expression<Func<PostCategory, bool>> ToExpression()
        {
            return postCategory => postCategory.Post.Id == _post.Id;
        }
    }
}