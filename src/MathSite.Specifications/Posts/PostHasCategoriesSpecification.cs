using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Posts
{
    public class PostHasCategoriesSpecification : Specification<Post>
    {
        private readonly IEnumerable<Guid> _categoryIds;

        public PostHasCategoriesSpecification(Guid categoryId)
            : this(new[] {categoryId})
        {
        }

        public PostHasCategoriesSpecification(IEnumerable<Guid> categoryIds)
        {
            _categoryIds = categoryIds;
        }

        public override Expression<Func<Post, bool>> ToExpression()
        {
            return post => post.PostCategories.Any(
                category => _categoryIds.Any(
                    categoryId => categoryId == category.CategoryId
                )
            );
        }
    }
}