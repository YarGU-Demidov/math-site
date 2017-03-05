namespace MathSite.ViewModels.Api.UsersInfo
{
	public class UserInfo
	{
		public string Name { get; }
		public string Surname { get; }
		public string MiddleName { get; }
		public string Nick { get; }

		public UserInfo(string name, string surname, string middleName, string nick)
		{
			Name = name;
			Surname = surname;
			MiddleName = middleName;
			Nick = nick;
		}
	}
}