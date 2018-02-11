namespace MathSite.Common.Crypto
{
    public interface IKeyVectorReader
    {
        KeyVectorPair GetKeyVector();
    }
}