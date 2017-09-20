using System;
using System.Linq.Expressions;

namespace MathSite.Common.Specs.Expressions
{
    /// <summary>
    ///     Represents the base class for specifications.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public abstract class ExpressionSpecification<T> : IExpressionSpecification<T>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Returns a <see cref="T:System.Boolean" /> value which indicates whether the specification
        ///     is satisfied by the given object.
        /// </summary>
        /// <param name="obj">The object to which the specification is applied.</param>
        /// <returns>True if the specification is satisfied, otherwise false.</returns>
        public virtual bool IsSatisfiedBy(T obj)
        {
            return ToExpression().Compile()(obj);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        public abstract Expression<Func<T, bool>> ToExpression();

        /// <summary>
        ///     Implicitly converts a specification to expression.
        /// </summary>
        /// <param name="expressionSpecification"></param>
        public static implicit operator Expression<Func<T, bool>>(ExpressionSpecification<T> expressionSpecification)
        {
            return expressionSpecification.ToExpression();
        }
    }
}