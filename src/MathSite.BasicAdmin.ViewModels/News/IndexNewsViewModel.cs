using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public class IndexNewsViewModel : AdminPageWithPagingViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}