﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Extensions;
using MathSite.Controllers;
using MathSite.Core.DataTableApi;
using MathSite.Core.Responses;
using MathSite.Core.Responses.ResponseTypes;
using MathSite.Db;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.Repository;
using MathSite.ViewModels.Api.UsersInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Areas.Api.Controllers
{
    [Area("Api")]
    public class UsersInfoController : BaseController, IDataTableApi<UserInfo, UsersSortData>
    {
        private readonly IUsersRepository _usersLogic;

        public UsersInfoController(
            IUserValidationFacade userValidationFacade, 
            MathSiteDbContext dbContext,
            IUsersRepository usersLogic, 
            IUsersFacade usersFacade
        )
            : base(userValidationFacade, usersFacade)
        {
            _usersLogic = usersLogic;
            DbContext = dbContext;
        }

        public MathSiteDbContext DbContext { get; }

        [HttpGet]
        public async Task<GetCountResponse> GetCount()
        {
            try
            {
                return new GetCountResponse(new SuccessResponseType(), null, await UsersFacade.GetUsersCountAsync(false));
            }
            catch (Exception exception)
            {
                return new GetCountResponse(new ErrorResponseType(), exception.Message);
            }
        }

        [HttpPost]
        public GetAllResponse<UserInfo> GetAll(int offset = 0, int count = 50,
            [FromBody] FilterAndSortData<UsersSortData> filterAndSortData = null)
        {
            try
            {
                var usersDbRequest = DbContext.Users
                    .Include(u => u.Person)
                    .Include(u => u.Group);

                if (filterAndSortData?.SortData != null)
                    if (filterAndSortData.SortData.GroupSort != SortDirection.Default)
                    {
                        var isAscending = filterAndSortData.SortData.GroupSort == SortDirection.Ascending;
                        usersDbRequest = usersDbRequest.OrderBy(user => user.Group.Name, isAscending)
                            .Include(user => user.Group);
                    }

                // TODO: избавиться от костыля, EF7 делает не корректный запрос с Include,
                // а делать подзапросы отдельно не хочется.
                // Утверждается, что ко 2й версии может появится возможность делать запросы вручную.
                var users = usersDbRequest
                    .Skip(offset)
                    .ToArray()
                    .Take(count)
                    .ToArray();

                var data = users.Length > 0
                    ? users.Select(u => new UserInfo(u)).ToArray()
                    : new UserInfo[0];

                return new GetAllResponse<UserInfo>(new SuccessResponseType(), null, data);
            }
            catch (Exception exception)
            {
                return new GetAllResponse<UserInfo>(new ErrorResponseType(), exception.Message);
            }
        }

        public async Task<UserInfo> GetCurrentUserInfo()
        {
            return CurrentUserId == null
                ? new UserInfo(null, null, null, null, null)
                : new UserInfo(await _usersLogic.FirstOrDefaultAsync(CurrentUserId.Value));
        }

        public IActionResult GetUserInfo(string id)
        {
            if (!Guid.TryParse(id, out var uId))
                return NotFound(id);

            var user = DbContext.Users
                .Where(u => u.Id == uId)
                .Include(u => u.Person)
                .Include(u => u.Group)
                .FirstOrDefault();

            if (user == null)
                return NotFound();

            return Json(new UserInfo(user));
        }

        public IActionResult SaveAll(IEnumerable<UserInfo> items)
        {
            throw new NotImplementedException();
        }
    }
}