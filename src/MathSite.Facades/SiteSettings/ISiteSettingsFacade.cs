using System;
using System.Threading.Tasks;

namespace MathSite.Facades.SiteSettings
{
    public interface ISiteSettingsFacade : IFacade
    {
        Task<string> this[string name] { get; }
        Task<string> GetStringSettingAsync(string name, bool cache);
        Task<bool> SetStringSettingAsync(Guid userId, string name, string value);
    }
}