using System;
using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Events;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IEventsViewModelBuilder _viewModelBuilder;

        public EventsController(IUserValidationFacade userValidationFacade, IEventsViewModelBuilder viewModelBuilder, IUsersFacade usersFacade)
            : base(userValidationFacade, usersFacade)
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
                return View("EventItem", await _viewModelBuilder.BuildNewsItemViewModelAsync(CurrentUserId ?? Guid.Empty, query, page));
            }
            catch (PostNotFoundException)
            {
                return NotFound();
            }
        }
    }
}