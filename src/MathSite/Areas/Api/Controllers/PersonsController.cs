using System;
using System.Linq;
using MathSite.Controllers;
using MathSite.Core.DataTableApi;
using MathSite.Core.Responses;
using MathSite.Core.Responses.ResponseTypes;
using MathSite.Db;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Api.Persons;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
    [Area("Api")]
    public class PersonsController : BaseController, IDataTableApi<Person, PersonsSortData>
    {
        public PersonsController(IUserValidationFacade userValidationFacade, MathSiteDbContext dbContext, IUsersFacade usersFacade) 
            : base(userValidationFacade, usersFacade)
        {
            DbContext = dbContext;
        }

        public MathSiteDbContext DbContext { get; }

        [HttpPost]
        public GetAllResponse<Person> GetAll(int offset = 0, int count = 50,
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

                return new GetAllResponse<Person>(new SuccessResponseType(), null, data);
            }
            catch (Exception exception)
            {
                return new GetAllResponse<Person>(new ErrorResponseType(), exception.Message);
            }
        }

        [HttpGet]
        public GetCountResponse GetCount()
        {
            try
            {
                return new GetCountResponse(new SuccessResponseType(), null, DbContext.Persons.Count());
            }
            catch (Exception exception)
            {
                return new GetCountResponse(new ErrorResponseType(), exception.Message);
            }
        }
    }
}