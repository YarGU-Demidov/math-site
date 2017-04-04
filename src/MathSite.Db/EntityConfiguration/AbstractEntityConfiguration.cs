using Microsoft.EntityFrameworkCore;

namespace MathSite.Db.EntityConfiguration
{
	/// <inheritdoc />
	public abstract class AbstractEntityConfiguration : IEntityConfiguration
	{
		/// <summary>
		///		Установка первичного ключа
		/// </summary>
		/// <param name="modelBuilder">Билдер моделей</param>
		protected abstract void SetPrimaryKey(ModelBuilder modelBuilder);

		/// <summary>
		///		Установка параметров полей сущностей
		/// </summary>
		/// <param name="modelBuilder">Билдер моделей</param>
		protected abstract void SetFields(ModelBuilder modelBuilder);

		/// <summary>
		///		Установка отношений между сущностями
		/// </summary>
		/// <param name="modelBuilder">Билдер моделей</param>
		protected abstract void SetRelationships(ModelBuilder modelBuilder);

		/// <inheritdoc />
		public abstract string ConfigurationName { get; }

		/// <inheritdoc />
		public virtual void Configure(ModelBuilder modelBuilder)
		{
			SetPrimaryKey(modelBuilder);
			SetFields(modelBuilder);
			SetRelationships(modelBuilder);
		}
	}
}