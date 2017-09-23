using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.SiteSettings
{
    public class HasKeySpecification : Specification<SiteSetting>
    {
        private readonly string _key;

        public HasKeySpecification(string key)
        {
            _key = key;
        }

        public override Expression<Func<SiteSetting, bool>> ToExpression()
        {
            return setting => setting.Key == _key;
        }
    }
}