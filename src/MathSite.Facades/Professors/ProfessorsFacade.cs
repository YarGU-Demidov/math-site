using System;
using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Professors
{
    public interface IProfessorsFacade
    {
        Task<Professor> GetProfessorAsync(Guid id);
    }

    public class ProfessorsFacade : BaseFacade<IProfessorsRepository, Professor>, IProfessorsFacade
    {
        public ProfessorsFacade(
            IRepositoryManager repositoryManager, 
            IMemoryCache memoryCache
        ) : base(repositoryManager, memoryCache)
        {
        }

        public async Task<Professor> GetProfessorAsync(Guid id)
        {
            return await Repository.WithPerson()
                .FirstOrDefaultAsync(id);
        }
    }
}