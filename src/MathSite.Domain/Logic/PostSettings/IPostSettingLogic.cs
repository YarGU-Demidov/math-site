using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.PostSettings
{
	public interface IPostSettingLogic
	{
		Task<Guid> CreateAsync(
			bool isCommentsAllowed,
			bool canBeRated,
			bool postOnStartPage,
			Guid? previewImageId
		);
		
		Task UpdateAsync(
			Guid id,
			bool isCommentsAllowed,
			bool canBeRated,
			bool postOnStartPage,
			Guid? previewImageId
		);

		Task DeleteAsync(Guid id);

		Task<PostSetting> TryGetByIdAsync(Guid id);
	}
}