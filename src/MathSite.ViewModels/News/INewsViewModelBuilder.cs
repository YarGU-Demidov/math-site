using System.Threading.Tasks;

namespace MathSite.ViewModels.News
{
	public interface INewsViewModelBuilder
	{
		Task<NewsIndexViewModel> BuildIndexViewModelAsync(int page);
		Task<NewsItemViewModel> BuildNewsItemViewModelAsync(string query, int page = 1);
	}
}