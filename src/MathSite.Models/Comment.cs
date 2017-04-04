﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class Comment
	{
		/// <summary>
		/// Идентификатор.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool Allowed { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid PostId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Post Post { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public User User { get; set; }
	}
}