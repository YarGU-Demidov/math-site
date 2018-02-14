using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.UserValidation;
using MathSite.Repository.Core;
using MathSite.Specifications;
using MathSite.Specifications.Users;
using MathSite.Tests.CoreThings;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class UserValidationTests : FacadesTestsBase
    {
        public IUserValidationFacade GetFacade(IRepositoryManager manager)
        {
            return new UserValidationFacade(manager, MemoryCache, new DoubleSha512HashPasswordsManager(), new TestKeyManager());
        }

        [Fact]
        public async Task CheckRights_Fail()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {

                var rightsValidator = GetFacade(manager);

                var user = await GetUserByLogin(manager, UsersAliases.AndreyDevyatkin);

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
                var rightsValidator = GetFacade(manager);

                var user = await GetUserByLogin(manager, UsersAliases.Mokeev1995);

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

        private async Task<User> GetUserByLogin(IRepositoryManager manager, string login)
        {
            var requirements = new HasLoginSpecification(login);

            return await manager.UsersRepository.FirstOrDefaultWithRightsAsync(requirements.ToExpression());
        }
    }
}