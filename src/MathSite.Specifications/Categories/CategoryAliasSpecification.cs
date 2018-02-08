using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Categories
{
    public class CategoryAliasSpecification : Specification<Category>
    {
        private readonly string _categoryAlias;

        public CategoryAliasSpecification(string categoryAlias)
        {
            _categoryAlias = categoryAlias;
        }

        public override Expression<Func<Category, bool>> ToExpression()
        {
            return category => category.Alias == _categoryAlias;
        }
    }
}