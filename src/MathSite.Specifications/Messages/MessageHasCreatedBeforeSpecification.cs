using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Messages
{
    public class MessageHasCreatedBeforeSpecification : Specification<Message>
    {
        private readonly DateTime _creationDate;

        public MessageHasCreatedBeforeSpecification(DateTime creationDate)
        {
            _creationDate = creationDate;
        }

        public override Expression<Func<Message, bool>> ToExpression()
        {
            return message => message.CreationDate < _creationDate;
        }
    }
}
