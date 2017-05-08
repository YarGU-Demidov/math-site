using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Models;

namespace MathSite.Domain.Logic.Users
{
    public class UsersLogic : LogicBase, IUsersLogic
    {
	    public UsersLogic(IMathSiteDbContext contextManager) : base(contextManager)
	    {
	    }

	    TResult IUsersLogic.GetFromUsers<TResult>(Func<IQueryable<User>, TResult> getResult)
	    {
		    return GetFromUsers(getResult);
	    }

	    Task<TResult> IUsersLogic.GetFromUsersAsync<TResult>(Func<IQueryable<User>, Task<TResult>> getResult)
	    {
		    return GetFromUsersAsync(getResult);
	    }

	    TResult IUsersLogic.GetUserRights<TResult>(Func<IQueryable<UsersRights>, TResult> getResult)
	    {
		    return GetUserRights(getResult);
	    }

	    Task<TResult> IUsersLogic.GetUserRightsAsync<TResult>(Func<IQueryable<UsersRights>, Task<TResult>> getResult)
	    {
		    return GetUserRightsAsync(getResult);
	    }

		/// <summary>
		/// Возвращает результат из перечня пользователей.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		private TResult GetFromUsers<TResult>(Func<IQueryable<User>, TResult> getResult)
	    {
			return GetFromItems(i => i.Users, getResult);
		}

		/// <summary>
		/// Асинхронно возвращает результат из перечня пользователей.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		private Task<TResult> GetFromUsersAsync<TResult>(Func<IQueryable<User>, Task<TResult>> getResult)
	    {
		    return GetFromItems(i => i.Users, getResult);
	    }

	    /// <summary>
	    /// Возвращает результат из перечня прав пользователя.
	    /// </summary>
	    /// <typeparam name="TResult">Тип результата.</typeparam>
	    /// <param name="getResult">Метод получения результата.</param>
	    private TResult GetUserRights<TResult>(Func<IQueryable<UsersRights>, TResult> getResult)
	    {
		    return GetFromItems(i => i.UsersRights, getResult);
	    }

		/// <summary>
		/// Асинхронно возвращает результат из перечня прав пользователя.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		private Task<TResult> GetUserRightsAsync<TResult>(Func<IQueryable<UsersRights>, Task<TResult>> getResult)
	    {
		    return GetFromItems(i => i.UsersRights, getResult);
	    }
	}
}
