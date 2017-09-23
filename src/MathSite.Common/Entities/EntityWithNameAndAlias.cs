using System;

namespace MathSite.Common.Entities
{
    public interface IEntityWithNameAndAlias : IEntityWithNameAndAlias<Guid> { }

    public interface IEntityWithNameAndAlias<TPrimaryKey> : IEntityWithName<TPrimaryKey>, IEntityWithAlias<TPrimaryKey> { }

    [Serializable]
    public class EntityWithNameAndAlias : EntityWithNameAndAlias<Guid>, IEntityWithNameAndAlias { }

    [Serializable]
    public class EntityWithNameAndAlias<TPrimaryKey> : Entity<TPrimaryKey>, IEntityWithNameAndAlias<TPrimaryKey>
    {
        public string Name { get; set; }
        public string Alias { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} | [{nameof(Name)}: {Name}, {nameof(Alias)}: {Alias}]";
        }
    }
}