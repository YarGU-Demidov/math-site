using System;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Common;
using MathSite.Facades.UserValidation;

namespace MathSite.Facades.SiteSettings
{
	public class SiteSettingsFacade : BaseFacade, ISiteSettingsFacade
	{
		private readonly IUserValidationFacade _userValidation;
		private const string SettingNotFound = "Настройка с именем '{0}' не была найдена.";

		public SiteSettingsFacade(IBusinessLogicManger logicManger, IUserValidationFacade userValidation) : base(logicManger)
		{
			_userValidation = userValidation;
		}

		public Task<string> this[string name] => GetStringSettingAsync(name);

		public async Task<string> GetStringSettingAsync(string name)
		{
			var setting = await LogicManger.SiteSettingsLogic.TryGetByKeyAsync(name);

			return setting != null 
				? Encoding.UTF8.GetString(setting.Value) 
				: null;
		}

		public async Task<bool> SetStringSettingAsync(Guid userId, string name, string value)
		{
			var userExists = await _userValidation.DoesUserExistsAsync(userId);
			if (!userExists)
				return false;

			var hasRight = await _userValidation.UserHasRightAsync(userId, RightAliases.SetSiteSettingsAccess);
			if (!hasRight)
				return false;

			var setting = await LogicManger.SiteSettingsLogic.TryGetByKeyAsync(name);

			var valueBytes = Encoding.UTF8.GetBytes(value);

			if (setting == null)
			{
				await LogicManger.SiteSettingsLogic.CreateSettingAsync(name, valueBytes);
			}
			else
			{
				await LogicManger.SiteSettingsLogic.UpdateSettingAsync(name, valueBytes);
			}
			return true;
		}
	}
}