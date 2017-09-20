using System;
using System.Linq.Expressions;

namespace MathSite.Common.Specs.Expressions
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the combined specification which indicates that both of the given
    ///     specifications should be satisfied by the given object.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public class AndExpressionSpecification<T> : CompositeExpressionSpecification<T>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Constructs a new instance of <see cref="T:MathSite.Common.Specs.Expressions.AndExpressionSpecification`1" /> class.
        /// </summary>
        /// <param name="left">The first specification.</param>
        /// <param name="right">The second specification.</param>
        public AndExpressionSpecification(IExpressionSpecification<T> left, IExpressionSpecification<T> right) 
            : base(left, right)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        public override Expression<Func<T, bool>> ToExpression()
        {
            return Left.ToExpression().And(Right.ToExpression());
        }
    }
}