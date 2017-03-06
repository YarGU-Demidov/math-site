using System;
using System.Linq;
using System.Collections.Generic;
using MathSite.Controllers;
using MathSite.Core;
using MathSite.Db;
using MathSite.ViewModels.Api.UsersInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class UsersInfo : BaseController, IApiCompatible<UserInfo>
	{
		public UsersInfo(IMathSiteDbContext dbContext) : base(dbContext)
		{
		}

		public UserInfo GetCurrentUserInfo()
		{
			if (CurrentUser == null)
				return new UserInfo("Guest", "", "", "Guest", null);

			var person = CurrentUser.Person;
			return new UserInfo(person.Name, person.Surname, person.MiddleName, CurrentUser.Login, CurrentUser.Group);
		}

		public IActionResult GetUserInfo(string id)
		{
			Guid uId;

			if (!Guid.TryParse(id, out uId))
			{
				return NotFound(id);
			}

			var user = DbContext.Users
				.Include(u => u.Person)
				.Include(u => u.Group)
					.ThenInclude(g => g.GroupsRights)
						.ThenInclude(gr => gr.Right)
				.FirstOrDefault(u => u.Id == uId);

			if (user == null)
			{
				return NotFound();
			}

			var person = user.Person;
			return Json(new UserInfo(person.Name, person.Surname, person.MiddleName, user.Login, user.Group));
		}

		public IEnumerable<UserInfo> GetAll()
		{
			throw new NotImplementedException();
		}

		public IActionResult SaveAll(IEnumerable<UserInfo> items)
		{
			throw new NotImplementedException();
		}
	}
}