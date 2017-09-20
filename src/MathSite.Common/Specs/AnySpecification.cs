namespace MathSite.Common.Specs
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the specification that can be satisfied by the given object
    ///     in any circumstance.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public sealed class AnySpecification<T> : ISpecification<T>
    {
        public bool IsSatisfiedBy(T obj)
        {
            return true;
        }
    }
}