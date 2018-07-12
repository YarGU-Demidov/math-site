using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;
using MathSite.Specifications.MessageUserConversationSpecification;

namespace MathSite.Facades.MessageUserConversationsFacade
{
    public interface IMessageUserConversationsFacade : IFacade
    {
        Task<int> UnreadMessagesCount(Guid userId, Guid conversationId);
        Task<Guid> CreateMessageUserConversationAsync(Guid messageId, Guid userCoversationId);
        Task SetMessageRead(Guid userId, Guid messageId);
        Task SetAllMessageRead(Guid userId, Guid conversationId);
    }
    public class MessageUserConversationsFacade : BaseMathFacade<IMessageUserConversationsRepository, MessageUserConversation>, IMessageUserConversationsFacade
    {
        public MessageUserConversationsFacade(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
        }


        public async Task<int> UnreadMessagesCount(Guid userId, Guid conversationId)
        {
            return await Repository.WithUserConversation().CountAsync(new MessageUserConversationHasUserIdSpecification(userId)
                .And(new MessageUserConversationHasConversationIdSpecification(conversationId))
                .And(new MessageUserConversationHasMessageStatusSpecification(false)));
        }

        public async Task<Guid> CreateMessageUserConversationAsync(Guid messageId, Guid userConversationId)
        {
            var messageUser = new MessageUserConversation(messageId, userConversationId);

            return await Repository.InsertAndGetIdAsync(messageUser);
        }

        private async Task<MessageUserConversation> GetMessageUserConversationByUserIdAndMessageId(Guid userId, Guid messageId)
        {
            return await Repository.WithMessage().WithUserConversation().FirstOrDefaultAsync(new MessageUserConversationHasUserIdSpecification(userId)
                .And(new MessageUserConversationHasMessageIdSpecification(messageId)));
        }

        public async Task SetMessageRead(Guid userId, Guid messageId)
        {
            var messageUserConversation = await GetMessageUserConversationByUserIdAndMessageId(userId, messageId);
            messageUserConversation.IsRead = true;
            await Repository.UpdateAsync(messageUserConversation);
        }

        public async Task SetAllMessageRead(Guid userId, Guid conversationId)
        {
            var messageUserConversations = await GetUnreadMessageUserConversationsFromConversation(userId, conversationId);
            foreach (var messageUserConversation in messageUserConversations)
            {
                messageUserConversation.IsRead = true;
                await Repository.UpdateAsync(messageUserConversation);
            }
        }

        private async Task<IEnumerable<MessageUserConversation>> GetUnreadMessageUserConversationsFromConversation(Guid userId, Guid conversationId)
        {
            return await Repository.WithUserConversation().GetAllListAsync(new MessageUserConversationHasUserIdSpecification(userId)
            .And(new MessageUserConversationHasConversationIdSpecification(conversationId)).And(new MessageUserConversationHasMessageStatusSpecification(false)));
        }
    }
}
