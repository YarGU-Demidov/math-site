using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.PostSettings
{
    public class PostSettingsForPostSpecification : Specification<PostSetting>
    {
        private readonly Post _post;

        public PostSettingsForPostSpecification(Post post)
        {
            _post = post;
        }

        public override Expression<Func<PostSetting, bool>> ToExpression()
        {
            return setting => setting.Post.Id == _post.Id;
        }
    }
}