using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Facades.MessageUserConversationsFacade;
using MathSite.Facades.UserConversations;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;
using MathSite.Specifications.Messages;

namespace MathSite.Facades.Messages
{
    public interface IMessagesFacade : IFacade
    {
        Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId);

        Task<Message> GetMessageAsync(Guid messageId);

        Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId,
            Guid firstMessageId, int count);

        Task CreateMessageAsync(Guid userId, Guid conversationId, string body);

        Task<Guid> CreateMessageAndGetIdAsync(Guid userId, Guid conversationId, string body);

        Task<Tuple<Guid, DateTime>> CreateMessageAndGetIdWithCreationDateAsync(Guid userId, Guid conversationId,
            string body);

        Task<int> UnreadMessagesCount(Guid userId, Guid conversationId);

        Task SetMessageRead(Guid userId, Guid messageId);

        Task SetAllMessagesRead(Guid userId, Guid conversationId);

    }
    public class MessagesFacade : BaseMathFacade<IMessagesRepository, Message>, IMessagesFacade
    {
        private readonly IMessageUserConversationsFacade _messageUserConversationsFacade;
        private readonly IUserConversationsFacade _userConversationsFacade;

        public MessagesFacade(IRepositoryManager repositoryManager, IMessageUserConversationsFacade userConversationFacade,
            IUserConversationsFacade userFacade) : base(repositoryManager)
        {
            _messageUserConversationsFacade = userConversationFacade;
            _userConversationsFacade = userFacade;
        }

        public async Task<Message> GetMessageAsync(Guid messageId)
        {
            return await Repository.WithMessageUserConversations().FirstOrDefaultAsync(messageId);
        }

        public async Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId,
            Guid firstMessageId, int count)
        {
            if (firstMessageId == Guid.Empty)
            {
                return await Repository.WithAuthor().GetAllListOrderedByWithPagingAsync(
                    predicate: new MessageHasConversationIdSpecification(conversationId),
                    keySelector: message => message.CreationDate,
                    isAscending: false,
                    skip: 0,
                    count: count
                );
            }

            var firstMessage = await GetMessageAsync(firstMessageId);
            return await Repository.WithAuthor().GetAllListOrderedByWithPagingAsync(
                predicate: new MessageHasConversationIdSpecification(conversationId)
                    .And(new MessageHasCreatedBeforeSpecification(firstMessage.CreationDate)),
                keySelector: message => message.CreationDate,
                isAscending: false,
                skip: 0,
                count: count
            );
        }

        public async Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(Guid conversationId)
        {
            return await Repository.WithAuthor().GetAllListOrderedByAsync(
                predicate: new MessageHasConversationIdSpecification(conversationId),
                keySelector: msg => msg.CreationDate,
                isAscending: false);
        }

        public async Task CreateMessageAsync(Guid userId, Guid conversationId, string body)
        {
            var userConversations = await _userConversationsFacade.GetUserConversationsByConversationIdAsync(conversationId);
            var message = new Message(userId, conversationId, body);
            var messgeId = await Repository.InsertAndGetIdAsync(message);
            foreach (var userConversation in userConversations)
            {
                await _messageUserConversationsFacade.CreateMessageUserConversationAsync(messgeId, userConversation.Id);
            }

        }

        public async Task<Guid> CreateMessageAndGetIdAsync(Guid userId, Guid conversationId, string body)
        {
            var userConversations = await _userConversationsFacade.GetUserConversationsByConversationIdAsync(conversationId);
            var message = new Message(userId, conversationId, body);
            var messgeId = await Repository.InsertAndGetIdAsync(message);
            foreach (var member in userConversations)
            {
                await _messageUserConversationsFacade.CreateMessageUserConversationAsync(messgeId, member.Id);
            }

            return messgeId;
        }

        public async Task<Tuple<Guid,DateTime>> CreateMessageAndGetIdWithCreationDateAsync(Guid userId, Guid conversationId, string body)
        {
            var userConversations = await _userConversationsFacade.GetUserConversationsByConversationIdAsync(conversationId);
            var message = new Message(userId, conversationId, body);
            var messgeId = await Repository.InsertAndGetIdAsync(message);
            foreach (var member in userConversations)
            {
                await _messageUserConversationsFacade.CreateMessageUserConversationAsync(messgeId, member.Id);
            }

            return new Tuple<Guid, DateTime>(messgeId, message.CreationDate);
        }

        public async Task<int> UnreadMessagesCount(Guid userId, Guid conversationId)
        {
            return await _messageUserConversationsFacade.UnreadMessagesCount(userId, conversationId);
        }

        public async Task SetMessageRead(Guid userId, Guid messageId)
        {
            await _messageUserConversationsFacade.SetMessageRead(userId, messageId);
        }

        public async Task SetAllMessagesRead(Guid userId, Guid conversationId)
        {
            await _messageUserConversationsFacade.SetAllMessageRead(userId, conversationId);
        }
    }
}
