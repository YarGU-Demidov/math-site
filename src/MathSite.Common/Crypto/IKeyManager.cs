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
        /// Проверяет правилен ли введенный пользователем 6-ти циферный код.
        /// </summary>
        /// <param name="keyForVerification">Введенный пользователем код</param>
        /// <param name="encryptedUserUniqueKey">Хранящийся в базе данный зашифрованный ключ</param>
        bool KeysAreEqual(string keyForVerification, byte[] encryptedUserUniqueKey);
    }
}