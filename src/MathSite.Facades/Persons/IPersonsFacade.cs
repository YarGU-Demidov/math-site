using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Facades.Persons
{
    public interface IPersonsFacade : IFacade
    {
        Task<int> GetPersonsCountAsync(int perPage, bool cache);
        Task<IEnumerable<Person>> GetPersonsAsync(int page, int perPage, bool cache);
    }
}