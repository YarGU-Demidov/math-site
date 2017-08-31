using System;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Domain.LogicValidation;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.SiteSettings
{
	public class SiteSettingsLogic : LogicBase<Entities.SiteSettings>, ISiteSettingsLogic
	{
		private const string AlreadyExistsFormat = "Key '{0}' already exists in database!";

		private readonly ICurrentUserAccessValidation _accessValidation;

		public SiteSettingsLogic(IMathSiteDbContext context, ICurrentUserAccessValidation accessValidation)
			: base(context)
		{
			_accessValidation = accessValidation;
		}

		public async Task<Guid> CreateSettingAsync(Guid currentUser, string key, byte[] value)
		{
			_accessValidation.CheckCurrentUserExistence(currentUser);
			await _accessValidation.CheckCurrentUserRightsAsync(currentUser);

			var id = Guid.Empty;
			await UseContextAsync(async context =>
			{
				var sameKeyExists = await context.SiteSettings.AnyAsync(settings => settings.Key == key);

				if (sameKeyExists)
					throw new ArgumentException(string.Format(AlreadyExistsFormat, key));

				var setting = new Entities.SiteSettings(key, value);
				await context.SiteSettings.AddAsync(setting);

				id = setting.Id;
			});

			return id;
		}

		public Task UpdateSettingAsync(Guid currentUser, Guid id, string key, byte[] value)
		{
			throw new NotImplementedException();
		}

		public Task<Guid> DeleteSettingAsync(Guid currentUser, Guid id, string key, byte[] value)
		{
			throw new NotImplementedException();
		}
	}
}