using Microsoft.EntityFrameworkCore;

namespace MathSite.Db.EntityConfiguration
{
	/// <summary>
	///		Конфигуратор сущностей из базы
	/// </summary>
	public interface IEntitiesConfigurator
	{
		/// <summary>
		///		Добавляет конфигурацию для сущности
		/// </summary>
		/// <param name="configuration">Конфигурация для сущности</param>
		void AddConfiguration(IEntityConfiguration configuration);

		/// <summary>
		///		Запускает конфигурацию сущностей
		/// </summary>
		/// <param name="modelBuilder"></param>
		void Configure(ModelBuilder modelBuilder);
	}
}