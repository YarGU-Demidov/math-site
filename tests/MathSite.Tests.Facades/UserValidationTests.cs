using System;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db.DataSeeding;
using MathSite.Db.DataSeeding.Seeders;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.UserValidation;
using MathSite.Repository.Core;
using MathSite.Specifications;
using MathSite.Specifications.Users;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class UserValidationTests : FacadesTestsBase
    {
        public IUserValidationFacade GetFacade(IRepositoryManager manager)
        {
            return new UserValidationFacade(manager, MemoryCache, new DoubleSha512HashPasswordsManager());
        }

        [Fact]
        public async Task CheckRights_Fail()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                SeedData(new ISeeder[]
                {
                    new GroupTypeSeeder(logger, context),
                    new GroupSeeder(logger, context),
                    new PersonSeeder(logger, context),
                    new UserSeeder(logger, context, new DoubleSha512HashPasswordsManager()),
                    new RightSeeder(logger, context),
                    new GroupRightsSeeder(logger, context),
                    new UsersToGroupsSeeder(logger, context),
                    new UserRightsSeeder(logger, context),
                });

                var rightsValidator = GetFacade(manager);

                var user = await GetUserByLogin(manager, UsersAliases.SecondUser);

                var requirements = new SameAliasSpecification<Right>(RightAliases.AdminAccess);

                var groupRight = await manager.RightsRepository.FirstOrDefaultAsync(requirements.ToExpression());

                var hasRight = await rightsValidator.UserHasRightAsync(user.Id, groupRight);
                Assert.False(hasRight);

                hasRight = await rightsValidator.UserHasRightAsync(user.Id, groupRight.Alias);
                Assert.False(hasRight);

                hasRight = await rightsValidator.UserHasRightAsync(user, groupRight);
                Assert.False(hasRight);

                hasRight = await rightsValidator.UserHasRightAsync(user, groupRight.Alias);
                Assert.False(hasRight);
            });
        }

        [Fact]
        public async Task CheckRights_Success()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                SeedData(new ISeeder[]
                {
                    new GroupTypeSeeder(logger, context),
                    new GroupSeeder(logger, context),
                    new PersonSeeder(logger, context),
                    new UserSeeder(logger, context, new DoubleSha512HashPasswordsManager()),
                    new RightSeeder(logger, context),
                    new GroupRightsSeeder(logger, context),
                    new UsersToGroupsSeeder(logger, context),
                    new UserRightsSeeder(logger, context),
                });

                var rightsValidator = GetFacade(manager);

                var user = await GetUserByLogin(manager, UsersAliases.FirstUser);

                var requirements = new SameAliasSpecification<Right>(RightAliases.AdminAccess);

                var groupRight = await manager.RightsRepository.FirstOrDefaultAsync(requirements.ToExpression());

                var hasRight = await rightsValidator.UserHasRightAsync(user.Id, groupRight);
                Assert.True(hasRight);

                hasRight = await rightsValidator.UserHasRightAsync(user.Id, groupRight.Alias);
                Assert.True(hasRight);

                hasRight = await rightsValidator.UserHasRightAsync(user, groupRight);
                Assert.True(hasRight);

                hasRight = await rightsValidator.UserHasRightAsync(user, groupRight.Alias);
                Assert.True(hasRight);
            });
        }

        [Fact]
        public async Task UserDoesNotExistsTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                SeedData(new ISeeder[]
                {
                    new GroupTypeSeeder(logger, context),
                    new GroupSeeder(logger, context),
                    new PersonSeeder(logger, context),
                    new UserSeeder(logger, context, new DoubleSha512HashPasswordsManager()),
                    new RightSeeder(logger, context),
                    new GroupRightsSeeder(logger, context),
                    new UsersToGroupsSeeder(logger, context),
                    new UserRightsSeeder(logger, context),
                });

                Guid? userId = null;
                do
                {
                    var tempId = Guid.NewGuid();
                    var tempUser = await manager.UsersRepository.FirstOrDefaultWithRightsAsync(tempId);

                    if (tempUser == null)
                        userId = tempId;
                } while (userId == null);

                var rightsValidator = GetFacade(manager);

                var exists = await rightsValidator.DoesUserExistsAsync(userId.Value);

                Assert.False(exists);
            });
        }

        [Fact]
        public async Task UserExistsTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                SeedData(new ISeeder[]
                {
                    new GroupTypeSeeder(logger, context),
                    new GroupSeeder(logger, context),
                    new PersonSeeder(logger, context),
                    new UserSeeder(logger, context, new DoubleSha512HashPasswordsManager()),
                    new RightSeeder(logger, context),
                    new GroupRightsSeeder(logger, context),
                    new UsersToGroupsSeeder(logger, context),
                    new UserRightsSeeder(logger, context),
                });

                var user = await GetUserByLogin(manager, UsersAliases.FirstUser);
                var rightsValidator = GetFacade(manager);

                var exists = await rightsValidator.DoesUserExistsAsync(user.Id);

                Assert.True(exists);
            });
        }

        private async Task<User> GetUserByLogin(IRepositoryManager manager, string login)
        {
            var requirements = new HasLoginSpecification(login);

            return await manager.UsersRepository.FirstOrDefaultWithRightsAsync(requirements.ToExpression());
        }
    }
}