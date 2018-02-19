using System;
using System.Threading.Tasks;
using MathSite.Entities.Dtos;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public interface INewsManagerViewModelBuilder
    {
        Task<IndexNewsViewModel> BuildIndexViewModel(int page, int perPage);
        Task<IndexNewsViewModel> BuildRemovedViewModel(int page, int perPage);
        Task<NewsViewModel> BuildCreateViewModel(PostDto postDto = null);
        Task<NewsViewModel> BuildEditViewModel(Guid id);
        Task<NewsViewModel> BuildEditViewModel(PostDto postDto);
        Task<IndexNewsViewModel> BuildDeleteViewModel(Guid id);
    }
}
