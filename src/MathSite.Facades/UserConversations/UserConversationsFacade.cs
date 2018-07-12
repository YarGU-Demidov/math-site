using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;
using MathSite.Specifications.UserConversationSpecification;

namespace MathSite.Facades.UserConversations
{
    public interface IUserConversationsFacade : IFacade
    {
        Task<IEnumerable<UserConversation>> GetUserConversationsByConversationIdAsync(Guid conversationId);
        Task CreateUserConversationAsync(Guid userId, Guid conversationId);
        Task RemoveUserConversation(Guid conversationId, Guid userId);
        Task<bool> DoesUserConversationExists(string login, Guid conversationId);
    }
    public class UserConversationsFacade : BaseMathFacade<IUserConversationsRepository, UserConversation>, IUserConversationsFacade
    {
        public UserConversationsFacade(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
        }


        public async Task<IEnumerable<UserConversation>> GetUserConversationsByConversationIdAsync(Guid conversationId)
        {
            return await Repository.WithUser().GetAllListAsync(new UserConversationHasConversationIdSpecification(conversationId));
        }

        public async Task CreateUserConversationAsync(Guid userId, Guid conversationId)
        {
            var userConversation = new UserConversation(userId, conversationId);

            await Repository.InsertAsync(userConversation);
        }

        public async Task RemoveUserConversation(Guid conversationId, Guid userId)
        {
            await Repository.DeleteAsync(new UserConversationHasConversationIdSpecification(conversationId)
            .And(new UserConversationHasUserIdspecification(userId)));
        }

        public async Task<bool> DoesUserConversationExists(string login, Guid conversationId)
        {
            return await Repository.WithUser().CountAsync(new UserConversationHasConversationIdSpecification(conversationId)
                   .And(new UserConversationHasUserLoginspecification(login))) > 0;
        }
    }
}
