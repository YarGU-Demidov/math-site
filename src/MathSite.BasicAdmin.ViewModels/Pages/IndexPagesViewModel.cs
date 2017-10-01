using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public class IndexPagesViewModel : AdminPageWithPagingViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}