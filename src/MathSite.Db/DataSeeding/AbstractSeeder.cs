using System;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding
{
	/// <summary>
	///     Seeder, заполняющий таблицу данными
	/// </summary>
	public abstract class AbstractSeeder : ISeeder
	{
		/// <summary>
		///     Создание объекта Seeder-а
		/// </summary>
		/// <param name="logger">Логгер</param>
		/// <param name="context">Контекст базы сайта</param>
		public AbstractSeeder(ILogger logger, MathSiteDbContext context)
		{
			Logger = logger;
			Context = context;
		}

		/// <summary>
		///     Логгер
		/// </summary>
		protected ILogger Logger { get; }

		/// <summary>
		///     Контекст базы сайта
		/// </summary>
		protected MathSiteDbContext Context { get; }

		/// <inheritdoc />
		public void Dispose()
		{
			Context.SaveChanges();
		}

		/// <inheritdoc />
		public abstract string SeedingObjectName { get; }

		/// <inheritdoc />
		public virtual bool CanSeed => !DbContainsEntities() && ShouldSeed();

		/// <summary>
		///		Возвращает 
		/// </summary>
		/// <returns>Есть ли сущности в базе</returns>
		protected abstract bool DbContainsEntities();

		protected virtual bool ShouldSeed()
		{
			return true;
		}

		/// <inheritdoc />
		public void Seed()
		{
			if (!CanSeed)
			{
				throw new NotSupportedException($"Can't seed {SeedingObjectName}");
			}

			SeedData();
		}

		/// <summary>
		///		Заполнение данными
		/// </summary>
		protected abstract void SeedData();
	}
}