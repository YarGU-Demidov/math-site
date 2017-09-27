using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
    /// <inheritdoc />
    public class UsersToGroupsSeeder : AbstractSeeder<User>
    {
        /// <inheritdoc />
        public UsersToGroupsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
        {
        }

        /// <inheritdoc />
        public override string SeedingObjectName { get; } = "UsersToGroups";

        /// <inheritdoc />
        protected override bool ShouldSeed()
        {
            return Context.Groups.Any();
        }

        protected override bool DbContainsEntities()
        {
            return !Context.Set<User>().Any();
        }

        /// <inheritdoc />
        protected override void SeedData()
        {
            var adminGroup = GetGroupByAlias(GroupAliases.Admin);
            var usersGroup = GetGroupByAlias(GroupAliases.User);

            var firstUser = GetUserByLogin(UsersAliases.FirstUser);
            var secondUser = GetUserByLogin(UsersAliases.SecondUser);
            var testUser = GetUserByLogin(UsersAliases.TestUser);

            firstUser.Group = adminGroup;
            secondUser.Group = adminGroup;
            testUser.Group = usersGroup;
        }

        private Group GetGroupByAlias(string alias)
        {
            return Context.Groups.First(group => group.Alias == alias);
        }

        private User GetUserByLogin(string login)
        {
            return Context.Users.First(user => user.Login == login);
        }
    }
}