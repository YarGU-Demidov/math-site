﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.MessageUserConversationSpecification
{
    public class MessageUserConversationHasConversationIdSpecification : Specification<MessageUserConversation>
    {
        private readonly Guid _conversationId;

        public MessageUserConversationHasConversationIdSpecification(Guid conversationId)
        {
            _conversationId = conversationId;
        }

        public override Expression<Func<MessageUserConversation, bool>> ToExpression()
        {
            return messageUserConversation => messageUserConversation.Message.ConversationId == _conversationId;
        }
    }
}
