using System;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Users
{
	public class UsersLogic : LogicBase<User>, IUsersLogic
	{
		public UsersLogic(MathSiteDbContext contextManager) : base(contextManager)
		{
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
		/// <param name="id">Идентификатор пользователя.</param>
		/// <param name="passwordHash">Пароль.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <exception cref="Exception">Личность не найдена.</exception>
		public async Task UpdateUserAsync(Guid id, byte[] passwordHash, Guid groupId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var user = await context.Users.FirstAsync(p => p.Id == id);

				user.PasswordHash = passwordHash;
				user.GroupId = groupId;
			});
		}

		/// <summary>
		///     Асинхронно удаляет личность.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="personId">Идентификатор личности.</param>
		public async Task DeleteUserAsync(Guid id)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var user = await context.Users.FirstAsync(p => p.Id == id);

				context.Users.Remove(user);
			});
		}

		public async Task<User> TryGetByIdAsync(Guid userId)
		{
			User user = null;

			await UseContextAsync(async context => { user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId); });

			return user;
		}

		public async Task<User> TryGetByLoginAsync(string login)
		{
			User user = null;

			await UseContextAsync(async context => { user = await context.Users.FirstOrDefaultAsync(u => u.Login == login); });

			return user;
		}

		public async Task<User> TryGetUserWithRightsById(Guid id)
		{
			User user = null;

			await UseContextAsync(async context =>
			{
				user = await context.Users
					.Include(u => u.UserRights).ThenInclude(ur => ur.Right)
					.Include(u => u.Group).ThenInclude(g => g.GroupsRights).ThenInclude(gr => gr.Right)
					.FirstOrDefaultAsync(u => u.Id == id);
			});

			return user;
		}

		public async Task<User> TryGetUserWithRightsByLogin(string login)
		{
			User user = null;

			await UseContextAsync(async context =>
			{
				user = await context.Users
					.Include(u => u.UserRights).ThenInclude(ur => ur.Right)
					.Include(u => u.Group).ThenInclude(g => g.GroupsRights).ThenInclude(gr => gr.Right)
					.FirstOrDefaultAsync(u => u.Login == login);
			});

			return user;
		}
	}
}