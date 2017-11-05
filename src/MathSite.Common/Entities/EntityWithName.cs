using System;

namespace MathSite.Common.Entities
{
    public interface IEntityWithName : IEntityWithName<Guid> { }

    public interface IEntityWithName<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        string Name { get; set; }
    }

    [Serializable]
    public class EntityWithName<TPrimaryKey> : Entity<TPrimaryKey>, IEntityWithName<TPrimaryKey>
    {
        public string Name { get; set; }
    }

    [Serializable]
    public class EntityWithName : EntityWithName<Guid>, IEntityWithName
    {
        public override string ToString()
        {
            return $"{base.ToString()} | [{nameof(Name)}: {Name}]";
        }
    }
}