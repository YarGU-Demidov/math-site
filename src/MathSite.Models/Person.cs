using System;

namespace MathSite.Models
{
	public class Person
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string MiddleName { get; set; }
		
		public Guid? UserId { get; set; }

		public User User { get; set; }
	}
}