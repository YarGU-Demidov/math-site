using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration
{
	/// <inheritdoc />
	public abstract class AbstractEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
	{
		protected abstract string TableName { get; }

		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<T> modelBuilder)
		{
			SetKeys(modelBuilder);
			SetIndexes(modelBuilder);
			SetFields(modelBuilder);
			SetRelationships(modelBuilder);

			modelBuilder.ToTable(TableName);
		}

		/// <summary>
		///     Установка первичного ключа
		/// </summary>
		/// <param name="modelBuilder">Билдер моделей</param>
		protected abstract void SetKeys(EntityTypeBuilder<T> modelBuilder);

		/// <summary>
		///     Установка параметров полей сущностей
		/// </summary>
		/// <param name="modelBuilder">Билдер моделей</param>
		protected abstract void SetFields(EntityTypeBuilder<T> modelBuilder);

		/// <summary>
		///     Установка отношений между сущностями
		/// </summary>
		/// <param name="modelBuilder">Билдер моделей</param>
		protected abstract void SetRelationships(EntityTypeBuilder<T> modelBuilder);

		protected abstract void SetIndexes(EntityTypeBuilder<T> modelBuilder);
	}
}