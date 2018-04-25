using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.PostCategories
{
    public class PostCategoryWithPostSpecification : Specification<PostCategory>
    {
        private readonly Guid _postId;

        public PostCategoryWithPostSpecification(Post post)
        {
            _postId = post.Id;
        }

        public PostCategoryWithPostSpecification(Guid postId)
        {
            _postId = postId;
        }

        public override Expression<Func<PostCategory, bool>> ToExpression()
        {
            return postCategory => postCategory.Post.Id == _postId;
        }
    }
}