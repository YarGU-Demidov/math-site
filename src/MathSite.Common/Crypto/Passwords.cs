namespace MathSite.Common.Crypto
{
	public class Passwords: IPasswordHasher
	{
		public string GetHash(string password)
		{
			return password;
		}
	}
}