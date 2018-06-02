using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.PostSettings
{
    public class PostSettingsForPostSpecification : Specification<PostSetting>
    {
        private readonly Guid _postId;

        public PostSettingsForPostSpecification(Post post)
        {
            _postId = post.Id;
        }

        public PostSettingsForPostSpecification(Guid postId)
        {
            _postId = postId;
        }

        public override Expression<Func<PostSetting, bool>> ToExpression()
        {
            return setting => setting.Post.Id == _postId;
        }
    }
}