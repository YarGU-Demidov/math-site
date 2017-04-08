using System;
using System.Collections.Generic;

namespace MathSite.Models
{
	/// <summary>
	/// </summary>
	public class Group
	{
		/// <summary>
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// </summary>
		public Guid? ParentGroupId { get; set; }

		/// <summary>
		/// </summary>
		public Group ParentGroup { get; set; }

		/// <summary>
		/// </summary>
		public Guid? GroupTypeId { get; set; }

		/// <summary>
		/// </summary>
		public GroupType GroupType { get; set; }

		/// <summary>
		/// </summary>
		public List<GroupsRights> GroupsRights { get; set; }

		/// <summary>
		/// </summary>
		public List<User> Users { get; set; }

		/// <summary>
		/// </summary>
		public List<PostGroupsAllowed> PostGroupsAllowed { get; set; }
	}
}