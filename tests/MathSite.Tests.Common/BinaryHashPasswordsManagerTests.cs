using System;
using System.Linq;
using MathSite.Common.Crypto;
using Xunit;

namespace MathSite.Tests.Common
{
	public class BinaryHashPasswordsManagerTests
	{
		private readonly DoubleSha512HashPasswordsManager _manager;

		public BinaryHashPasswordsManagerTests()
		{
			_manager = new DoubleSha512HashPasswordsManager();
		}

		[Fact]
		public void PasswordBytesAreEqual()
		{
			const string login = "login";
			const string pass = "pass";
			
			var resultBytes = _manager.CreatePassword(login, pass);

			var result = _manager.PasswordsAreEqual(login, pass, resultBytes);

			Assert.True(result);
		}

		[Fact]
		public void LoginNull()
		{
			Assert.Throws<ArgumentNullException>("login", () => _manager.PasswordsAreEqual(null, "test", new byte[0]));
		}

		[Fact]
		public void PasswordNull()
		{
			Assert.Throws<ArgumentNullException>("password", () => _manager.PasswordsAreEqual("test", null, new byte[0]));
		}

		[Fact]
		public void DifferentHashesAreNotEqual()
		{
			const string login = "login";
			const string pass = "pass";
			
			var resultBytes = _manager.CreatePassword(login, pass);

			var result = _manager.PasswordsAreEqual($"login_{login}", pass, resultBytes);

			Assert.False(result);
		}

		[Fact]
		public void DifferentHashesLengthAreNotEqual()
		{
			const string login = "login";
			const string pass = "pass";
			
			var resultBytes = _manager.CreatePassword(login, pass);

			var result = _manager.PasswordsAreEqual(login, pass, resultBytes.TakeWhile((b, i) => i < 10).ToArray());

			Assert.False(result);
		}
	}
}