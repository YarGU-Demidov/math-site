using System;
using MathSite.Entities;

namespace MathSite.ViewModels.Api.UsersInfo
{
	public class UserInfo
	{
		public UserInfo(string name, string surname, string middleName, string nick, GroupInfo group)
		{
			Name = name;
			Surname = surname;
			MiddleName = middleName;
			Nick = nick;
			Group = group;
		}

		public UserInfo(User user)
		{
			Id = user.Id;
			Name = user.Person.Name;
			Surname = user.Person.Surname;
			MiddleName = user.Person.MiddleName;

			Nick = user.Login;

			if (user.Group != null)
				Group = new GroupInfo(user.GroupId, user.Group.Alias, user.Group.Name, user.Group.Description);
		}

		public Guid Id { get; }
		public string Name { get; }
		public string Surname { get; }
		public string MiddleName { get; }
		public string Nick { get; }
		public GroupInfo Group { get; }
	}
}