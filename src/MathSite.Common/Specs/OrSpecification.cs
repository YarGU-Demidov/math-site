namespace MathSite.Common.Specs
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the combined specification which indicates that either of the given
    ///     specification should be satisfied by the given object.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of <see cref="T:MathSite.Common.Specs.OrSpecification`1" /> class.
        /// </summary>
        /// <param name="left">The first specification.</param>
        /// <param name="right">The second specification.</param>
        public OrSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right)
        {
        }

        public override bool IsSatisfiedBy(T obj)
        {
            return Left.IsSatisfiedBy(obj) || Right.IsSatisfiedBy(obj);
        }
    }
}