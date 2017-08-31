using System;

namespace MathSite.Db.DataSeeding
{
	/// <summary>
	///     Конкретный seeder, добавляющий данные для конкретной сущности
	/// </summary>
	public interface ISeeder : IDisposable
	{
		/// <summary>
		///     Имя сущности в базе, которую заполняем
		/// </summary>
		string SeedingObjectName { get; }

		/// <summary>
		///     Можно ли обновлять данные
		/// </summary>
		bool CanSeed { get; }

		/// <summary>
		///     Заполнение данными
		/// </summary>
		void Seed();
	}
}