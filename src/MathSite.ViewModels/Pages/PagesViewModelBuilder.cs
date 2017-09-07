using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.SharedModels.PostPreview;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.Pages
{
	public class PagesViewModelBuilder : SecondaryViewModelBuilder, IPagesViewModelBuilder

	{
		public PagesViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade, IPostPreviewViewModelBuilder postPreviewViewModelBuilder) 
			: base(siteSettingsFacade, postsFacade, postPreviewViewModelBuilder)
		{
		}

		protected override string PageTitle { get; set; }

		public async Task<PageItemViewModel> BuildPageItemViewModelAsync(string query)
		{
			var model = await BuildSecondaryViewModel<PageItemViewModel>();

			var post = await GetPostAsync(query);

			model.PageTitle.Title = post.Title;
			model.Content = post.Content;
			
			return model;
		}

		private async Task<Post> GetPostAsync(string query)
		{
			return new Post
			{
				Title = "Static Page \\\\Hardcoded title",
				Content = $"Query: {query}<br><br>test content<hr><br><br>bla bla bla..."
			};
		}
	}
}