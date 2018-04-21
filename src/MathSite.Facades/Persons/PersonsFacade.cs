using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Common.Extensions;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.Persons;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Persons
{
    public class PersonsFacade : BaseFacade<IPersonsRepository, Person>, IPersonsFacade
    {
        private TimeSpan CacheTime { get; } = TimeSpan.FromMinutes(5);

        public PersonsFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache) 
            : base(repositoryManager, memoryCache)
        {
        }

        // TODO: FIXME: Extract to classes or smth else
        public async Task<int> GetPersonsCountAsync(int perPage, bool cache)
        {
            perPage = perPage > 0 ? perPage : 1;

            var requirements = new AnySpecification<Person>();

            var newsCount = await GetCountAsync(requirements, cache, CacheTime);

            return (int)Math.Ceiling(newsCount / (float)perPage);
        }

        // TODO: FIXME: Extract to classes or smth else
        public async Task<IEnumerable<Person>> GetPersonsAsync(int page, int perPage, bool cache)
        {
            page = page >= 1 ? page : 1;
            perPage = perPage > 0 ? perPage : 1;

            var skip = (page - 1) * perPage;

            return await RepositoryManager.PersonsRepository
                .WithUser()
                .GetAllWithPagingAsync(skip, perPage);
        }

        public async Task<Guid> CreatePersonAsync(
            string surname, 
            string name, 
            string middlename, 
            DateTime bday,
            string phone = null, 
            string additionalPhone = null, 
            Guid? photoId = null
        )
        {
            var person = new Person
            {
                Surname = surname,
                Name = name,
                MiddleName = middlename,
                Birthday = bday
            };


            if (phone.IsNotNull())
                person.Phone = phone;
            
            if (additionalPhone.IsNotNull())
                person.AdditionalPhone = additionalPhone;
            
            if (photoId.HasValue)
                person.PhotoId = photoId.Value;
            
            return await Repository.InsertAndGetIdAsync(person);
        }

        public Task<Person> GetPersonAsync(Guid id)
        {
            return Repository
                .WithUser()
                .WithProfessor()
                .GetAsync(id);
        }

        public async Task UpdatePersonAsync(
            Guid personId, 
            string surname = null, 
            string name = null, 
            string middlename = null, 
            string phone = null, 
            string additionalPhone = null, 
            Guid? photoId = null, 
            DateTime? bday = null
        )
        {
            var person = await GetPersonAsync(personId);

            if (surname.IsNotNull())
                person.Surname = surname;

            if (name.IsNotNull())
                person.Name = name;

            if (middlename.IsNotNull())
                person.MiddleName = middlename;
            
            if (phone.IsNotNull())
                person.Phone = phone;
            
            if (additionalPhone.IsNotNull())
                person.AdditionalPhone = additionalPhone;
            
            if (photoId.HasValue)
                person.PhotoId = photoId.Value;
            
            if (bday.HasValue)
                person.Birthday = bday.Value;

            await Repository.UpdateAsync(person);
        }

        public async Task DeletePersonAsync(Guid personId, bool force = false)
        {
            var person = await GetPersonAsync(personId);

            if (person.IsNull())
                throw new ArgumentNullException(nameof(person));

            if (!force)
            {
                var hasUser = person.User != null;
                var hasProfessor = person.Professor != null;

                // TODO: дописать проверку на использование персоны!!!
                if (hasUser || hasProfessor)
                    throw new PersonIsUsedException();
            }

            await Repository.DeleteAsync(person.Id);
        }

        public async Task<IEnumerable<Person>> GetAvailablePersonsAsync()
        {
            var spec = new AvailablePersonSpecification();

            return await Repository.WithUser().WithProfessor().GetAllListAsync(spec);
        }
    }
}