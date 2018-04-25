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

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}