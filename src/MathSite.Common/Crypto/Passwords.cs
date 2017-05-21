﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace MathSite.Common.Crypto
{
	public class Passwords : IPasswordsManager
	{
		private byte[] _hash; // хэш пароля

		/// <inheritdoc />
		public string CreatePasswordString(string login, string password)
		{
			_hash = HashPassword(login, password);
			var utf8 = Encoding.UTF8;
			return utf8.GetString(_hash);
		}

		/// <inheritdoc />
		public bool PasswordsAreEqual(string login, string passwordForVerification, string hashForVerification)
		{
			var utf8 = new UTF8Encoding();

			var currentHash = utf8.GetBytes(hashForVerification);

			var hash = HashPassword(login, passwordForVerification);

			var currentArrayForHash = utf8.GetString(hash);
			hash = utf8.GetBytes(currentArrayForHash);

			if (hash.Length == currentHash.Length)
			{
				for (var i = 0; i < hash.Length; i++)
					if (hash[i] != currentHash[i])
						return false;

				return true;
			}

			return false;
		}

		private byte[] HashPassword(string login, string password)
		{
			/**
			 * Для более эффективной защиты hash строится так:
			 * 1) складываем строки salt и password
			 * 2) получаем их хеш с помощью SHA512 алгоритма
			 * 3) снова получаем хеш с помощью sha512
			 */

			byte[] hash;

			if (password != null && login != null)
			{
				var utf8 = Encoding.UTF8;
				var sha512 = SHA512.Create();

				var saltedPassword = password + login;

				var currentSaltedPassword = utf8.GetBytes(saltedPassword);

				var hashForSaltedPassword = sha512.ComputeHash(currentSaltedPassword);

				hash = sha512.ComputeHash(hashForSaltedPassword);

				//очистка масиивов во избежание утечки памяти   
				Array.Clear(currentSaltedPassword, 0, currentSaltedPassword.Length);
				Array.Clear(hashForSaltedPassword, 0, hashForSaltedPassword.Length);
			}
			else
			{
				throw new ArgumentNullException();
			}

			return hash;
		}
	}
}