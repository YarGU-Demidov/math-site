using System;
using System.Linq.Expressions;

namespace MathSite.Common.Specs.Expressions
{
    public interface IExpressionSpecification<T> : ISpecification<T>
    {
        /// <summary>
        ///     Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        Expression<Func<T, bool>> ToExpression();
    }
}