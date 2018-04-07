using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.PostSeoSettings
{
    public class PostSeoSettingForPostSpecification : Specification<PostSeoSetting>
    {
        private readonly Post _postSeoSetting;

        public PostSeoSettingForPostSpecification(Post postSeoSetting)
        {
            _postSeoSetting = postSeoSetting;
        }

        public override Expression<Func<PostSeoSetting, bool>> ToExpression()
        {
            return setting => setting.Post.Id == _postSeoSetting.Id;
        }
    }
}