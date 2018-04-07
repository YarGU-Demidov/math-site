using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Categories
{
    public class IndexCategoriesViewModel : AdminPageWithPagingViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}