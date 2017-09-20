namespace MathSite.Common.Specs
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the specification which indicates the semantics opposite to the given specification.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public sealed class NotSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _specification;

        /// <summary>
        ///     Initializes a new instance of <see cref="NotSpecification{T}" /> class.
        /// </summary>
        /// <param name="specification">The specification to be reversed.</param>
        public NotSpecification(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public bool IsSatisfiedBy(T obj)
        {
            return !_specification.IsSatisfiedBy(obj);
        }
    }
}