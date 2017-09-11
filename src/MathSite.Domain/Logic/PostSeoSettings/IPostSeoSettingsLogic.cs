using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.PostSeoSettings
{
	public interface IPostSeoSettingsLogic
	{
		Task<Guid> CreateAsync(
			string url,
			string title,
			string description
		);

		Task UpdateAsync(
			Guid id,
			string url,
			string title,
			string description
		);

		Task DeleteAsync(Guid id);

		Task<PostSeoSetting> TryGetByIdAsync(Guid id);
		Task<PostSeoSetting> TryGetByUrlAsync(string url);
	}
}