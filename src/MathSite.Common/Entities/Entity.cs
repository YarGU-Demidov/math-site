using System;
using System.Collections.Generic;
using System.Reflection;

namespace MathSite.Common.Entities
{
    /// <inheritdoc cref="Entity{TPrimaryKey}" />
    /// <summary>
    ///     A shortcut of <see cref="T:MathSite.Common.Entities.Entity`1" /> for most used primary key type (
    ///     <see cref="T:System.Guid" />).
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<Guid>, IEntity
    {
    }

    /// <summary>
    ///     Basic implementation of IEntity interface.
    ///     An entity can inherit this class of directly implement to IEntity interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>, IEquatable<Entity<TPrimaryKey>>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Unique identifier for this entity.
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Checks if this entity is transient (it has not an Id).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        public virtual bool IsTransient()
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default))
                return true;

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(TPrimaryKey) == typeof(int))
                return Convert.ToInt32(Id) <= 0;

            if (typeof(TPrimaryKey) == typeof(long))
                return Convert.ToInt64(Id) <= 0;

            if (typeof(TPrimaryKey) == typeof(Guid))
                return Guid.Parse(Id.ToString()) == Guid.Empty;

            return false;
        }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public bool Equals(Entity<TPrimaryKey> other)
        {
            return Equals((object) other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TPrimaryKey>))
                return false;

            //Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
                return true;

            //Transient objects are not considered as equal
            var other = (Entity<TPrimaryKey>) obj;
            if (IsTransient() && other.IsTransient())
                return false;

            //Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) &&
                !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
                return false;

            return Id.Equals(other.Id);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return Equals(left, null)
                ? Equals(right, null)
                : left.Equals(right);
        }

        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left == right);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{GetType().Name} {Id} {CreationDate:F}:{CreationDate.Millisecond}]";
        }
    }
}