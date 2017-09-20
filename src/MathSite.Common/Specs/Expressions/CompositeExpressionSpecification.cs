namespace MathSite.Common.Specs.Expressions
{
    /// <summary>
    ///     Represents the base class for composite specifications.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public abstract class CompositeExpressionSpecification<T> : ExpressionSpecification<T>, ICompositeExpressionSpecification<T>
    {
        /// <summary>
        ///     Constructs a new instance of <see cref="CompositeSpecification{T}" /> class.
        /// </summary>
        /// <param name="left">The first specification.</param>
        /// <param name="right">The second specification.</param>
        protected CompositeExpressionSpecification(IExpressionSpecification<T> left, IExpressionSpecification<T> right)
        {
            Left = left;
            Right = right;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the first specification.
        /// </summary>
        public IExpressionSpecification<T> Left { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the second specification.
        /// </summary>
        public IExpressionSpecification<T> Right { get; }
    }
}