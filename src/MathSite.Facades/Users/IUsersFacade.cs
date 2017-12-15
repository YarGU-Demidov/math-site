using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Facades.Users
{
    public interface IUsersFacade : IFacade
    {
        Task<int> GetUsersCountAsync(int perPage, bool cache);
        Task<IEnumerable<User>> GetUsersAsync(int page, int perPage, bool cache);

        Task<User> GetCurrentUserAsync(string possibleUserId);
        Task<User> GetCurrentUserAsync(Guid possibleUserId);

        /// <summary>
        ///     Выполняет проверку существования текущего пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор текущего пользователя.</param>
        Task<bool> DoesUserExistsAsync(Guid userId);

        /// <summary>
        ///     Выполняет проверку существования текущего пользователя.
        /// </summary>
        /// <param name="login">Логин текущего пользователя.</param>
        Task<bool> DoesUserExistsAsync(string login);
    }
}