using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Persons
{
    public class IndexPersonsViewModel : AdminPageWithPagingViewModel
    {
        public IEnumerable<Person> Persons { get; set; }
    }
}