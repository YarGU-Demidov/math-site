using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.Persons
{
	public interface IPersonsLogic
	{
		/// <summary>
		///     Асинхронно создает личность.
		/// </summary>
		/// <param name="name">Имя.</param>
		/// <param name="surname">Фамилия.</param>
		/// <param name="middlename">Отчество.</param>
		/// <param name="birthday">Дата рождения.</param>
		/// <param name="phoneNumber">Номер телефона.</param>
		/// <param name="additionalPhoneNumber">Дополнительный номер телефона.</param>
		/// <param name="photoId">Идентификатор изображения личности.</param>
		/// <exception cref="Exception">Личность не найдена.</exception>
		Task<Guid> CreatePersonAsync(string name, string surname, string middlename,
			DateTime birthday, string phoneNumber, string additionalPhoneNumber, Guid? photoId);

		/// <summary>
		///     Асинхронно обновляет личность.
		/// </summary>
		/// <param name="personId">Идентификатор личности.</param>
		/// <param name="name">Имя.</param>
		/// <param name="surname">Фамилия.</param>
		/// <param name="middlename">Отчество.</param>
		/// <param name="birthday">Дата рождения.</param>
		/// <param name="phoneNumber">Номер телефона.</param>
		/// <param name="additionalPhoneNumber">Дополнительный номер телефона.</param>
		/// <param name="photoId">Идентификатор изображения личности.</param>
		/// <exception cref="Exception">Личность не найдена.</exception>
		Task UpdatePersonAsync(Guid personId, string name, string surname, string middlename,
			DateTime birthday, string phoneNumber, string additionalPhoneNumber, Guid? photoId);

		/// <summary>
		///     Асинхронно удаляет личность.
		/// </summary>
		/// <param name="personId">Идентификатор личности.</param>
		Task DeletePersonAsync(Guid personId);

		Task<Person> TryGetByIdAsync(Guid id);
	}
}