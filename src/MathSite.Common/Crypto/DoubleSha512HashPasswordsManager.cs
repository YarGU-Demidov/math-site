using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MathSite.Common.Crypto
{
	public class DoubleSha512HashPasswordsManager : IPasswordsManager
	{
		/// <inheritdoc />
		public bool PasswordsAreEqual(string login, string password, byte[] hashForVerification)
		{
			var hash = CreatePassword(login, password);

			if (hash.Length != hashForVerification.Length)
				return false;

			return !hash.Where((currentByte, index) => currentByte != hashForVerification[index]).Any();
		}

		/// <inheritdoc />
		public byte[] CreatePassword(string login, string password)
		{
			if (password == null)
				throw new ArgumentNullException(nameof(password));

			if (login == null)
				throw new ArgumentNullException(nameof(login));

			var saltedStr = GetSaltedString(login, password);

			var saltedPasswordBytes = GetSaltedPasswordBytes(saltedStr);

			var hashForSaltedPassword = ComputeHash(saltedPasswordBytes);

			var hash = ComputeHash(hashForSaltedPassword);

			return hash;
		}

		private string GetSaltedString(string login, string password)
		{
			return password + login;
		}

		private byte[] GetSaltedPasswordBytes(string str)
		{
			return Encoding.UTF8.GetBytes(str);
		}

		private byte[] ComputeHash(byte[] passwordBytes)
		{
			var sha512 = SHA512.Create();
			using (sha512)
			{
				return sha512.ComputeHash(passwordBytes);
			}
		}
	}
}