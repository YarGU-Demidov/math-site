namespace MathSite.Common.Specs.Expressions
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents that the implemented classes are composite specifications.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public interface ICompositeExpressionSpecification<T> : ISpecification<T>
    {
        /// <summary>
        ///     Gets the left side of the specification.
        /// </summary>
        IExpressionSpecification<T> Left { get; }

        /// <summary>
        ///     Gets the right side of the specification.
        /// </summary>
        IExpressionSpecification<T> Right { get; }
    }
}