using System;
using System.Threading.Tasks;
using MathSite.Domain.Logic.Files;
using MathSite.Domain.Logic.Persons;
using Xunit;

namespace MathSite.Tests.Domain.Logic
{
	public class PersonsLogicTests : DomainTestsBase
	{
		[Fact]
		public async Task TryGet_Found()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var personsLogic = new PersonsLogic(context);

				var id = await CreatePersonAsync(personsLogic);

				var file = await personsLogic.TryGetByIdAsync(id);

				Assert.NotNull(file);
			});
		}

		[Fact]
		public async Task TryGet_NotFound()
		{
			var id = Guid.NewGuid();

			await ExecuteWithContextAsync(async context =>
			{
				var personsLogic = new PersonsLogic(context);
				var file = await personsLogic.TryGetByIdAsync(id);

				Assert.Null(file);
			});
		}

		[Fact]
		public async Task CreatePersonTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var personsLogic = new PersonsLogic(context);
				var filesLogic = new FilesLogic(context);

				var name = "test-person-name-new";
				var surname = "test-person-name-new";
				var middlename = "test-person-name-new";
				var phone = "+78889991100";
				var additionalPhone = "78889991122";
				var birthday = DateTime.Today.AddYears(-25);
				var photo = await filesLogic.CreateFileAsync("test file", "/home/test/filePath.jpg", "jpg");

				var personId = await CreatePersonAsync(
					personsLogic,
					name,
					surname,
					middlename,
					birthday,
					phone,
					additionalPhone,
					photo
				);

				var person = await personsLogic.TryGetByIdAsync(personId);
				
				Assert.NotNull(person);
				Assert.Equal(name, person.Name);
				Assert.Equal(surname, person.Surname);
				Assert.Equal(middlename, person.MiddleName);
				Assert.Equal(phone, person.Phone);
				Assert.Equal(additionalPhone, person.AdditionalPhone);
				Assert.Equal(birthday, person.Birthday);
				Assert.Equal(photo, person.PhotoId);
			});
		}

		[Fact]
		public async Task UpdatePersonTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var personsLogic = new PersonsLogic(context);
				var filesLogic = new FilesLogic(context);

				var name = "test-person-name-new";
				var surname = "test-person-surname-new";
				var middlename = "test-person-middlename-new";
				var phone = "+78889991100";
				var additionalPhone = "78889991122";
				var birthday = DateTime.Today.AddYears(-25);
				var photo = await filesLogic.CreateFileAsync("test file", "/home/test/filePath.jpg", "jpg");

				var personId = await CreatePersonAsync(personsLogic);

				await personsLogic.UpdatePersonAsync(personId, name, surname, middlename, birthday, phone, additionalPhone, photo);

				var person = await personsLogic.TryGetByIdAsync(personId);

				Assert.NotNull(person);
				Assert.Equal(name, person.Name);
				Assert.Equal(surname, person.Surname);
				Assert.Equal(middlename, person.MiddleName);
				Assert.Equal(phone, person.Phone);
				Assert.Equal(additionalPhone, person.AdditionalPhone);
				Assert.Equal(birthday, person.Birthday);
				Assert.Equal(photo, person.PhotoId);
			});
		}

		[Fact]
		public async Task DeletePersonTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var personsLogic = new PersonsLogic(context);

				var id = await CreatePersonAsync(personsLogic);

				await personsLogic.DeletePersonAsync(id);

				var person = await personsLogic.TryGetByIdAsync(id);

				Assert.Null(person);
			});
		}

		private async Task<Guid> CreatePersonAsync(
			IPersonsLogic logic, 
			string name = null, 
			string surname = null, 
			string middlename = null, 
			DateTime? birthday = null,
			string phone = null,
			string additionalPhone = null,
			Guid? photoId = null
		)
		{
			var salt = Guid.NewGuid();

			var personName = name ?? $"test-person-name-{salt}";
			var personSurname = surname ?? $"test-person-surname-{salt}";
			var personMiddlename = middlename ?? $"test-person-middlename-{salt}";
			var personBirthday = birthday ?? DateTime.UtcNow.AddYears(-50);
			var personPhone = phone ?? "+79998887766";
			var personAdditionalPhone = additionalPhone ?? "+76665554433";

			return await logic.CreatePersonAsync(personName, personSurname, personMiddlename, personBirthday, personPhone,
				personAdditionalPhone, photoId);
		}
	}
}