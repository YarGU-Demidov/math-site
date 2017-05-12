using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Domain.LogicValidation;
using MathSite.Models;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Persons
{
	public class PersonsLogic : LogicBase, IPersonsLogic
	{
		private const string PersonNotFoundFormat = "Личность с Id='{0}' не найдена";
		private const string PersonEstablishedFormat = "Пользователь с Id='{0}' уже зарегистрирован";

		private readonly ICurrentUserAccessValidation _userValidation;

		public PersonsLogic(IMathSiteDbContext contextManager,
			ICurrentUserAccessValidation userValidation) : base(contextManager)
		{
			_userValidation = userValidation;
		}

		/// <summary>
		///		Асинхронно создает личность.
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
				var user = await context.Users.AnyAsync(i => i.Id == userId);
				if (user)
					throw new Exception(string.Format(PersonEstablishedFormat, userId));

				var person = new Person(name, surname, middlename, birthday, phoneNumber, additionalPhoneNumber, userId,
					photoId);

				context.Persons.Add(person);
				await context.SaveChangesAsync();

				personId = person.Id;
			});

			return personId;
		}

		/// <summary>
		///		Асинхронно обновляет личность.
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
			_userValidation.CheckCurrentUserExistence(currentUserId);
			await _userValidation.CheckCurrentUserRightsAsync(currentUserId);

			await UseContextAsync(async context =>
			{
				var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == personId);
				if (person == null)
					throw new Exception(string.Format(PersonNotFoundFormat, personId));

				person.Name = name;
				person.Surname = surname;
				person.MiddleName = middlename;
				person.Birthday = birthday;
				person.Phone = phoneNumber;
				person.AdditionalPhone = additionalPhoneNumber;
				person.PhotoId = photoId;

				await context.SaveChangesAsync();
			});
		}

		/// <summary>
		///		Асинхронно удаляет личность.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="personId">Идентификатор личности.</param>
		public async Task DeletePersonAsync(Guid currentUserId, Guid personId)
		{
			_userValidation.CheckCurrentUserExistence(currentUserId);
			await _userValidation.CheckCurrentUserRightsAsync(currentUserId);

			await UseContextAsync(async context =>
			{
				var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == personId);
				if (person == null)
					throw new Exception(string.Format(PersonNotFoundFormat, personId));

				context.Persons.Remove(person);
				await context.SaveChangesAsync();
			});
		}

		/// <summary>
		///		Возвращает результат из перечня личностей.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		public TResult GetFromPersons<TResult>(Func<IQueryable<Person>, TResult> getResult)
		{
			return GetFromItems(i => i.Persons, getResult);
		}

		/// <summary>
		///		Асинхронно возвращает результат из перечня личностей.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		public Task<TResult> GetFromPersonsAsync<TResult>(Func<IQueryable<Person>, Task<TResult>> getResult)
		{
			return GetFromItems(i => i.Persons, getResult);
		}
	}
}