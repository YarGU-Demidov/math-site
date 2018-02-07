using System;
using System.Threading.Tasks;

namespace MathSite.ViewModels.Events
{
    public interface IEventsViewModelBuilder
    {
        Task<EventsIndexViewModel> BuildIndexViewModelAsync(int page);
        Task<EventItemViewModel> BuildNewsItemViewModelAsync(Guid currentUserId, string query, int page = 1);
    }
}