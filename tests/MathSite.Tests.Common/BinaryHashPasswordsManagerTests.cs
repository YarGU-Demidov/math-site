using System;
using System.Linq;
using MathSite.Common.Crypto;
using Xunit;

namespace MathSite.Tests.Common
{
	public class BinaryHashPasswordsManagerTests
	{
		[Fact]
		public void PasswordBytesAreEqual()
		{
			const string login = "login";
			const string pass = "pass";

			var hasher = new DoubleSha512HashPasswordsManager();

			var resultBytes = hasher.CreatePassword(login, pass);

			var result = hasher.PasswordsAreEqual(login, pass, resultBytes);

			Assert.True(result);
		}

		[Fact]
		public void LoginNull()
		{
			var hasher = new DoubleSha512HashPasswordsManager();

			Assert.Throws<ArgumentNullException>("login", () => hasher.PasswordsAreEqual(null, "test", new byte[0]));
		}

		[Fact]
		public void PasswordNull()
		{
			var hasher = new DoubleSha512HashPasswordsManager();

			Assert.Throws<ArgumentNullException>("password", () => hasher.PasswordsAreEqual("test", null, new byte[0]));
		}

		[Fact]
		public void DifferentHashesAreNotEqual()
		{
			const string login = "login";
			const string pass = "pass";

			var hasher = new DoubleSha512HashPasswordsManager();

			var resultBytes = hasher.CreatePassword(login, pass);

			var result = hasher.PasswordsAreEqual($"login_{login}", pass, resultBytes);

			Assert.False(result);
		}

		[Fact]
		public void DifferentHashesLengthAreNotEqual()
		{
			const string login = "login";
			const string pass = "pass";

			var hasher = new DoubleSha512HashPasswordsManager();

			var resultBytes = hasher.CreatePassword(login, pass);

			var result = hasher.PasswordsAreEqual(login, pass, resultBytes.TakeWhile((b, i) => i < 10).ToArray());

			Assert.False(result);
		}
	}
}