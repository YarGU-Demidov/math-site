using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	public class Right : IEquatable<Right>
	{
		public string Alias { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public List<GroupsRights> GroupsRights { get; set; } = new List<GroupsRights>();
		public List<UsersRights> UsersRights { get; set; } = new List<UsersRights>();

		public bool Equals(Right other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Alias, other.Alias) && string.Equals(Name, other.Name) &&
			       string.Equals(Description, other.Description) && Equals(GroupsRights, other.GroupsRights) &&
			       Equals(UsersRights, other.UsersRights);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Right) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Alias != null ? Alias.GetHashCode() : 0;
				hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (GroupsRights != null ? GroupsRights.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (UsersRights != null ? UsersRights.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}