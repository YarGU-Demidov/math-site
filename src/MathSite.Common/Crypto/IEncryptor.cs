using System.Threading.Tasks;

namespace MathSite.Common.Crypto
{
    public interface IEncryptor
    {
        Task<byte[]> EncryptStringToBytes(string message);
        string DecryptStringFromBytes(byte[] encryptedMessage);

    }
}