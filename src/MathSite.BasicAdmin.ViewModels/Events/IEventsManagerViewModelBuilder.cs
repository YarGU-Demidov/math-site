using System;
using System.Threading.Tasks;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.BasicAdmin.ViewModels.Events
{
    public interface IEventsManagerViewModelBuilder
    {
        Task<ListEventsViewModel> BuildIndexViewModel(int page, int perPage);
        Task<ListEventsViewModel> BuildRemovedViewModel(int page, int perPage);
        Task<EventViewModel> BuildCreateViewModel();
        Task<EventViewModel> BuildCreateViewModel(EventViewModel page);
        Task<EventViewModel> BuildEditViewModel(Guid id);
        Task<EventViewModel> BuildEditViewModel(EventViewModel page);
        Task<ListEventsViewModel> BuildDeleteViewModel(Guid id);
        Task BuildRecoverViewModel(Guid postId);
        Task BuildForceDeleteViewModel(Guid postId);
        void FillPostItemViewModel(SecondaryViewModel model);
    }
}
