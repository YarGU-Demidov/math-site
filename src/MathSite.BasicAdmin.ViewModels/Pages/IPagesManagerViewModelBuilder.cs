using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public interface IPagesManagerViewModelBuilder
    {
        Task<IndexPagesViewModel> BuildIndexViewModel(int page, int perPage);
        Task<IndexPagesViewModel> BuildRemovedViewModel(int page, int perPage);
        Task<CreatePageViewModel> BuildCreateViewModel(Post post = null);
        Task<IndexPagesViewModel> BuildDeleteViewModel(Guid id);
    }
}
