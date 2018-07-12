using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Facades.Users
{
    public interface IUsersFacade : IFacade
    {
        Task<int> GetUsersPagesCountAsync(int perPage);
        Task<int> GetUsersCountAsync();

        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetUsersAsync(int page, int perPage);

        Task<User> GetUserAsync(string possibleUserId);
        Task<User> GetUserAsync(Guid possibleUserId);

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

        Task CreateUserAsync(Guid currentUser, Guid personId, string login, string password, Guid groupId);
        Task UpdateUserAsync(Guid currentUser, Guid id, Guid? personId = null, Guid? groupId = null, string newPassword = null);
        Task RemoveUser(Guid currentUser, Guid id);
        Task<User> GetUserByLoginAsync(string login);

        Task<IEnumerable<User>> GetUsersByConversationIdAsync(Guid conversationId);

        Task<User> GetConversationCreatorAsync(Guid conversationId);

        Task<int> GetConversationsMembersCountAsync(Guid conversationId);
    }
}