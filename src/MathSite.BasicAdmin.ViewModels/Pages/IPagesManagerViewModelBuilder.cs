using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public interface IPagesManagerViewModelBuilder
    {
        Task<ListPagesViewModel> BuildIndexViewModel(int page, int perPage);
        Task<ListPagesViewModel> BuildRemovedViewModel(int page, int perPage);
        Task<PageViewModel> BuildCreateViewModel();
        Task<PageViewModel> BuildCreateViewModel(PageViewModel page);
        Task<PageViewModel> BuildEditViewModel(Guid id);
        Task<PageViewModel> BuildEditViewModel(PageViewModel page);
        Task<ListPagesViewModel> BuildDeleteViewModel(Guid id);
    }
}
