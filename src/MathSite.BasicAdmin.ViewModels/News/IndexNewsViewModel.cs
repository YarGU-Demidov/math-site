using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public class IndexNewsViewModel : AdminPageBaseViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}