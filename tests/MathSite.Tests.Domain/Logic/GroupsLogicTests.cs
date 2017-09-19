using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Logic.Groups;
using MathSite.Entities;
using Xunit;

namespace MathSite.Tests.Domain.Logic
{
    public class GroupsLogicTests : DomainTestsBase
    {
        private void TryGet_WithUsers_Assertions(Group group)
        {
            Assert.NotNull(group);
            Assert.NotNull(group.Users);
            Assert.NotEmpty(group.Users);
        }

        private void TryGet_WithUsers_WithRights_Assertions(Group group)
        {
            TryGet_WithUsers_Assertions(group);

            var user = group.Users.First();
            var rights = user.UserRights;
            var groupRights = group.GroupsRights;

            Assert.NotNull(rights);
            Assert.NotNull(groupRights);

            Assert.NotEmpty(rights);
            Assert.NotEmpty(groupRights);
        }

        private async Task<Guid> CreateGroupAsync(IGroupsLogic logic, string name = null, string description = null,
            string alias = null, Guid? parentGroup = null, bool isAdmin = false)
        {
            var salt = Guid.NewGuid();

            var groupName = name ?? $"test-group-name-{salt}";
            var groupDesc = description ?? $"test-group-description-{salt}";
            var groupAlias = alias ?? $"test-group-alias-{salt}";

            return await logic.CreateAsync(groupName, groupDesc, groupAlias, GroupTypeAliases.User, isAdmin,
                parentGroup);
        }

        [Fact]
        public async Task CreateGroupTest()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var groupsLogic = new GroupsLogic(context);

                const string groupName = "test-group-name";
                const string description = "test-group-description";
                const string alias = "test-group-alias";
                const bool isAdmin = true;

                var testGroupId = await CreateGroupAsync(groupsLogic);

                var id = await CreateGroupAsync(groupsLogic, groupName, description, alias, testGroupId, isAdmin);

                Assert.NotEqual(Guid.Empty, id);

                var group = await groupsLogic.TryGetByIdAsync(id);
                Assert.NotNull(group);

                Assert.Equal(groupName, group.Name);
                Assert.Equal(description, group.Description);
                Assert.Equal(alias, group.Alias);
                Assert.Equal(testGroupId, group.ParentGroupId);
                Assert.Equal(isAdmin, group.IsAdmin);
            });
        }

        [Fact]
        public async Task DeleteGroupTest()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var groupsLogic = new GroupsLogic(context);

                var id = await CreateGroupAsync(groupsLogic);

                await groupsLogic.DeleteAsync(id);

                var file = await groupsLogic.TryGetByIdAsync(id);

                Assert.Null(file);
            });
        }

        [Fact]
        public async Task TryGet_Found()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var filesLogic = new GroupsLogic(context);

                var id = await CreateGroupAsync(filesLogic);

                var group = await filesLogic.TryGetByIdAsync(id);

                Assert.NotNull(group);
            });
        }

        [Fact]
        public async Task TryGet_NotFound()
        {
            var id = Guid.NewGuid();

            await ExecuteWithContextAsync(async context =>
            {
                var filesLogic = new GroupsLogic(context);
                var group = await filesLogic.TryGetByIdAsync(id);

                Assert.Null(group);
            });
        }

        [Fact]
        public async Task TryGet_WithUsers_ByAlias()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var groupsLogic = new GroupsLogic(context);

                var groupWithUsers = await groupsLogic.TryGetGroupWithUsersByAliasAsync(GroupAliases.Admin);

                TryGet_WithUsers_Assertions(groupWithUsers);
            });
        }

        [Fact]
        public async Task TryGet_WithUsers_ById()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var groupsLogic = new GroupsLogic(context);

                var adminsGroup = await groupsLogic.TryGetByAliasAsync(GroupAliases.Admin);

                var groupWithUsers = await groupsLogic.TryGetGroupWithUsersByIdAsync(adminsGroup.Id);

                TryGet_WithUsers_Assertions(groupWithUsers);
            });
        }

        [Fact]
        public async Task TryGet_WithUsers_WithRights_ByAlias()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var groupsLogic = new GroupsLogic(context);

                var groupWithUsers = await groupsLogic.TryGetGroupWithUsersWithRightsByAliasAsync(GroupAliases.Admin);

                TryGet_WithUsers_WithRights_Assertions(groupWithUsers);
            });
        }

        [Fact]
        public async Task TryGet_WithUsers_WithRights_ById()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var groupsLogic = new GroupsLogic(context);

                var adminsGroup = await groupsLogic.TryGetByAliasAsync(GroupAliases.Admin);

                var groupWithUsers = await groupsLogic.TryGetGroupWithUsersWithRightsByIdAsync(adminsGroup.Id);

                TryGet_WithUsers_WithRights_Assertions(groupWithUsers);
            });
        }

        [Fact]
        public async Task UpdateGroupTest()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var groupsLogic = new GroupsLogic(context);

                const string groupName = "test-group-name-new";
                const string groupDescription = "test-group-description-new";
                const bool isAdmin = false;

                var testGroupId = await CreateGroupAsync(groupsLogic);

                var id = await CreateGroupAsync(groupsLogic, isAdmin: true);

                await groupsLogic.UpdateAsync(id, groupName, groupDescription, GroupTypeAliases.Employee, isAdmin,
                    testGroupId);

                var group = await groupsLogic.TryGetByIdAsync(id);

                Assert.NotNull(group);

                Assert.Equal(groupName, group.Name);
                Assert.Equal(groupDescription, group.Description);
                Assert.Equal(testGroupId, group.ParentGroupId);
                Assert.Equal(isAdmin, group.IsAdmin);
            });
        }
    }
}