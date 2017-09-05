using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.PostSeoSettings
{
	public interface IPostSeoSettingsLogic
	{
		Task<Guid> CreateSeoSettingsAsync(
			string url,
			string title,
			string description
		);

		Task UpdateSeoSettingsAsync(
			Guid id,
			string url,
			string title,
			string description
		);

		Task DeleteSeoSettingsAsync(Guid id);

		Task<PostSeoSetting> TryGetByIdAsync(Guid id);
		Task<PostSeoSetting> TryGetByUrlAsync(string url);
	}
}