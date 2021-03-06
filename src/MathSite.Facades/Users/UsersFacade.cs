﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Common.Extensions;
using MathSite.Common.Specifications;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.UserValidation;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.Users;

namespace MathSite.Facades.Users
{
    public class UsersFacade : BaseMathFacade<IUsersRepository, User>, IUsersFacade
    {
        private readonly IUserValidationFacade _validationFacade;
        private readonly IPasswordsManager _passwordsManager;

        public UsersFacade(
            IRepositoryManager repositoryManager,
            IUserValidationFacade validationFacade,
            IPasswordsManager passwordsManager
        )
            : base(repositoryManager)
        {
            _validationFacade = validationFacade;
            _passwordsManager = passwordsManager;
        }

        // TODO: FIXME: Extract to classes or smth else
        public async Task<int> GetUsersPagesCountAsync(int perPage)
        {
            var usersCount = await GetUsersCountAsync();

            return GetPagesCount(perPage, usersCount);
        }

        public async Task<int> GetUsersCountAsync()
        {
            var requirements = new AnySpecification<User>();
            return await GetCountAsync(requirements);
        }


        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Repository.WithPerson().GetAllListAsync();
        }

        // TODO: FIXME: Extract to classes or smth else
        public async Task<IEnumerable<User>> GetUsersAsync(int page, int perPage)
        {
            return await GetItemsForPageAsync(repository => repository.WithPerson(), page, perPage);
        }

        public async Task<User> GetUserAsync(string possibleUserId)
        {
            if (possibleUserId.IsNullOrWhiteSpace())
                return null;

            var userIdGuid = Guid.Parse(possibleUserId);

            return await GetUserAsync(userIdGuid);
        }

        public async Task<User> GetUserAsync(Guid possibleUserId)
        {
            if (possibleUserId == default)
                return null;

            return await Repository
                .WithPerson()
                .FirstOrDefaultAsync(possibleUserId);
        }

        public async Task<bool> DoesUserExistsAsync(Guid userId)
        {
            return await Repository.FirstOrDefaultAsync(userId) != null;
        }
        public async Task<bool> DoesUserExistsAsync(string login)
        {
            var requirements = new HasLoginSpecification(login);

            return await RepositoryManager.UsersRepository.FirstOrDefaultAsync(requirements) != null;
        }

        public async Task CreateUserAsync(Guid currentUser, Guid personId, string login, string password, Guid groupId)
        {
            var canCreate = await _validationFacade.UserHasRightAsync(currentUser, RightAliases.AdminAccess);

            if (!canCreate)
                throw new AccessViolationException();

            var passHash = _passwordsManager.CreatePassword(login, password);

            var user = new User(login, passHash, groupId) {PersonId = personId};

            await Repository.InsertAsync(user);
        }
        
        public async Task UpdateUserAsync(Guid currentUser, Guid id, Guid? personId = null, Guid? groupId = null, string newPassword = null)
        {
            var canUpdate = await _validationFacade.UserHasRightAsync(currentUser, RightAliases.AdminAccess);

            if (!canUpdate)
                throw new AccessViolationException();

            var user = await GetUserAsync(id);

            if (newPassword.IsNotNullOrWhiteSpace())
            {
                var passHash = _passwordsManager.CreatePassword(user.Login, newPassword);
                user.PasswordHash = passHash;
            }
            
            if (groupId.HasValue)
                user.GroupId = groupId.Value;

            if (personId.HasValue)
                user.PersonId = personId.Value;

            await Repository.UpdateAsync(user);
        }

        public async Task RemoveUser(Guid currentUser, Guid id)
        {
            var canUpdate = await _validationFacade.UserHasRightAsync(currentUser, RightAliases.AdminAccess);

            if (!canUpdate)
                throw new AccessViolationException();

            await Repository.DeleteAsync(id);
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await Repository.WithPerson().FirstOrDefaultAsync(new HasLoginSpecification(login));
        }
    }
}