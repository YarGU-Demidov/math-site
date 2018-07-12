using System;
using System.Collections.Generic;
using System.Text;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IMessageUserConversationsRepository : IMathSiteEfCoreRepository<MessageUserConversation>
    {
        IMessageUserConversationsRepository WithUserConversation();
        IMessageUserConversationsRepository WithMessage();
    }
    public class MessageUserConversationsRepository : MathSiteEfCoreRepositoryBase<MessageUserConversation>, IMessageUserConversationsRepository
    {
        public MessageUserConversationsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public IMessageUserConversationsRepository WithUserConversation()
        {
            SetCurrentQuery(GetCurrentQuery().Include(messageUserConversation => messageUserConversation.UserConversation));
            return this;
        }

        public IMessageUserConversationsRepository WithMessage()
        {
            SetCurrentQuery(GetCurrentQuery().Include(messageUserConversation => messageUserConversation.Message));
            return this;
        }
    }
}
