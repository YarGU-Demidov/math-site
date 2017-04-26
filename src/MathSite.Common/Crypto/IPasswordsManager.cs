namespace MathSite.Common.Crypto
{
	public interface IPasswordsManager
	{
		string CreatePasswordString(string login, string password);
		bool PasswordsAreEqual(string login, string passwordForVerification, string hashForVerification);
	}
}