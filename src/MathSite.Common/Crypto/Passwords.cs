namespace MathSite.Common.Crypto
{
	public class Passwords : IPasswordsManager
	{
      private byte [] _hash;       // хэш пароля

        internal Password (string login, string password)
        {
            /*Конструктор, принимающий логин и пароль пользователя,
             * создает хеш пароля пользователя с помощью внутренней функции HashPassword
             */
            _hash = HashPassword(login, password);
        }

            /*Конструкторы, принимающие хеш пароля (либо байтовым массивом либо строкой),
             * копируют хеш пароля пользователя во внутреннее поле для дальнейшей процедуры верификации
             */
        internal Password (byte [] hash)
        {
            _hash = (byte[])hash.Clone();
        }

        internal Password(string hash)
        {
            Encoding utf8 = Encoding.UTF8;

            _hash = utf8.GetBytes(hash);
        }

            /* Метод, получающий строковое представление хеша пароля для занесения в БД
             * Используется кодировка UTF-8
             */
        public string CreatePasswordString(string login, string password)
        {
             _hash = HashPassword(login, password);
            Encoding utf8 = Encoding.UTF8;
            return utf8.GetString(_hash);
        }

        private byte[] HashPassword(string login, string password)
        {
            /* Для более эффективной защиты hash строится так:
             * 1) складываем строки salt и password
             * 2) получаем их хеш с помощью SHA256 алгоритма
             * 3) складываем salt с получившимся массивом
             * 4) снова получаем хеш уже нового массива с помощью sha256
             */


            byte[] hash;

            if (password != null && login != null)
            {
                Encoding utf8 = Encoding.UTF8;
                SHA256 sha256 = SHA256.Create();

                var currentSalt = utf8.GetBytes(login);

                var saltedPassword = password + login;

                var currentSaltedPassword = utf8.GetBytes(saltedPassword);

                var hashForSaltedPassword = sha256.ComputeHash(currentSaltedPassword);

                var hashArray = new byte[currentSalt.Length + hashForSaltedPassword.Length];

                currentSalt.CopyTo(hashArray, 0);

                hashForSaltedPassword.CopyTo(hashArray, currentSalt.Length);

                hash = sha256.ComputeHash(hashArray);

                Array.Clear(hashArray, 0, hashArray.Length);
            }
            else
            {
                throw new ArgumentNullException();
            }

            return hash;
        }

            /* Метод, который сравнивает строковое представление хеша 
             * реального пароля пользователя в кодировке UTF-8 из БД
             * 
             * На вход: логин пользователя, пароль для проверки, хеш реального пароля из БД
             * 
             * Используется кодировка UTF-8 для правильного сравнения массивов байт
             */
        public bool PasswordsAreEqual(string login, string passwordForVerification, string hashForVerification)
        {
            UTF8Encoding utf8 = new UTF8Encoding();

            byte[] currentHash = utf8.GetBytes(hashForVerification);

            byte[] hash = HashPassword(login, passwordForVerification);

            var currentArrayForHash = utf8.GetString(hash);
            hash = utf8.GetBytes(currentArrayForHash);

            if (hash.Length == currentHash.Length)
            {
                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != currentHash[i])
                        return false;
                }

                return true;
            }

            return false;
        }
	}
}