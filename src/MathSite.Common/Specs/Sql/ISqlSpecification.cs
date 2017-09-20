namespace MathSite.Common.Specs
{
    public interface ISqlSpecification<T> : ISpecification<T>
    {
        string ToSql();
    }
}