using System;

namespace MathSite.ViewModels.Api.UsersInfo
{
    public class GroupInfo
    {
        public GroupInfo(Guid id, string alias, string name, string description)
        {
            Id = id;
            Alias = alias;
            Name = name;
            Description = description;
        }

        public Guid Id { get; }
        public string Alias { get; }
        public string Name { get; }
        public string Description { get; }
    }
}