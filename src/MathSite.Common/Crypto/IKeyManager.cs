using System.Threading.Tasks;

namespace MathSite.Common.Crypto
{
    public interface IKeyManager
    {
        /// <summary>
        /// Создает случайную 12-ти численную комбинацию символов
        /// и возвращает зашифрованный массив байт этой комбинации.
        /// </summary>
        Task<byte[]> CreateEncryptedKey();

        /// <summary>
        /// Расшифровывает массив байт из таблицы бд юзера.
        /// </summary>
        Task<string> GetDecryptedString(byte[] encryptedBytes);
    }
}