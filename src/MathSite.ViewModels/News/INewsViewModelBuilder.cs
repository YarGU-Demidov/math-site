using System.Threading.Tasks;

namespace MathSite.ViewModels.News
{
	public interface INewsViewModelBuilder
	{
		Task<NewsIndexViewModel> BuildIndexViewModelAsync();
		Task<NewsItemViewModel> BuildNewsItemViewModelAsync(string query, int page = 1);
	}
}