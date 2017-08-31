﻿using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	public class Right : IEquatable<Right>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Alias { get; set; }

		public List<GroupsRights> GroupsRights { get; set; } = new List<GroupsRights>();
		public List<UsersRights> UsersRights { get; set; } = new List<UsersRights>();

		/// <inheritdoc />
		public bool Equals(Right other)
		{
			return Id.Equals(other.Id) && string.Equals(Name, other.Name) && string.Equals(Description, other.Description);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Right) obj);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Id.GetHashCode();
				hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}