using System;

namespace MathSite.Models
{
	public class Person
	{
		public Person() {}

		/// <summary>
		/// Создает сущность.
		/// </summary>
		/// <param name="name">Имя.</param>
		/// <param name="surname">Фамилия.</param>
		/// <param name="middlename">Отчество.</param>
		/// <param name="birthday">Дата рождения.</param>
		/// <param name="phoneNumber">Телефонный номер.</param>
		/// <param name="additionalPhoneNumber">Дополнительный телефонный номер.</param>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <param name="photoId">Идентификатор изображения личности.</param>
		/// <param name="creationDate">Дата регистрации.</param>
		public Person(string name, string surname, string middlename,
			DateTime birthday, string phoneNumber, string additionalPhoneNumber, Guid? userId, Guid? photoId,
			DateTime creationDate)
		{
			Name = name;
			Surname = surname;
			MiddleName = middlename;
			Phone = phoneNumber;
			AdditionalPhone = additionalPhoneNumber;
			Birthday = birthday;
			CreationDate = creationDate;
			PhotoId = photoId;
			UserId = userId;
		}

		/// <summary>
		/// Идентификатор.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Имя.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Фамилия.
		/// </summary>
		public string Surname { get; set; }

		/// <summary>
		/// Отчество.
		/// </summary>
		public string MiddleName { get; set; }

		/// <summary>
		/// Телефонный номер.
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// Дополнительный телефонный номер.
		/// </summary>
		public string AdditionalPhone { get; set; }

		/// <summary>
		/// Дата рождения.
		/// </summary>
		public DateTime Birthday { get; set; }

		/// <summary>
		/// Дата регистрации.
		/// </summary>
		public DateTime? CreationDate { get; set; }

		/// <summary>
		/// Идентификатор изображения личности.
		/// </summary>
		public Guid? PhotoId { get; set; }

		/// <summary>
		/// Файл.
		/// </summary>
		public File Photo { get; set; }

		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public Guid? UserId { get; set; }

		/// <summary>
		/// Пользователь.
		/// </summary>
		public User User { get; set; }
	}
}