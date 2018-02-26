using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public interface IPagesManagerViewModelBuilder
    {
        Task<IndexPagesViewModel> BuildIndexViewModel(int page, int perPage);
        Task<IndexPagesViewModel> BuildRemovedViewModel(int page, int perPage);
        Task<PageViewModel> BuildCreateViewModel();
        Task<PageViewModel> BuildCreateViewModel(PageViewModel page);
        Task<PageViewModel> BuildEditViewModel(Guid id);
        Task<PageViewModel> BuildEditViewModel(PageViewModel page);
        Task<IndexPagesViewModel> BuildDeleteViewModel(Guid id);
    }
}
