using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Facades.Users
{
    public interface IUsersFacade
    {
        Task<int> GetUsersCountAsync(int perPage, bool cache);
        Task<IEnumerable<User>> GetUsersAsync(int page, int perPage, bool cache);

        Task<User> GetCurrentUserAsync();
    }
}