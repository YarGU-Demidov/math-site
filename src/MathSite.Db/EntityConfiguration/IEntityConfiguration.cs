using Microsoft.EntityFrameworkCore;

namespace MathSite.Db.EntityConfiguration
{
	/// <summary>
	///		Конфигурация сущности
	/// </summary>
	public interface IEntityConfiguration
	{
		/// <summary>
		///		Имя конфигурации
		/// </summary>
		string ConfigurationName { get; }

		/// <summary>
		///		Запуск конфигурирования
		/// </summary>
		/// <param name="modelBuilder">Билдер моделей</param>
		void Configure(ModelBuilder modelBuilder);
	}
}