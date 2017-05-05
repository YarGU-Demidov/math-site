using System;
using System.Linq;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Models;

namespace MathSite.Domain.Logic.Persons
{
	public class PersonsLogic : LogicBase, IPersonsLogic
	{
		public PersonsLogic(IMathSiteDbContext contextManager) : base(contextManager)
		{
		}

		//Task<Person> IPersonsLogic.GetPersonAsync(string userId)
		//{
		//	return GetPersonAsync(userId);
		//}

		TResult IPersonsLogic.GetFromPersons<TResult>(Func<IQueryable<Person>, TResult> getResult)
		{
			return GetFromPersons(getResult);
		}

		///// <summary>
		///// Асинхронно возвращает личность.
		///// </summary>
		///// <param name="personId">Идентификатор личности.</param>
		///// <exception cref="Exception">Личность не найдена.</exception>
		//private Task<Person> GetPersonAsync(string personId)
		//{
		//	if (string.IsNullOrEmpty(personId))
		//		throw new Exception();

		//	var result = GetFromItemsAsync(async allPersons =>
		//	{
		//		var persons = allPersons;

		//		return await persons.FirstOrDefaultAsync(i => i.Surname == personId);
		//	});

		//	if (result == null)
		//		throw new Exception();

		//	return null;
		//}

		/// <summary>
		/// Возвращает результат из перечня личностей.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		private TResult GetFromPersons<TResult>(Func<IQueryable<Person>, TResult> getResult)
		{
			return GetFromItems(i => i.Persons, getResult);
		}
	}
}