using System;
using System.Linq;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Posts
{
    public class PostNotInCategorySpecification : Specification<Post>
    {
        private readonly Category _excludedCategory;

        public PostNotInCategorySpecification(Category excludedCategory)
        {
            _excludedCategory = excludedCategory;
        }

        public override Expression<Func<Post, bool>> ToExpression()
        {
            return post => post.PostCategories.All(
                postCategory => postCategory.CategoryId != _excludedCategory.Id
            );
        }
    }
}