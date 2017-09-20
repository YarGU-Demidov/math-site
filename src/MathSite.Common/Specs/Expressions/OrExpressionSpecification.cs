using System;
using System.Linq.Expressions;

namespace MathSite.Common.Specs.Expressions
{
    /// <summary>
    ///     Represents the combined specification which indicates that either of the given
    ///     specification should be satisfied by the given object.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public class OrExpressionSpecification<T> : CompositeExpressionSpecification<T>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="OrSpecification{T}" /> class.
        /// </summary>
        /// <param name="left">The first specification.</param>
        /// <param name="right">The second specification.</param>
        public OrExpressionSpecification(IExpressionSpecification<T> left, IExpressionSpecification<T> right) : base(left, right)
        {
        }

        /// <summary>
        ///     Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        public override Expression<Func<T, bool>> ToExpression()
        {
            return Left.ToExpression().Or(Right.ToExpression());
        }
    }
}