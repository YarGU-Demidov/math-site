using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public interface INewsManagerViewModelBuilder
    {
        Task<IndexNewsViewModel> BuildIndexViewModel(int page, int perPage);
        Task<IndexNewsViewModel> BuildRemovedViewModel(int page, int perPage);
        Task<NewsViewModel> BuildCreateViewModel(Post post = null);
        Task<NewsViewModel> BuildEditViewModel(Guid id);
        Task<NewsViewModel> BuildEditViewModel(Post post);
        Task<IndexNewsViewModel> BuildDeleteViewModel(Guid id);
    }
}
