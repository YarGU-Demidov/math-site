using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Responses;
using MathSite.Controllers;
using MathSite.Core.DataTableApi;
using MathSite.Db;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Api.Persons;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
    [Area("Api")]
    public class PersonsController : BaseController, IDataTableApi<PersonsSortData>
    {
        public PersonsController(IUserValidationFacade userValidationFacade, MathSiteDbContext dbContext, IUsersFacade usersFacade) 
            : base(userValidationFacade, usersFacade)
        {
            DbContext = dbContext;
        }

        private MathSiteDbContext DbContext { get; }

        [HttpPost]
        public IResponse GetAll(int offset = 0, int count = 50,
            [FromBody] FilterAndSortData<PersonsSortData> filterAndSortData = null)
        {
            try
            {
                var usersDbRequest = DbContext.Persons;

//				if (filterAndSortData?.SortData != null)
//				{
//					if (filterAndSortData.SortData.GroupSort != SortDirection.Default)
//					{
//						var isAscending = filterAndSortData.SortData.GroupSort == SortDirection.Ascending;
//						usersDbRequest = usersDbRequest.OrderBy(usersDbRequest, user => user.Group.Name, isAscending)
//							.Include(user => user.Group);
//					}
//				}

                // TODO: избавиться от костыля, EF7 делает не корректный запрос с Include,
                // а делать подзапросы отдельно не хочется.
                // Утверждается, что ко 2й версии может появится возможность делать запросы вручную.
                var persons = usersDbRequest
                    .Skip(offset)
                    .ToArray()
                    .Take(count)
                    .ToArray();

                var data = persons.Length > 0
                    ? persons.Select(u => new Person(u)).ToArray()
                    : new Person[0];

                return new SuccessResponse<Person[]>(data);
            }
            catch (Exception exception)
            {
                return new ErrorResponse(exception.Message);
            }
        }

        [HttpGet]
        public async Task<IResponse> GetCount()
        {
            try
            {
                return new SuccessResponse<int>(await UsersFacade.GetUsersCountAsync(cache: false));
            }
            catch (Exception exception)
            {
                return new ErrorResponse(exception.Message);
            }
        }
    }
}