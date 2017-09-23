using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Entities;
using MathSite.Repository.Core;
using MathSite.Specifications.Users;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.UserValidation
{
    public class UserValidationFacade : BaseFacade, IUserValidationFacade
    {
        private readonly IPasswordsManager _passwordHasher;

        public UserValidationFacade(
            IRepositoryManager logicManager,
            IMemoryCache memoryCache,
            IPasswordsManager passwordHasher
        )
            : base(logicManager, memoryCache)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> DoesUserExistsAsync(Guid userId)
        {
            var user = await LogicManager.UsersRepository.FirstOrDefaultAsync(userId);
            return user != null;
        }

        public async Task<bool> DoesUserExistsAsync(string login)
        {
            var requirements = new HasLoginSpecification(login);
            var user = await LogicManager.UsersRepository.FirstOrDefaultAsync(requirements.ToExpression());

            return user != null;
        }

        public async Task<bool> UserHasRightAsync(Guid userId, string rightAlias)
        {
            if (userId == Guid.Empty)
                return false;

            var user = await LogicManager.UsersRepository.FirstOrDefaultWithRightsAsync(userId);

            if (user == null)
                return false;

            var userRights = user.UserRights.ToArray();
            var groupRights = user.Group.GroupsRights.ToArray();

            var allowed = user.Group.IsAdmin;

            if (userRights.All(rights => rights.Right.Alias != rightAlias) && !user.Group.IsAdmin)
            {
                foreach (var groupRight in groupRights)
                    if (groupRight.Right.Alias == rightAlias)
                        allowed = groupRight.Allowed;

                return allowed;
            }

            foreach (var userRight in userRights)
                if (userRight.Right.Alias == rightAlias)
                    allowed = userRight.Allowed;

            return allowed;
        }

        public async Task<bool> UserHasRightAsync(User user, string rightAlias)
        {
            return await UserHasRightAsync(user.Id, rightAlias);
        }

        public async Task<bool> UserHasRightAsync(Guid userId, Right right)
        {
            return await UserHasRightAsync(userId, right.Alias);
        }

        public async Task<bool> UserHasRightAsync(User user, Right right)
        {
            return await UserHasRightAsync(user.Id, right.Alias);
        }

        public async Task<User> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            var requirements = new HasLoginSpecification(login);

            var user = await LogicManager.UsersRepository.FirstOrDefaultAsync(requirements.ToExpression());

            if (user == null || !_passwordHasher.PasswordsAreEqual(login, password, user.PasswordHash))
                return null;

            return user;
        }
    }
}