using System;
using System.Linq;
using MathSite.Models;

namespace MathSite.Domain.Logic.Persons
{
	public interface IPersonsLogic
	{
		///// <summary>
		///// Асинхронно возвращает личность.
		///// </summary>
		///// <param name="personId">Идентификатор личности.</param>
		///// <exception cref="Exception">Личность не найдена.</exception>
		//Task<Person> GetPersonAsync(string personId);

		/// <summary>
		/// Возвращает результат из перечня личностей.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		TResult GetFromPersons<TResult>(Func<IQueryable<Person>, TResult> getResult);

		///// <summary>
		///// Асинхронно обновляет личность.
		///// </summary>
		///// <param name="personId">Идентификатор личности.</param>
		///// <param name="name">Имя.</param>
		///// <param name="surname">Фамилия.</param>
		///// <param name="middlename">Отчество.</param>
		///// <param name="birthday">Дата рождения.</param>
		///// <param name="phoneNumber">Номер телефона.</param>
		///// <param name="additionalPhoneNumber">Дополнительный номер телефона.</param>
		///// <exception cref="Exception">Личность не найдена.</exception>    
		//Task UpdatePersonAsync(string personId, string name, string surname, string middlename,
		//	string birthday, string phoneNumber, string additionalPhoneNumber);
	}
}