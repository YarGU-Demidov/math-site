using System.Collections.Generic;
using MathSite.ViewModels.SharedModels.PostPreview;

namespace MathSite.ViewModels.Home
{
	public class HomeIndexViewModel : CommonViewModel
	{
		public IEnumerable<PostPreviewViewModel> Posts { get; set; }
	}
}