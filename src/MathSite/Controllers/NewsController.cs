using System;
using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Common.Extensions;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.News;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsViewModelBuilder _viewModelBuilder;

        public NewsController(IUserValidationFacade userValidationFacade, INewsViewModelBuilder viewModelBuilder, IUsersFacade usersFacade)
            : base(userValidationFacade, usersFacade)
        {
            _viewModelBuilder = viewModelBuilder;
        }

        public async Task<IActionResult> Index(string query, [FromQuery] int page = 1)
        {
            return query.IsNullOrWhiteSpace()
                ? await ShowAllNews(page)
                : await ShowNewsItem(query, page);
        }

        public async Task<IActionResult> ByCategory(string query, [FromQuery] int page = 1)
        {
            try
            {
                return View("ByCategory", await _viewModelBuilder.BuildByCategoryViewModelAsync(query, page));
            }
            catch (PostNotFoundException)
            {
                return NotFound();
            }
            catch (CategoryDoesNotExists)
            {
                return NotFound();
            }
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
                return View("NewsItem", await _viewModelBuilder.BuildNewsItemViewModelAsync(CurrentUserId ?? Guid.Empty, query, page));
            }
            catch (PostNotFoundException)
            {
                return NotFound();
            }
        }
    }
}