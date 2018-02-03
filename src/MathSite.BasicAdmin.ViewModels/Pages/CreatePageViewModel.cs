using System;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public class CreatePageViewModel : AdminPageWithPagingViewModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
    }
}
