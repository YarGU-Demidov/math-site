using System;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Users;
using MathSite.Repository.Core;
using MathSite.Specifications.Users;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class UserFacadeTests : FacadesTestsBase
    {
        [Fact]
        public async Task UserDoesNotExistsTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {

                Guid? userId = null;
                do
                {
                    var tempId = Guid.NewGuid();
                    var tempUser = await manager.UsersRepository.WithRights().FirstOrDefaultAsync(tempId);

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
                var user = await GetUserByLogin(manager, UsersAliases.Mokeev1995);
                var rightsValidator = GetFacade(manager);

                var exists = await rightsValidator.DoesUserExistsAsync(user.Id);

                Assert.True(exists);
            });
        }

        private IUsersFacade GetFacade(IRepositoryManager manager)
        {
            return new UsersFacade(manager, MemoryCache);
        }

        private async Task<User> GetUserByLogin(IRepositoryManager manager, string login)
        {
            var requirements = new HasLoginSpecification(login);

            return await manager.UsersRepository.WithRights().FirstOrDefaultAsync(requirements.ToExpression());
        }
    }
}