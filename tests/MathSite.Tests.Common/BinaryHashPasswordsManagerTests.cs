using MathSite.Common.Crypto;
using Xunit;

namespace MathSite.Tests.Common
{
	public class BinaryHashPasswordsManagerTests
	{
		[Fact]
		public void PasswordHash()
		{
			const string login = "login";
			const string pass = "pass";

			var hasher = new DoubleSha512HashPasswordsManager();

			var resultBytes = hasher.CreatePassword(login, pass);

			var result = hasher.PasswordsAreEqual(login, pass, resultBytes);

			Assert.True(result);
		}
	}
}