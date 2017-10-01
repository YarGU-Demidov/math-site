using System.Collections.Generic;
using System.Linq;
using MathSite.Entities;

namespace MathSite.Core
{
    public class UserRightsRetriever
    {
        public IDictionary<string, bool> GetUserRights(User user)
        {
            var userRights = user.UserRights;

            var groupRights =
                user.Group.GroupsRights
                    .Where(
                        gr =>
                            !userRights.Any(usersRights => usersRights.Right.Equals(gr.Right))
                    );

            var rights = groupRights
                .ToDictionary(
                    groupRight => groupRight.Right.Alias,
                    groupRight => groupRight.Allowed
                );

            foreach (var userRight in userRights)
                if (rights.ContainsKey(userRight.Right.Alias))
                    rights[userRight.Right.Alias] = userRight.Allowed;
                else
                    rights.Add(userRight.Right.Alias, userRight.Allowed);

            return rights;
        }
    }
}