using System;
using System.Globalization;

namespace MathSite.Common.Logs
{
	public class ConsoleLogger : ILogger
	{
		private ConsoleColor _previousColor;

		public ConsoleLogger()
		{
			StoreColor();
		}

		public void WriteInfo(string message, params object[] values)
		{
			PrintMessageWithColor(ConsoleColor.Cyan, "INFO", message, values);
		}

		public void WriteDebug(string message, params object[] values)
		{
			PrintMessageWithColor(ConsoleColor.Cyan, "DEBUG", message, values);
		}

		public void WriteError(string message, params object[] values)
		{
			PrintMessageWithColor(ConsoleColor.Red, "ERROR", message, values);
		}

		private void SetColor(ConsoleColor color)
		{
			Console.ForegroundColor = color;
		}

		private static string GetFormatedDateAndTime()
		{
			var now = DateTime.UtcNow;
			return now.ToString(CultureInfo.InvariantCulture);
		}

		private static string PrepareString(string type, string message, object[] args)
		{
			var formatedString = string.Format(message, args);
			return $"[{type}] | {GetFormatedDateAndTime()} | {formatedString}";
		}

		private void StoreColor()
		{
			_previousColor = Console.ForegroundColor;
		}

		private void RestoreColor()
		{
			Console.ForegroundColor = _previousColor;
		}

		private void PrintMessageWithColor(ConsoleColor color, string messageType, string message, object[] values)
		{
			StoreColor();
			SetColor(color);
			Console.WriteLine(PrepareString(messageType, message, values));
			RestoreColor();
		}
	}
}