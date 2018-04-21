using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Persons;
using MathSite.Common.Exceptions;
using MathSite.Controllers;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Area("manager")]
    [Authorize(RightAliases.AdminAccess)]
    [Route("[area]/[controller]")]
    public class PersonsController : BaseController
    {
        private readonly IPersonsManagerViewModelBuilder _viewModelBuilder;

        public PersonsController(IUserValidationFacade userValidationFacade, IPersonsManagerViewModelBuilder viewModelBuilder, IUsersFacade usersFacade) 
            : base(userValidationFacade, usersFacade)
        {
            _viewModelBuilder = viewModelBuilder;
        }

        [Route("")]
        [Route("index")]
        [Route("list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View("Index", await _viewModelBuilder.BuildIndexViewModelAsync(page, perPage));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Create", await _viewModelBuilder.BuildCreateViewModelAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePersonsViewModel model)
        {
            if (!TryValidateModel(model))
                return BadRequest("Entered data is incorrect!");

            await _viewModelBuilder.CreatePersonAsync(model);
            
            return RedirectToActionPermanent("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Entered data is incorrect!");

            return View("Edit", await _viewModelBuilder.BuildEditViewModelAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditPersonsViewModel model)
        {
            if (!TryValidateModel(model) || id == Guid.Empty)
                return BadRequest("Entered data is incorrect!");

            await _viewModelBuilder.EditPersonAsync(id, model);
            
            return RedirectToActionPermanent("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _viewModelBuilder.DeletePersonAsync(id);

                return RedirectToActionPermanent("Index");
            }
            catch (PersonIsUsedException)
            {
                return BadRequest("Person is used by something else.");
            }
        }
    }
}