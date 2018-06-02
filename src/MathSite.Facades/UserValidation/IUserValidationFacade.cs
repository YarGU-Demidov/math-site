﻿using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Facades.UserValidation
{
    public interface IUserValidationFacade : IFacade
    {
        /// <summary>
        ///     Асинхронно выполняет проверку прав пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="rightAlias">Alias права.</param>
        Task<bool> UserHasRightAsync(Guid userId, string rightAlias);

        /// <summary>
        ///     Асинхронно выполняет проверку прав пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="rightAlias">Alias права.</param>
        Task<bool> UserHasRightAsync(User user, string rightAlias);

        /// <summary>
        ///     Асинхронно выполняет проверку прав текущего пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="right">Право.</param>
        Task<bool> UserHasRightAsync(Guid userId, Right right);

        /// <summary>
        ///     Асинхронно выполняет проверку прав текущего пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="right">Право.</param>
        Task<bool> UserHasRightAsync(User user, Right right);

        Task<User> GetUserByLoginAndPasswordAsync(string login, string password);
    }
}