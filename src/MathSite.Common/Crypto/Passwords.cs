namespace MathSite.Common.Crypto
{
	public class Passwords : IPasswordsManager
	{
		public string CreatePasswordString(string password)
		{
			return password;
		}

		public bool PasswordsAreEqual(string firstPassword, string secondPassword)
		{
			return firstPassword == secondPassword;
		}
	}
}