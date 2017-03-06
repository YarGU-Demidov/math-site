using MathSite.Models;

namespace MathSite.ViewModels.Api.UsersInfo
{
	public class UserInfo
	{
		public string Name { get; }
		public string Surname { get; }
		public string MiddleName { get; }
		public string Nick { get; }
		public Group Group { get; }

		public UserInfo(string name, string surname, string middleName, string nick, Group group)
		{
			Name = name;
			Surname = surname;
			MiddleName = middleName;
			Nick = nick;
			Group = group;
		}
	}
}