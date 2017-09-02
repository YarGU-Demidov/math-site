using MathSite.Facades.SiteSettings;

namespace MathSite.ViewModels.Pages
{
	public interface IPagesViewModelBuilder
	{
		
	}

	public class PagesViewModelBuilder : CommonViewModelBuilder, IPagesViewModelBuilder

	{
		public PagesViewModelBuilder(ISiteSettingsFacade siteSettingsFacade) : base(siteSettingsFacade)
		{
		}

		protected override string PageTitle { get; set; }
	}
}