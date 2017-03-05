using System.Collections.Generic;
using MathSite.Controllers;
using MathSite.Core;
using MathSite.Db;
using MathSite.ViewModels.Api.UsersInfo;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class UsersInfo: BaseController, IApiCompatible<UserInfo>
	{
		public UsersInfo(IMathSiteDbContext dbContext) : base(dbContext)
		{
		}

		public UserInfo GetCurrentUserInfo()
		{
			if(CurrentUser == null)
				return new UserInfo("Guest", "", "", "Guest");

			var person = CurrentUser.Person;
			return new UserInfo(person.Name, person.Surname, person.MiddleName, CurrentUser.Login);
		}
/*
		[HttpGet("get-user-info/{id}")]
		public UserInfo GetUserInfo(string id)
		{

		}*/
		public IEnumerable<UserInfo> GetAll()
		{
			throw new System.NotImplementedException();
		}

		public IActionResult SaveAll(IEnumerable<UserInfo> items)
		{
			throw new System.NotImplementedException();
		}
	}
}