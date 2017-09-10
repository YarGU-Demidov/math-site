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
		public PersonsLogic(MathSiteDbContext contextManager)
			: base(contextManager)
		{
		}
		
		public async Task<Guid> CreatePersonAsync(string name, string surname, string middlename,
			DateTime birthday, string phoneNumber, string additionalPhoneNumber, Guid? photoId)
		{
			var personId = Guid.Empty;
			await UseContextAsync(async context =>
			{
				var person = new Person(name, surname, middlename, birthday, phoneNumber, additionalPhoneNumber, photoId);

				context.Persons.Add(person);
				await context.SaveChangesAsync();

				personId = person.Id;
			});

			return personId;
		}
		
		public async Task UpdatePersonAsync(Guid personId, string name, string surname, string middlename,
			DateTime birthday, string phoneNumber, string additionalPhoneNumber, Guid? photoId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var person = await TryGetByIdAsync(personId);

				person.Name = name;
				person.Surname = surname;
				person.MiddleName = middlename;
				person.Birthday = birthday;
				person.Phone = phoneNumber;
				person.AdditionalPhone = additionalPhoneNumber;
				person.PhotoId = photoId;
			});
		}
		
		public async Task DeletePersonAsync(Guid personId)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var person = await TryGetByIdAsync(personId);

				context.Persons.Remove(person);
			});
		}

		public async Task<Person> TryGetByIdAsync(Guid id)
		{
			return await GetFromItemsAsync(persons => persons.FirstOrDefaultAsync(p => p.Id == id));
		}
	}
}