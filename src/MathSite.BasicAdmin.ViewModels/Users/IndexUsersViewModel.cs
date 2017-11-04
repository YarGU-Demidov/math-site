using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Users
{
    public class IndexUsersViewModel : AdminPageWithPagingViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}