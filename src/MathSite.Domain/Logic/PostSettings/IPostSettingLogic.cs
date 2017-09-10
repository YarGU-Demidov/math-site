using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.PostSettings
{
	public interface IPostSettingLogic
	{
		Task<Guid> CreatePostSettings(
			bool? isCommentsAllowed,
			bool? canBeRated,
			bool? postOnStartPage,
			Guid? previewImageId
		);
		
		Task UpdatePostSettings(
			Guid id,
			bool? isCommentsAllowed,
			bool? canBeRated,
			bool? postOnStartPage,
			Guid? previewImageId
		);

		Task DeletePostSettings(Guid id);

		Task<PostSetting> TryGetByIdAsync(Guid id);
	}
}