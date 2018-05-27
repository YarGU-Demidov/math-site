using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;

namespace MathSite.Facades.Professors
{
    public interface IProfessorsFacade
    {
        Task<Professor> GetProfessorAsync(Guid id);
        Task<int> GetPagesCountAsync(int perPage);
        Task<IEnumerable<Professor>> GetProfessorsForPage(int page, int perPage);
        Task<Guid> CreateAsync(Professor professor);
        Task UpdateAsync(Professor professor);
        Task DeleteAsync(Guid id);
    }

    public class ProfessorsFacade : BaseMathFacade<IProfessorsRepository, Professor>, IProfessorsFacade
    {
        public ProfessorsFacade(
            IRepositoryManager repositoryManager
        ) : base(repositoryManager)
        {
        }

        public async Task<Professor> GetProfessorAsync(Guid id)
        {
            return await Repository.WithPerson()
                .FirstOrDefaultAsync(id);
        }

        public async Task<int> GetPagesCountAsync(int perPage)
        {
            var total = await GetCountAsync(new AnySpecification<Professor>());
            
            return GetPagesCount(perPage, total);
        }

        public async Task<IEnumerable<Professor>> GetProfessorsForPage(int page, int perPage)
        {
            return await GetItemsForPageAsync(repository => repository.WithPerson(), page, perPage);
        }

        public async Task<Guid> CreateAsync(Professor professor)
        {
            return await Repository.InsertAndGetIdAsync(professor);
        }

        public async Task UpdateAsync(Professor professor)
        {
            await Repository.UpdateAsync(professor);
        }

        public async Task DeleteAsync(Guid id)
        {
            await Repository.DeleteAsync(id);
        }
    }
}