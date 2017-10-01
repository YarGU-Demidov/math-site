using System.Collections.Generic;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    public class Right : EntityWithNameAndAlias
    {
        public string Description { get; set; }

        public List<GroupsRight> GroupsRights { get; set; } = new List<GroupsRight>();
        public List<UsersRight> UsersRights { get; set; } = new List<UsersRight>();
    }
}