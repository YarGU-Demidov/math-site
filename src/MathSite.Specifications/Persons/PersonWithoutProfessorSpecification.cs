using System;
using System.Linq.Expressions;
using MathSite.Common.Specifications;
using MathSite.Entities;

namespace MathSite.Specifications.Persons
{
    public class PersonWithoutProfessorSpecification : Specification<Person>
    {
        public override Expression<Func<Person, bool>> ToExpression()
        {
            return person => person.Professor == null;
        }
    }
}