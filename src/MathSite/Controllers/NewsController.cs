using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.News;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsViewModelBuilder _viewModelBuilder;

        public NewsController(IUserValidationFacade userValidationFacade, INewsViewModelBuilder viewModelBuilder) :
            base(userValidationFacade)
        {
            _viewModelBuilder = viewModelBuilder;
        }

        public async Task<IActionResult> Index(string query, [FromQuery] int page = 1)
        {
            return string.IsNullOrWhiteSpace(query)
                ? await ShowAllNews(page)
                : await ShowNewsItem(query, page);
        }

        [NonAction]
        private async Task<IActionResult> ShowAllNews(int page)
        {
            try
            {
                return View(await _viewModelBuilder.BuildIndexViewModelAsync(page));
            }
            catch (NoMorePosts)
            {
                return NotFound();
            }
        }

        [NonAction]
        private async Task<IActionResult> ShowNewsItem(string query, int page)
        {
            try
            {
                return View("NewsItem", await _viewModelBuilder.BuildNewsItemViewModelAsync(query, page));
            }
            catch (PostNotFoundException)
            {
                return NotFound();
            }
        }
    }
}