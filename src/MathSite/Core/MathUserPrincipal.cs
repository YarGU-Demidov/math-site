using System.Security.Claims;
using System.Security.Principal;

namespace MathSite.Core
{
	public class MathUser : IIdentity
	{
		public string AuthenticationType { get; }
		public bool IsAuthenticated { get; }
		public string Name { get; }
	}

	public class MathUserPrincipal : ClaimsPrincipal
	{
		
	}
}