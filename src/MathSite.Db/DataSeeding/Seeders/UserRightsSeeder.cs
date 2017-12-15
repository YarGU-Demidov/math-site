using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
    public class UserRightsSeeder : AbstractSeeder<UsersRight>
    {
        /// <inheritdoc />
        public UserRightsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
        {
        }

        /// <inheritdoc />
        public override string SeedingObjectName { get; } = nameof(UsersRight);

        /// <inheritdoc />
        protected override void SeedData()
        {
            var firstUser = GetUserByLogin(UsersAliases.Mokeev1995);
            var secondUser = GetUserByLogin(UsersAliases.AndreyDevyatkin);

            var adminAccess = GetRightByAlias(RightAliases.AdminAccess);
            var logoutAccess = GetRightByAlias(RightAliases.LogoutAccess);
            var panelAccess = GetRightByAlias(RightAliases.PanelAccess);

            var firstUserRights = new[]
            {
                CreateRights(true, firstUser, adminAccess),
                CreateRights(true, firstUser, logoutAccess),
                CreateRights(true, firstUser, panelAccess)
            };

            var secondUserRights = new[]
            {
                CreateRights(false, secondUser, adminAccess),
                CreateRights(true, secondUser, logoutAccess),
                CreateRights(true, secondUser, panelAccess)
            };

            Context.UsersRights.AddRange(secondUserRights);
            Dispose();

            Context.UsersRights.AddRange(firstUserRights);
            Dispose();
        }

        private Right GetRightByAlias(string alias)
        {
            return Context.Rights.First(right => right.Alias == alias);
        }

        private User GetUserByLogin(string login)
        {
            return Context.Users.First(user => user.Login == login);
        }

        private static UsersRight CreateRights(bool allowed, User user, Right right)
        {
            return new UsersRight
            {
                Allowed = allowed,
                User = user,
                Right = right
            };
        }
    }
}