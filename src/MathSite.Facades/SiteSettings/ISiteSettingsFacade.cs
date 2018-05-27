using System;
using System.Threading.Tasks;

namespace MathSite.Facades.SiteSettings
{
    public interface ISiteSettingsFacade : IFacade
    {
        Task<int> GetPerPageCountAsync(int defaultCount = 18);
        Task<string> GetTitleDelimiter();
        Task<string> GetDefaultHomePageTitle();
        Task<string> GetDefaultNewsPageTitle();
        Task<string> GetSiteName();

        Task<bool> SetPerPageCountAsync(Guid userId, string perPageCount);
        Task<bool> SetTitleDelimiter(Guid userId, string titleDelimiter);
        Task<bool> SetDefaultHomePageTitle(Guid userId, string defaultHomePageTitle);
        Task<bool> SetDefaultNewsPageTitle(Guid userId, string defaultNewsPageTitle);
        Task<bool> SetSiteName(Guid userId, string siteName);
    }
}