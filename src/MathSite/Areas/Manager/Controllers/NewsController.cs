using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.News;
using MathSite.BasicAdmin.ViewModels.SharedModels;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Controllers;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Area("manager")]
    [Authorize("admin")]
    public class NewsController : BaseController
    {
        private readonly INewsManagerViewModelBuilder _modelBuilder;
        private readonly IPostsFacade _posts;

        public NewsController(IUserValidationFacade userValidationFacade, INewsManagerViewModelBuilder modelBuilder, IPostsFacade posts) 
            : base(userValidationFacade)
        {
            _modelBuilder = modelBuilder;
            _posts = posts;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _modelBuilder.BuildIndexViewModel());
        }

        public async Task<IActionResult> Create(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}