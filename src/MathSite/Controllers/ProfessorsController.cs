using System;
using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.ViewModels.Professors;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    [Route("[controller]/[action]")]
    public class ProfessorsController : Controller
    {
        private readonly IProfessorsViewModelBuilder _modelBuilder;

        public ProfessorsController(IProfessorsViewModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Show(Guid id)
        {
            try
            {
                return View(await _modelBuilder.BuildShowViewModelAsync(id));
            }
            catch (EntityNotFoundException )
            {
                return NotFound();
            }
        }
    }
}