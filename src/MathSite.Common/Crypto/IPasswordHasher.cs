namespace MathSite.Common.Crypto
{
	public interface IPasswordHasher
	{
		string GetHash(string password);
	}
}