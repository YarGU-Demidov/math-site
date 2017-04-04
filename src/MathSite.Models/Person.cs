using System;

namespace MathSite.Models
{
	public class Person
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string MiddleName { get; set; }
		public string Phone { get; set; }
		public string AdditionalPhone { get; set; }
		public DateTime Birthday { get; set; }
		public Guid PhotoId { get; set; }
		/*public File Photo { get; set; }*/

		public Guid? UserId { get; set; }
		public User User { get; set; }
	}
}