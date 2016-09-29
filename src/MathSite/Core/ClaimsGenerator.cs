using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using MathSite.Models;

namespace MathSite.Core
{
	public class ClaimsGenerator
	{
		public IEnumerable<Claim> GetUserClaims(User user)
		{
			var userRights = user.UsersRights;
			var groupRights = user.Group.GroupsRights.Where(gr => !userRights.Any(rights => rights.Right.Equals(gr.Right)));

			var claims = new Dictionary<Right, Claim>();

			foreach (var groupRight in groupRights)
			{
				var claim = GetClaimFromRight(groupRight.Right, groupRight.Allowed);
				claims.Add(groupRight.Right, claim);
			}

			foreach (var userRight in userRights)
			{
				var claim = GetClaimFromRight(userRight.Right, userRight.Allowed);

				if (claims.ContainsKey(userRight.Right))
					claims[userRight.Right] = claim;
				else
					claims.Add(userRight.Right, claim);
			}

			return claims.Select(pair => pair.Value);
		}

		private static Claim GetClaimFromRight(Right right, bool allowed)
		{
			return new Claim(right.Name, allowed ? "true" : "false");
		}
	}
}