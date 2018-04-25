using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.PostSeoSettings
{
    public class PostSeoSettingForPostSpecification : Specification<PostSeoSetting>
    {
        private readonly Guid _postSeoSettingSeoSettingId;

        public PostSeoSettingForPostSpecification(Post postSeoSetting)
        {
            _postSeoSettingSeoSettingId = postSeoSetting.Id;
        }

        public PostSeoSettingForPostSpecification(Guid postSeoSettingId)
        {
            _postSeoSettingSeoSettingId = postSeoSettingId;
        }

        public override Expression<Func<PostSeoSetting, bool>> ToExpression()
        {
            return setting => setting.Post.Id == _postSeoSettingSeoSettingId;
        }
    }
}