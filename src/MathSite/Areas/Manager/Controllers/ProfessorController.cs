using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Professors;
using MathSite.Db.DataSeeding.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Authorize(RightAliases.AdminAccess)]
    [Area("manager")]
    [Route("[area]/[controller]")]
    public class ProfessorController : Controller
    {
        private readonly IProfessorViewModelBuilder _modelBuilder;

        public ProfessorController(IProfessorViewModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        [Route("")]
        [Route("index")]
        [Route("list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View(await _modelBuilder.BuildListViewModel(page, perPage));
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View(await _modelBuilder.BuildCreateViewModelAsync());
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProfessorViewModel model)
        {
            try
            {
                await _modelBuilder.CreateProfessorAsync(model);

                return RedirectToActionPermanent("Index", "Professor");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == default)
                return BadRequest("Entered data is incorrect!");

            return View("Edit", await _modelBuilder.BuildEditViewModelAsync(id));
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(EditProfessorViewModel model)
        {
            if (model.Id == default)
                return BadRequest("Entered data is incorrect!");

            try
            {
                await _modelBuilder.EditProfessorAsync(model);

                return RedirectToActionPermanent("Index", "Professor");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _modelBuilder.DeleteProfessorAsync(id);

                return RedirectToActionPermanent("Index", "Professor");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}