using System;
using System.Threading.Tasks;

namespace MathSite.Facades.SiteSettings
{
    public interface ISiteSettingsFacade : IFacade
    {
        Task<int> GetPerPageCountAsync(bool cache = true, int defaultCount = 18);
        Task<string> GetTitleDelimiter(bool cache = true);
        Task<string> GetDefaultHomePageTitle(bool cache = true);
        Task<string> GetDefaultNewsPageTitle(bool cache = true);
        Task<string> GetSiteName(bool cache = true);

        Task<bool> SetPerPageCountAsync(Guid userId, string perPageCount);
        Task<bool> SetTitleDelimiter(Guid userId, string titleDelimiter);
        Task<bool> SetDefaultHomePageTitle(Guid userId, string defaultHomePageTitle);
        Task<bool> SetDefaultNewsPageTitle(Guid userId, string defaultNewsPageTitle);
        Task<bool> SetSiteName(Guid userId, string siteName);
    }
}