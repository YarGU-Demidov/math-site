namespace MathSite.Common.Specs
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the combined specification which indicates that the first specification
    ///     can be satisifed by the given object whereas the second one cannot.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public class AndNotSpecification<T> : CompositeSpecification<T>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Constructs a new instance of <see cref="T:MathSite.Common.Specs.AndNotSpecification`1" /> class.
        /// </summary>
        /// <param name="left">The first specification.</param>
        /// <param name="right">The second specification.</param>
        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right)
        {
        }

        public override bool IsSatisfiedBy(T obj)
        {
            return Left.IsSatisfiedBy(obj) && !Right.IsSatisfiedBy(obj);
        }
    }
}