using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Facades.Persons
{
    public interface IPersonsFacade : IFacade
    {
        Task<int> GetPersonsCountAsync(int perPage, bool cache);
        Task<IEnumerable<Person>> GetPersonsAsync(int page, int perPage, bool cache);
        Task<Guid> CreatePersonAsync(Person person, File photo = null);
        Task<Person> GetPersonAsync(Guid id);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(Person person, bool force = false);
    }
}