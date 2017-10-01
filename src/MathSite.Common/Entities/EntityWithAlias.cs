using System;

namespace MathSite.Common.Entities
{
    public interface IEntityWithAlias : IEntityWithAlias<Guid> { }

    public interface IEntityWithAlias<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        string Alias { get; set; }
    }

    [Serializable]
    public class EntityWithAlias : EntityWithAlias<Guid>, IEntityWithAlias { }

    [Serializable]
    public class EntityWithAlias<TPrimaryKey> : Entity<TPrimaryKey>, IEntityWithAlias<TPrimaryKey>
    {
        public string Alias { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} | [{nameof(Alias)}: {Alias}]";
        }
    }
}