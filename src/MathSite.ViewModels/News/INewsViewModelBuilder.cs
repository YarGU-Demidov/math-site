using System.Threading.Tasks;

namespace MathSite.ViewModels.News
{
	public interface INewsViewModelBuilder
	{
		Task<NewsIndexViewModel> BuildIndexViewModelAsync();
	}
}