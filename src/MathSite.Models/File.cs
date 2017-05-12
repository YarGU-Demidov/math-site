using System;
using System.Collections.Generic;

namespace MathSite.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class File
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string FilePath { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Extension { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime DateAdded { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Person Person { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public List<PostSettings> PostSettings { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public List<PostAttachment> PostAttachments { get; set; }
	}
}