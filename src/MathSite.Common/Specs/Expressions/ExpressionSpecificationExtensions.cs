namespace MathSite.Common.Specs.Expressions
{
    public static class ExpressionSpecificationExtensions
    {
        /// <summary>
        ///     Combines the current specification instance with another specification instance
        ///     and returns the combined specification which represents that both the current and
        ///     the given specification must be satisfied by the given object.
        /// </summary>
        /// <param name="specification">The specification</param>
        /// <param name="other">The specification instance with which the current specification is combined.</param>
        /// <returns>The combined specification instance.</returns>
        public static IExpressionSpecification<T> And<T>(this IExpressionSpecification<T> specification, IExpressionSpecification<T> other)
        {
            return new AndExpressionSpecification<T>(specification, other);
        }

        /// <summary>
        ///     Combines the current specification instance with another specification instance
        ///     and returns the combined specification which represents that either the current or
        ///     the given specification should be satisfied by the given object.
        /// </summary>
        /// <param name="specification">The specification</param>
        /// <param name="other">
        ///     The specification instance with which the current specification
        ///     is combined.
        /// </param>
        /// <returns>The combined specification instance.</returns>
        public static IExpressionSpecification<T> Or<T>(this IExpressionSpecification<T> specification, IExpressionSpecification<T> other)
        {
            return new OrExpressionSpecification<T>(specification, other);
        }

        /// <summary>
        ///     Combines the current specification instance with another specification instance
        ///     and returns the combined specification which represents that the current specification
        ///     should be satisfied by the given object but the specified specification should not.
        /// </summary>
        /// <param name="specification">The specification</param>
        /// <param name="other">
        ///     The specification instance with which the current specification
        ///     is combined.
        /// </param>
        /// <returns>The combined specification instance.</returns>
        public static IExpressionSpecification<T> AndNot<T>(this IExpressionSpecification<T> specification, IExpressionSpecification<T> other)
        {
            return new AndNotExpressionSpecification<T>(specification, other);
        }

        /// <summary>
        ///     Reverses the current specification instance and returns a specification which represents
        ///     the semantics opposite to the current specification.
        /// </summary>
        /// <returns>The reversed specification instance.</returns>
        public static IExpressionSpecification<T> Not<T>(this IExpressionSpecification<T> specification)
        {
            return new NotExpressionSpecification<T>(specification);
        }
    }
}