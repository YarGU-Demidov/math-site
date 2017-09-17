using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.SharedModels.PostPreview;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.Pages
{
  public class PagesViewModelBuilder : SecondaryViewModelBuilder, IPagesViewModelBuilder

  {
    public PagesViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade,
      IPostPreviewViewModelBuilder postPreviewViewModelBuilder)
      : base(siteSettingsFacade, postsFacade, postPreviewViewModelBuilder)
    {
    }

    protected override string PageTitle { get; set; }

    public async Task<PageItemViewModel> BuildPageItemViewModelAsync(string query)
    {
      var model = await BuildSecondaryViewModel<PageItemViewModel>();

      var post = await BuildPostData(query);
      if (post == null)
        throw new PostNotFoundException(query);

      model.PageTitle.Title = post.Title;
      model.Content = post.Content;

      return model;
    }

    private async Task<Post> BuildPostData(string query)
    {
      return await PostsFacade.GetStaticPageByUrlAsync(query);
    }
  }
}