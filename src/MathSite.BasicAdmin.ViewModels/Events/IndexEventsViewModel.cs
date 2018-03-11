using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Events
{
    public class IndexEventsViewModel : AdminPageWithPagingViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}