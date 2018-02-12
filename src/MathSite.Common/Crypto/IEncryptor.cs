using System.Threading.Tasks;

namespace MathSite.Common.Crypto
{
    public interface IEncryptor
    {
        Task<byte[]> EncryptStringToBytes(string message);
        Task<string> DecryptStringFromBytes(byte[] encryptedMessage);

    }
}