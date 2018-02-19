using System;
using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Entities.Dtos;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public interface IPagesManagerViewModelBuilder
    {
        Task<IndexPagesViewModel> BuildIndexViewModel(int page, int perPage);
        Task<IndexPagesViewModel> BuildRemovedViewModel(int page, int perPage);
        Task<PageViewModel> BuildCreateViewModel(PostDto post = null);
        Task<PageViewModel> BuildEditViewModel(Guid id);
        Task<PageViewModel> BuildEditViewModel(PostDto post);
        Task<IndexPagesViewModel> BuildDeleteViewModel(Guid id);
    }
}
