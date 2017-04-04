﻿using System;
using System.Linq;
using System.Collections.Generic;
using MathSite.Controllers;
using MathSite.Db;
using MathSite.Models;
using MathSite.ViewModels.Api.UsersInfo;
using MathSite.ViewModels.Api.UsersInfo.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class UsersInfoController : BaseController
	{
		public UsersInfoController(MathSiteDbContext dbContext) : base(dbContext)
		{
		}

		public UserInfo GetCurrentUserInfo()
		{
			return CurrentUser == null
				? new UserInfo("Guest", "Guest", "Guest", "Guest", null)
				: new UserInfo(CurrentUser);
		}

		public IActionResult GetUserInfo(string id)
		{
			if (!Guid.TryParse(id, out Guid uId))
			{
				return NotFound(id);
			}

			var user = DbContext.Users
				.Where(u => u.Id == uId)
				.Include(u => u.Person)
				.Include(u => u.Group)
				.FirstOrDefault();

			if (user == null)
			{
				return NotFound();
			}

			return Json(new UserInfo(user));
		}

		[HttpGet]
		public GetUsersCountResponse GetUsersCount()
		{
			try
			{
				return new GetUsersCountResponse("success", null, DbContext.Users.Count());
			}
			catch (Exception exception)
			{
				return new GetUsersCountResponse("error", exception.Message);
			}
		}

		[HttpGet]
		public GetAllResponse GetAll(int offset = 0, int count = 50)
		{
			try
			{
				// TODO: избавиться от костыля, EF7 делает не корректный запрос с Include,
				// а делать подзапросы отдельно не хочется.
				// Утверждается, что ко 2й версии может появится возможность делать запросы вручную.
				var users = DbContext.Users
					.Include(u => u.Person)
					.Include(u => u.Group)
					.Skip(offset)
					.ToArray()
					.Take(count)
					.ToArray();

				var data = users.Length > 0
					? users.Select(u => new UserInfo(u)).ToArray()
					: new UserInfo[0];

				return new GetAllResponse("success", data);
			}
			catch (Exception exception)
			{
				return new GetAllResponse("error", null, exception.Message);
			}
		}

		public IActionResult SaveAll(IEnumerable<UserInfo> items)
		{
			throw new NotImplementedException();
		}
	}
}