using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Settings;
using MathSite.Controllers;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Area("manager")]
    [Authorize("admin")]
    public class SettingsController : BaseController
    {
        private readonly ISettingsViewModelBuilder _modelBuilder;

        // GET
        public SettingsController(
            IUserValidationFacade userValidationFacade, 
            IUsersFacade usersFacade,
            ISettingsViewModelBuilder modelBuilder
        ) 
            : base(userValidationFacade, usersFacade)
        {
            _modelBuilder = modelBuilder;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _modelBuilder.BuildIndexViewModelAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(IndexSettingsViewModel model)
        {
            if (!TryValidateModel(model))
            {
                BadRequest("Page Model is wrong.");
            }

            await _modelBuilder.SaveData(CurrentUser, model);
            return RedirectToActionPermanent("Index");
        }
    }
}