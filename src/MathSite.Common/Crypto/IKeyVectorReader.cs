using System.Threading.Tasks;

namespace MathSite.Common.Crypto
{
    public interface IKeyVectorReader
    {
        KeyVectorPair GetKeyVector();
    }
}