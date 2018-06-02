using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.Posts
{
    public class ListPostViewModel : AdminPageWithPagingViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}