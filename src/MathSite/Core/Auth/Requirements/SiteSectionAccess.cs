using Microsoft.AspNetCore.Authorization;

namespace MathSite.Core.Auth.Requirements
{
	public class SiteSectionAccess : IAuthorizationRequirement
	{
		public SiteSectionAccess(string sectionName)
		{
			SectionName = sectionName;
		}

		public string SectionName { get; }
	}
}