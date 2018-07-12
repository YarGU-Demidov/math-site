using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IUserConversationsRepository : IMathSiteEfCoreRepository<UserConversation>
    {
        IUserConversationsRepository WithConversation();
        IUserConversationsRepository WithUser();
    }
    public class UserConversationsRepository : MathSiteEfCoreRepositoryBase<UserConversation>, IUserConversationsRepository
    {
        public UserConversationsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public IUserConversationsRepository WithConversation()
        {
            SetCurrentQuery(GetCurrentQuery().Include(userConversation => userConversation.Conversation));
            return this;
        }

        public IUserConversationsRepository WithUser()
        {
            SetCurrentQuery(GetCurrentQuery().Include(userConversation => userConversation.User));
            return this;
        }
    }
}
