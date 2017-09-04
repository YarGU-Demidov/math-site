using System.Threading.Tasks;
using MathSite.Db;
using MathSite.ViewModels.News;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class NewsController : BaseController
	{
		private readonly INewsViewModelBuilder _viewModelBuilder;

		public NewsController(MathSiteDbContext dbContext, INewsViewModelBuilder viewModelBuilder) : base(dbContext)
		{
			_viewModelBuilder = viewModelBuilder;
		}

		public async Task<IActionResult> Index(string query, int page = 1)
		{
			ViewData.Add("page", page);

			return string.IsNullOrWhiteSpace(query)
				? await ShowAllNews()
				: await ShowNewsItem(query, page);
		}

		[NonAction]
		private async Task<IActionResult> ShowAllNews()
		{
			return View(await _viewModelBuilder.BuildIndexViewModelAsync());
		}

		[NonAction]
		private async Task<IActionResult> ShowNewsItem(string query, int page)
		{
			return View("NewsItem", await _viewModelBuilder.BuildNewsItemViewModelAsync(query, page));
		}
	}
}