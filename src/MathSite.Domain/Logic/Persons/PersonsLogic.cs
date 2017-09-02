using System;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Persons
{
	public class PersonsLogic : LogicBase<Person>, IPersonsLogic
	{
		public PersonsLogic(IMathSiteDbContext contextManager)
			: base(contextManager)
		{
		}

		/// <summary>
		///     Асинхронно создает личность.
		/// </summary>
		/// <param name="name">Имя.</param>
		/// <param name="surname">Фамилия.</param>
		/// <param name="middlename">Отчество.</param>
		/// <param name="birthday">Дата рождения.</param>
		/// <param name="phoneNumber">Номер телефона.</param>
		/// <param name="additionalPhoneNumber">Дополнительный номер телефона.</param>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <param name="photoId">Идентификатор изображения личности.</param>
		/// <exception cref="Exception">Личность не найдена.</exception>
		public async Task<Guid> CreatePersonAsync(string name, string surname, string middlename,
			DateTime birthday, string phoneNumber, string additionalPhoneNumber, Guid? userId, Guid? photoId)
		{
			var personId = Guid.Empty;
			await UseContextAsync(async context =>
			{
				var person = new Person(name, surname, middlename, birthday, phoneNumber, additionalPhoneNumber, userId,
					photoId);

				context.Persons.Add(person);
				await context.SaveChangesAsync();

				personId = person.Id;
			});

			return personId;
		}

		/// <summary>
		///     Асинхронно обновляет личность.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="personId">Идентификатор личности.</param>
		/// <param name="name">Имя.</param>
		/// <param name="surname">Фамилия.</param>
		/// <param name="middlename">Отчество.</param>
		/// <param name="birthday">Дата рождения.</param>
		/// <param name="phoneNumber">Номер телефона.</param>
		/// <param name="additionalPhoneNumber">Дополнительный номер телефона.</param>
		/// <param name="photoId">Идентификатор изображения личности.</param>
		/// <exception cref="Exception">Личность не найдена.</exception>
		public async Task UpdatePersonAsync(Guid currentUserId, Guid personId, string name, string surname, string middlename,
			DateTime birthday, string phoneNumber, string additionalPhoneNumber, Guid? photoId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var person = await context.Persons.FirstAsync(p => p.Id == personId);

				person.Name = name;
				person.Surname = surname;
				person.MiddleName = middlename;
				person.Birthday = birthday;
				person.Phone = phoneNumber;
				person.AdditionalPhone = additionalPhoneNumber;
				person.PhotoId = photoId;
			});
		}

		/// <summary>
		///     Асинхронно удаляет личность.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="personId">Идентификатор личности.</param>
		public async Task DeletePersonAsync(Guid currentUserId, Guid personId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var person = await context.Persons.FirstAsync(p => p.Id == personId);

				context.Persons.Remove(person);
			});
		}
	}
}