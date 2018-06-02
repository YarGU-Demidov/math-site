﻿using System;
using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Pages;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    public class PagesController : BaseController
    {
        private readonly IPagesViewModelBuilder _viewModelBuilder;

        public PagesController(IUserValidationFacade userValidationFacade, IPagesViewModelBuilder pagesViewModelBuilder, IUsersFacade usersFacade)
            : base(userValidationFacade, usersFacade)
        {
            _viewModelBuilder = pagesViewModelBuilder;
        }

        public async Task<IActionResult> Index(string query)
        {
            try
            {
                return View(await _viewModelBuilder.BuildPageItemViewModelAsync(CurrentUserId ?? Guid.Empty, query));
            }
            catch (PostNotFoundException)
            {
                return NotFound();
            }
        }
    }
}