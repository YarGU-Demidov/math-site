using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Dtos;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public interface INewsManagerViewModelBuilder
    {
        Task<ListNewsViewModel> BuildIndexViewModel(int page, int perPage);
        Task<ListNewsViewModel> BuildRemovedViewModel(int page, int perPage);
        Task<NewsViewModel> BuildCreateViewModel();
        Task<NewsViewModel> BuildCreateViewModel(NewsViewModel news);
        Task<NewsViewModel> BuildEditViewModel(Guid id);
        Task<NewsViewModel> BuildEditViewModel(NewsViewModel news);
        Task<ListNewsViewModel> BuildDeleteViewModel(Guid id);
    }
}
