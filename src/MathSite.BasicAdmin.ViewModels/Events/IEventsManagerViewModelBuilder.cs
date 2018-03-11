using System;
using System.Threading.Tasks;

namespace MathSite.BasicAdmin.ViewModels.Events
{
    public interface IEventsManagerViewModelBuilder
    {
        Task<IndexEventsViewModel> BuildIndexViewModel(int page, int perPage);
        Task<IndexEventsViewModel> BuildRemovedViewModel(int page, int perPage);
        Task<EventViewModel> BuildCreateViewModel();
        Task<EventViewModel> BuildCreateViewModel(EventViewModel page);
        Task<EventViewModel> BuildEditViewModel(Guid id);
        Task<EventViewModel> BuildEditViewModel(EventViewModel page);
        Task<IndexEventsViewModel> BuildDeleteViewModel(Guid id);
    }
}
