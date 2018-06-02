using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Professors
{
    public class ProfessorListViewModel : AdminPageWithPagingViewModel
    {
        public IEnumerable<Professor> Professors { get; set; }
    }
}