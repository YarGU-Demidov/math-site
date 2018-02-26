using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Dtos;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public interface INewsManagerViewModelBuilder
    {
        Task<IndexNewsViewModel> BuildIndexViewModel(int page, int perPage);
        Task<IndexNewsViewModel> BuildRemovedViewModel(int page, int perPage);
        Task<NewsViewModel> BuildCreateViewModel();
        Task<NewsViewModel> BuildCreateViewModel(NewsViewModel news);
        Task<NewsViewModel> BuildEditViewModel(Guid id);
        Task<NewsViewModel> BuildEditViewModel(NewsViewModel news);
        Task<IndexNewsViewModel> BuildDeleteViewModel(Guid id);
    }
}
