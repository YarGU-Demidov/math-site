using System.Threading.Tasks;
using SimpleMvcSitemap;

namespace MathSite.ViewModels.Home
{
    public interface IHomeViewModelBuilder
    {
        Task<HomeIndexViewModel> BuildIndexModel();
        Task<SitemapModel> GenerateSiteMap();
    }
}