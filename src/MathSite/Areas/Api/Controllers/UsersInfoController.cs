using System;
using System.Linq;
using System.Collections.Generic;
using MathSite.Controllers;
using MathSite.Db;
using MathSite.ViewModels.Api.UsersInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class UsersInfoController : BaseController
	{
		public UsersInfoController(IMathSiteDbContext dbContext) : base(dbContext)
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
			Guid uId;

			if (!Guid.TryParse(id, out uId))
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

		public IEnumerable<UserInfo> GetAll(int offset = 0, int count = 50)
		{
			var users = DbContext.Users.Skip(offset).Take(count).Include(u => u.Group).Include(u => u.Person).ToArray();

			return users.Length > 0 
				? users.Select(u => new UserInfo(u)).ToArray()
				: new UserInfo[0];
		}

		public IActionResult SaveAll(IEnumerable<UserInfo> items)
		{
			throw new NotImplementedException();
		}
	}
}