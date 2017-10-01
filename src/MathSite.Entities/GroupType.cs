using System.Collections.Generic;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    /// <summary>
    ///     Тип группы.
    ///     Например: пользовательская, студенчаская, сотрудников вуза.
    /// </summary>
    public class GroupType : EntityWithNameAndAlias
    {
        /// <summary>
        ///     Описание типа группы.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        ///     Список групп этого типа.
        /// </summary>
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}