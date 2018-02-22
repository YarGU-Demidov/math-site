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
        Task<Guid> CreatePersonAsync(
            string surname, 
            string name, 
            string middlename, 
            DateTime bday,
            string phone = null, 
            string additionalPhone = null, 
            Guid? photoId = null
        );
        Task<Person> GetPersonAsync(Guid id);
        Task UpdatePersonAsync(
            Guid personId, 
            string surname = null, 
            string name = null, 
            string middlename = null, 
            string phone = null, 
            string additionalPhone = null, 
            Guid? photoId = null, 
            DateTime? bday = null
        );
        Task DeletePersonAsync(Guid personId, bool force = false);
        Task<IEnumerable<Person>> GetAvailablePersonsAsync();
    }
}