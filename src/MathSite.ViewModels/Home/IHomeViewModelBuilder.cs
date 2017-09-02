using System.Threading.Tasks;

namespace MathSite.ViewModels.Home
{
	public interface IHomeViewModelBuilder
	{
		Task<HomeIndexViewModel> BuildIndexModel();
	}
}