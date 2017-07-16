namespace MathSite.Common.Crypto
{
	public interface IPasswordsManager
	{
		/// <summary>
		///     Метод, получающий строковое представление хеша пароля для занесения в БД.
		///     Используется кодировка UTF-8.
		/// </summary>
		/// <param name="login">Логин</param>
		/// <param name="password">Пароль</param>
		/// <returns>Строковое представление пароля.</returns>
		byte[] CreatePassword(string login, string password);

		/// <summary>
		///     Метод, который сравнивает строковое представление хеша. <br />
		///     реального пароля пользователя в кодировке UTF-8 из БД. <br />
		///     Используется кодировка UTF-8 для правильного сравнения массивов байт.
		/// </summary>
		/// <param name="login">Логин пользователя</param>
		/// <param name="passwordForVerification">Пароль для проверки</param>
		/// <param name="hashForVerification">Хеш реального пароля из БД</param>
		/// <returns></returns>
		bool PasswordsAreEqual(string login, string passwordForVerification, byte[] hashForVerification);
	}
}