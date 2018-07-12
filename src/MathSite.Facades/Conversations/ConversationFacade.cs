using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Entities;
using MathSite.Common.Extensions;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Facades.UserConversations;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;
using MathSite.Specifications.Conversation;

namespace MathSite.Facades.Conversations
{
    public interface IConversationsFacade : IFacade
    {
        Task<Guid> CreateConversationAndGetIdAsync(Guid userId, string conversationName, string type);

        Task RemoveConversationAsync(Guid conversationId);

        Task AddUserToConversationAsync(Guid userId, Guid conversationId);

        Task<List<Conversation>> GetConversationsByUserIdAsync(Guid userId);

        Task<Conversation> GetConversationByMessageIdAsync(Guid messageId);

        Task<Conversation> GetConversationAsync(Guid conversationId);

        Task RemoveMemberAsync(Guid conversationId, Guid userId);

        Task<bool> IsConversationsCreatorAsync(Guid userId, Guid conversationId);

        Task<bool> DoesConversationContainsMemberAsync(Guid userId, Guid conversationId);

        Task<bool> DoesConversationExistsAsync(Guid conversationId);

        Task<bool> DoesPrivateConversationExistsAsync(Guid firstMember, Guid secondMember);

        Task<string> GetConversationsTypeAsync(Guid conversationId);

    }
    public class ConversationsFacade : BaseMathFacade<IConversationsRepository, Conversation>, IConversationsFacade
    {
        private readonly IUserConversationsFacade _userConversationFacade;
        public ConversationsFacade(IRepositoryManager repositoryManager, IUserConversationsFacade userConversationFacade) : base(repositoryManager)
        {
            _userConversationFacade = userConversationFacade;
        }

        public async Task<Guid> CreateConversationAndGetIdAsync(Guid userId, string conversationName, string type)
        {
            var conversation = new Conversation(userId, conversationName, type);
            var conversationId = await Repository.InsertAndGetIdAsync(conversation);
            await _userConversationFacade.CreateUserConversationAsync(userId, conversationId);
            return conversationId;
        }

        public async Task RemoveConversationAsync(Guid conversationId)
        {
            await Repository.DeleteAsync(conversationId);
        }

        public async Task AddUserToConversationAsync(Guid userId, Guid conversationId)
        {
            await _userConversationFacade.CreateUserConversationAsync(userId, conversationId);
        }
        public async Task<List<Conversation>> GetConversationsByUserIdAsync(Guid userId)
        {
            return await Repository.WithUserConversations().WithMessages().GetAllListOrderedByAsync(
                    predicate: new ConversationHasUserIdSpecification(userId),
                    keySelector: conversation => conversation.Messages.IsNotNullOrEmpty()  ?
                        conversation.Messages.Max(message=>message.CreationDate) : conversation.UserConversations.First(uc => uc.UserId == userId).CreationDate,
                    isAscending: false
            );
        }
        public async Task<Conversation> GetConversationByMessageIdAsync(Guid messageId)
        {
            return await Repository.FirstOrDefaultAsync(new ConversationHasMessageIdSpecification(messageId));
        }

        public async Task<Conversation> GetConversationAsync(Guid conversationId)
        {
            return await Repository.WithUserConversations().FirstOrDefaultAsync(conversationId);
        }

        public async Task RemoveMemberAsync(Guid conversationId, Guid userId)
        {
            await _userConversationFacade.RemoveUserConversation(conversationId, userId);
        }

        public async Task<bool> IsConversationsCreatorAsync(Guid userId, Guid conversationId)
        {
            var conversation = await GetConversationAsync(conversationId);
            return conversation.CreatorId == userId;
        }

        public async Task<bool> DoesConversationContainsMemberAsync(string login, Guid conversationId)
        {          
            return await _userConversationFacade.DoesUserConversationExists(login, conversationId);
        }

        public async Task<bool> DoesConversationContainsMemberAsync(Guid userId, Guid conversationId)
        {
            return await Repository.WithUserConversations().FirstOrDefaultAsync(new ConversationHasConversationIdSpecification(conversationId)
                       .And(new ConversationHasUserIdSpecification(userId))) != null;
        }

        public async Task<bool> DoesConversationExistsAsync(Guid conversationId)
        {
            return await Repository.FirstOrDefaultAsync(conversationId) != null;
        }

        public async Task<bool> DoesPrivateConversationExistsAsync(Guid firstMember, Guid secondMember)
        {
            return await Repository.FirstOrDefaultAsync(conversation =>
                       conversation.Type == "Private" &&
                       conversation.UserConversations.Any(uc => uc.UserId == firstMember) &&
                       conversation.UserConversations.Any(uc => uc.UserId == secondMember)) != null;
        }

        public async Task<string> GetConversationsTypeAsync(Guid conversationId)
        {
            var conversation = await GetConversationAsync(conversationId);
            return conversation.Type;
        }
    }
}
