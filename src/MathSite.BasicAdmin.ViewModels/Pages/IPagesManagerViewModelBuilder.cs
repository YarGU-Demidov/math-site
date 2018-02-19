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
        Task<PageViewModel> BuildCreateViewModel(PostDto postDto = null);
        Task<PageViewModel> BuildEditViewModel(Guid id);
        Task<PageViewModel> BuildEditViewModel(PostDto postDto);
        Task<IndexPagesViewModel> BuildDeleteViewModel(Guid id);
    }
}
