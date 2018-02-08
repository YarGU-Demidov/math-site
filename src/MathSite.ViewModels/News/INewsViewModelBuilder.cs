using System;
using System.Threading.Tasks;

namespace MathSite.ViewModels.News
{
    public interface INewsViewModelBuilder
    {
        Task<NewsIndexViewModel> BuildIndexViewModelAsync(int page);
        Task<NewsByCategoryViewModel> BuildByCategoryViewModelAsync(string categoryQuery, int page);
        Task<NewsItemViewModel> BuildNewsItemViewModelAsync(Guid currentUserId, string query, int page = 1);
    }
}