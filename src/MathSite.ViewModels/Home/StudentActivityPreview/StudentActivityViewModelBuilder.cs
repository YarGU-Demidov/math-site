using MathSite.Entities;

namespace MathSite.ViewModels.Home.StudentActivityPreview
{
    public interface IStudentActivityPreviewViewModelBuilder
    {
        StudentActivityViewModel Build(Post post);
    }

    public class StudentActivityViewModelBuilder: IStudentActivityPreviewViewModelBuilder
    {
        public StudentActivityViewModel Build(Post post)
        {
            var previewId = post.PostSettings?.PreviewImage?.Id.ToString() ??
                            post.PostType.DefaultPostsSettings.PreviewImage?.Id.ToString();

            return new StudentActivityViewModel
            {
                Title = post.Title,
                Url = $"/{post.PostType.Alias}/{post.PostSeoSetting.Url}",
                PreviewImageId = previewId,
                PreviewImageId2X = previewId
            };
        }
    }
}