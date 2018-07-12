using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MathSite.Common.Extensions;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IMessagesRepository : IMathSiteEfCoreRepository<Message>
    {
        IMessagesRepository WithAuthor();

        IMessagesRepository WithMessageUserConversations();

        Task<List<Message>> GetAllListOrderedByWithPagingAsync<TKey>(Expression<Func<Message, bool>> predicate, Expression<Func<Message, TKey>> keySelector, bool isAscending,
            int skip, int count);

        Task<List<Message>> GetAllListOrderedByAsync<TKey>(Expression<Func<Message, bool>> predicate, Expression<Func<Message, TKey>> keySelector, bool isAscending);
    }
    public class MessagesRepository : MathSiteEfCoreRepositoryBase<Message>, IMessagesRepository
    {
        public MessagesRepository(MathSiteDbContext dbContext) : base(dbContext)
        {

        }
        public IMessagesRepository WithAuthor()
        {
            SetCurrentQuery(GetCurrentQuery().Include(message => message.Author).ThenInclude(author=>author.Person));
            return this;
        }

        public IMessagesRepository WithMessageUserConversations()
        {
            SetCurrentQuery(GetCurrentQuery().Include(message => message.MessageUserConversations));
            return this;
        }

        public async Task<List<Message>> GetAllListOrderedByWithPagingAsync<TKey>(
            Expression<Func<Message, bool>> predicate, Expression<Func<Message, TKey>> keySelector, bool isAscending,
            int skip,
            int count)
        {
            return await GetAll()
                .Where(predicate)
                .OrderBy(keySelector, isAscending)
                .PageBy(skip,count).ToListAsync();
        
    }

        public async Task<List<Message>> GetAllListOrderedByAsync<TKey>(Expression<Func<Message, bool>> predicate, Expression<Func<Message, TKey>> keySelector, bool isAscending)
        {
            return await GetAll()
                .Where(predicate)
                .OrderBy(keySelector, isAscending)
                .ToListAsync();
        }
    }
}
