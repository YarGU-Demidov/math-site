using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Domain.LogicValidation;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Users
{
	public class UsersLogic : LogicBase<User>, IUsersLogic
	{
		private const string PersonNotFoundFormat = "Личность с Id={0} не найдена";
		private const string UserNotFoundFormat = "Пользователь с Id='{0}' не найдена";
		private const string GroupNotFoundFormat = "Группа с Id='{0}' не найдена";

		private readonly ICurrentUserAccessValidation _userValidation;

		public UsersLogic(IMathSiteDbContext contextManager,
			ICurrentUserAccessValidation userValidation) : base(contextManager)
		{
			_userValidation = userValidation;
		}

		/// <summary>
		///     Асинхронно создает личность.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="passwordHash">Пароль.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		public async Task<Guid> CreateUserAsync(string login, byte[] passwordHash, Guid groupId)
		{
			var userId = Guid.Empty;
			await UseContextAsync(async context =>
			{
				var group = await context.Groups.AnyAsync(p => p.Id == groupId);
				if (!group)
					throw new Exception(string.Format(GroupNotFoundFormat, groupId));

				var user = new User(login, passwordHash, groupId);

				context.Users.Add(user);
				await context.SaveChangesAsync();

				userId = user.Id;
			});

			return userId;
		}

		/// <summary>
		///     Асинхронно обновляет личность.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="passwordHash">Пароль.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <exception cref="Exception">Личность не найдена.</exception>
		public async Task UpdateUserAsync(Guid currentUserId, byte[] passwordHash, Guid groupId)
		{
			_userValidation.CheckCurrentUserExistence(currentUserId);
			await _userValidation.CheckCurrentUserRightsAsync(currentUserId);

			await UseContextAsync(async context =>
			{
				var group = await context.Groups.AnyAsync(p => p.Id == groupId);
				if (!group)
					throw new Exception(string.Format(GroupNotFoundFormat, groupId));

				var user = await context.Users.FirstOrDefaultAsync(p => p.Id == currentUserId);
				if (user == null)
					throw new Exception(string.Format(UserNotFoundFormat, currentUserId));

				user.PasswordHash = passwordHash;
				user.GroupId = groupId;

				await context.SaveChangesAsync();
			});
		}

		/// <summary>
		///     Асинхронно удаляет личность.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="personId">Идентификатор личности.</param>
		public async Task DeleteUserAsync(Guid currentUserId, Guid personId)
		{
			_userValidation.CheckCurrentUserExistence(currentUserId);
			await _userValidation.CheckCurrentUserRightsAsync(currentUserId);

			await UseContextAsync(async context =>
			{
				var user = await context.Users.FirstOrDefaultAsync(p => p.Id == currentUserId);
				if (user == null)
					throw new Exception(string.Format(UserNotFoundFormat, currentUserId));

				context.Users.Remove(user);
				await context.SaveChangesAsync();
			});
		}

		/// <summary>
		///     Возвращает результат из перечня прав пользователя.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		public TResult GetUserRights<TResult>(Func<IQueryable<UsersRights>, TResult> getResult)
		{
			return GetFromItems(getResult);
		}

		/// <summary>
		///     Асинхронно возвращает результат из перечня прав пользователя.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		public Task<TResult> GetUserRightsAsync<TResult>(Func<IQueryable<UsersRights>, Task<TResult>> getResult)
		{
			return GetFromItems(getResult);
		}
	}
}