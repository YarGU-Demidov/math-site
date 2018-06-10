using System;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;

namespace MathSite.ViewModels.Home.PostPreview
{
    public interface IPostPreviewViewModelBuilder
    {
        PostPreviewViewModel Build(Post post);
    }

    public class PostPreviewViewModelBuilder : PostPreviewViewModelBuilder<PostPreviewViewModel>, IPostPreviewViewModelBuilder
    {
        private PostPreviewViewModel BuildForStatic(Post post)
        {
            var model = BuildPreviewModel(post);
            model.Url = post.PostSeoSetting.Url;

            return model;
        }
        
        public virtual PostPreviewViewModel Build(Post post)
        {
            switch (post.PostType.Alias)
            {
                case PostTypeAliases.StaticPage:
                    return BuildForStatic(post);
                default:
                    return BuildPreviewModel(post);
            }
        }
    }

    public class PostPreviewViewModelBuilder<TPreivewViewModel>
        where TPreivewViewModel: PostPreviewViewModel, new()
    {
        protected TPreivewViewModel BuildPreviewModel(Post post)
        {
            var previewId = post.PostSettings?.PreviewImage?.Id.ToString() ??
                             post.PostType?.DefaultPostsSettings?.PreviewImage?.Id.ToString();

            return new TPreivewViewModel
            {
                Title = post.Title,
                Url = $"/{post.PostType?.Alias}/{Uri.EscapeDataString(post.PostSeoSetting.Url)}",
                Content = post.Excerpt,
                PostTypeName = post.PostType?.Name,
                PublishedAt = post.PublishDate,
                PreviewImageId = previewId,
                PreviewImageId2X = previewId
            };
        }
    }
}