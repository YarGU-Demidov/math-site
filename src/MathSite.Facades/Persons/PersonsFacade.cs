using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Common.Extensions;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
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

        public async Task<Guid> CreatePersonAsync(Person person, File photo = null)
        {
            if (photo.IsNotNull())
                person.PhotoId = photo?.Id;

            return await Repository.InsertAndGetIdAsync(person);
        }

        public Task<Person> GetPersonAsync(Guid id)
        {
            return Repository.WithUser().GetAsync(id);
        }

        public async Task UpdatePersonAsync(Person person)
        {
            await Repository.UpdateAsync(person);
        }

        public async Task DeletePersonAsync(Person person, bool force = false)
        {
            if (person.IsNull())
                throw new ArgumentNullException(nameof(person));

            if (!force)
            {
                var hasUser = person.UserId != null && person.UserId != Guid.Empty;

                // TODO: дописать проверку на использование персоны!!!
                if (hasUser)
                    throw new PersonIsUsedException();
            }

            await Repository.DeleteAsync(person.Id);
        }
    }
}