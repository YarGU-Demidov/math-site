using System.Threading.Tasks;
using MathSite.Db;
using MathSite.ViewModels.Pages;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class PagesController : BaseController
	{
		private readonly IPagesViewModelBuilder _viewModelBuilder; 
		
		public PagesController(MathSiteDbContext dbContext, IPagesViewModelBuilder pagesViewModelBuilder) 
			: base(dbContext)
		{
			_viewModelBuilder = pagesViewModelBuilder;
		}
		
		public async Task<IActionResult> Index(string query)
		{
			return View(await _viewModelBuilder.BuildPageItemViewModelAsync(query));
		}
	}
}