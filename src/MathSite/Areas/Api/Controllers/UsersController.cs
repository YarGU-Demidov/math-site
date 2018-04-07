using System.Threading.Tasks;
using MathSite.Controllers;
using MathSite.Entities;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
    [Area("api")]
    public class UsersController : BaseController
    {
        public UsersController(
            IUserValidationFacade userValidationFacade, 
            IUsersFacade usersFacade
        ) : base(userValidationFacade, usersFacade)
        {
        }

        [HttpGet]
        public async Task<User> GetByLogin(string login)
        {
            var user = await UsersFacade.GetUserByLoginAsync(login);

            user.PasswordHash = null;

            return user;
        }
    }
}