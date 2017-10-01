using System;
using MathSite.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration
{
    public abstract class AbstractEntityConfiguration<T> : AbstractEntityConfiguration<T, Guid> where T : class, IEntity<Guid> { }

    /// <inheritdoc />
    public abstract class AbstractEntityConfiguration<T, TEntityKey> : IEntityTypeConfiguration<T> where T : class, IEntity<TEntityKey>
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
        protected virtual void SetKeys(EntityTypeBuilder<T> modelBuilder)
        {
            modelBuilder.HasKey(model => model.Id);
        }

        /// <summary>
        ///     Установка параметров полей сущностей
        /// </summary>
        /// <param name="modelBuilder">Билдер моделей</param>
        protected virtual void SetFields(EntityTypeBuilder<T> modelBuilder)
        {
            modelBuilder.Property(model => model.CreationDate)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
        }

        /// <summary>
        ///     Установка отношений между сущностями
        /// </summary>
        /// <param name="modelBuilder">Билдер моделей</param>
        protected virtual void SetRelationships(EntityTypeBuilder<T> modelBuilder) { }

        protected virtual void SetIndexes(EntityTypeBuilder<T> modelBuilder) { }
    }
}