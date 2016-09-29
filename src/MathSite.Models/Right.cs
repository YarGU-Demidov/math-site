using System;
using System.Collections.Generic;

namespace MathSite.Models
{
	public class Right
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public List<GroupsRights> GroupsRights { get; set; }
		public List<UsersRights> UsersRights { get; set; }

		protected bool Equals(Right other)
		{
			return Id.Equals(other.Id) && string.Equals(Name, other.Name) && string.Equals(Description, other.Description);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Right) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Id.GetHashCode();
				hashCode = (hashCode*397) ^ (Name?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (Description?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}