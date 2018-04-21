using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Events;
using MathSite.Common.Extensions;
using MathSite.Controllers;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Area("manager")]
    [Authorize("admin")]
    [Route("[area]/[controller]")]
    public class EventsController : BaseController
    {
        private readonly IEventsManagerViewModelBuilder _modelBuilder;

        public EventsController(
            IUserValidationFacade userValidationFacade, 
            IEventsManagerViewModelBuilder modelBuilder,
            IUsersFacade usersFacade
        ) : base(userValidationFacade, usersFacade)
        {
            _modelBuilder = modelBuilder;
        }

        [Route("")]
        [Route("index")]
        [Route("list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View("Index", await _modelBuilder.BuildIndexViewModel(page, perPage));
        }

        [HttpGet("removed")]
        public async Task<IActionResult> Removed([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View("Index", await _modelBuilder.BuildRemovedViewModel(page, perPage));
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View("Create", await _modelBuilder.BuildCreateViewModel());
        }

        [HttpPost("create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventViewModel eventViewModel)
        {
            eventViewModel.AuthorId = CurrentUser.Id;

            await _modelBuilder.BuildCreateViewModel(eventViewModel);

            return RedirectToActionPermanent("Index");
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            return View("Edit", await _modelBuilder.BuildEditViewModel(id));
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(EventViewModel eventViewModel)
        {
            await _modelBuilder.BuildEditViewModel(eventViewModel);

            return RedirectToActionPermanent("Index");
        }

        [HttpPost("delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _modelBuilder.BuildDeleteViewModel(id);

            return RedirectToActionPermanent("Index");
        }

        [HttpPost("recover/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Recover(Guid id)
        {
            await _modelBuilder.BuildRecoverViewModel(id);
            return RedirectToActionPermanent("Removed");
        }

        [HttpPost("forece-delete/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForceDelete(Guid id)
        {
            await _modelBuilder.BuildForceDeleteViewModel(id);
            return RedirectToActionPermanent("Removed");
        }

        [HttpPost("preview")]
        public IActionResult Preview(EventItemViewModel model)
        {
            if (model.IsNull())
                return BadRequest();

            _modelBuilder.FillPostItemViewModel(model);

            return View("EventItem", "Events", model);
        }
    }
}