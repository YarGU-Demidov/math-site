using System.Threading.Tasks;

namespace MathSite.ViewModels.Pages
{
    public interface IPagesViewModelBuilder
    {
        Task<PageItemViewModel> BuildPageItemViewModelAsync(string query);
    }
}