using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;
using MathSite.Common.Extensions;


namespace MathSite.Repository
{
    public interface IConversationsRepository : IMathSiteEfCoreRepository<Conversation>
    {
        IConversationsRepository WithUserConversations();
        IConversationsRepository WithMessages();
        IConversationsRepository WithCreator();
        IConversationsRepository WithMembers();

        Task<List<Conversation>> GetAllListOrderedByAsync<TKey>(Expression<Func<Conversation, bool>> predicate,
            Func<Conversation, TKey> keySelector, bool isAscending);    
    }
    public class ConversationsRepository : MathSiteEfCoreRepositoryBase<Conversation>, IConversationsRepository
    {
        public ConversationsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public IConversationsRepository WithUserConversations()
        {
            SetCurrentQuery(GetCurrentQuery().Include(conversation => conversation.UserConversations));
            return this;
        }

        public IConversationsRepository WithMessages()
        {
            SetCurrentQuery(GetCurrentQuery().Include(conversation => conversation.Messages));
            return this;
        }

        public IConversationsRepository WithCreator()
        {
            SetCurrentQuery(GetCurrentQuery().Include(conversation => conversation.Creator));
            return this;
        }

        public IConversationsRepository WithMembers()
        {
            var query = GetCurrentQuery()
                .Include(c => c.UserConversations).ThenInclude(uc => uc.User);
            SetCurrentQuery(query);

            return this;
        }

        public async Task<List<Conversation>> GetAllListOrderedByAsync<TKey>(Expression<Func<Conversation, bool>> predicate, Func<Conversation, TKey> keySelector, bool isAscending)
        {
            var conversations = await GetAll()
                .Where(predicate)
                .ToListAsync();

            return conversations
                .OrderBy(keySelector, isAscending)
                .ToList();
        }
    }
}
