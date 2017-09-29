using System.Globalization;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;

namespace MathSite.ViewModels.SharedModels.PostPreview
{
    public interface IPostPreviewViewModelBuilder
    {
        PostPreviewViewModel Build(Post post);
    }

    public class PostPreviewViewModelBuilder : IPostPreviewViewModelBuilder
    {
        public PostPreviewViewModel Build(Post post)
        {
            switch (post.PostType.Alias)
            {
                case PostTypeAliases.StaticPage:
                    return BuildForStatic(post);
                default:
                    return BuildForAll(post);
            }
        }

        private PostPreviewViewModel BuildForStatic(Post post)
        {
            var model = BuildForAll(post);
            model.Url = post.PostSeoSetting.Url;

            return model;
        }

        private PostPreviewViewModel BuildForAll(Post post)
        {
            return new PostPreviewViewModel
            {
                Title = post.Title,
                Url = $"{post.PostType.Alias}/{post.PostSeoSetting.Url}",
                Content = post.Excerpt,
                PublishedAt = post.PublishDate.ToString("dd MMM yyyy г.", CultureInfo.GetCultureInfo("ru-RU")).Replace("май", "мая"),
                PreviewImage = post.PostSettings?.PreviewImage?.Path ??
                               post.PostType?.DefaultPostsSettings?.PreviewImage?.Path
            };
        }
    }
}