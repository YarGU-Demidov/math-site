namespace MathSite.Common.Crypto
{
	public interface IPasswordsManager
	{
		string CreatePasswordString(string password);
		bool PasswordsAreEqual(string firstPassword, string secondPassword);
	}
}