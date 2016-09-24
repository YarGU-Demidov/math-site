using System;
using System.Globalization;

namespace Math.Common.Logs
{
	public class ConsoleLogger : ILogger
	{
		public void WriteInfo(string message, params object[] values)
		{
			Console.WriteLine(PrepareString("INFO", message, values));
		}

		public void WriteDebug(string message, params object[] values)
		{
			Console.WriteLine(PrepareString("DEBUG", message, values));
		}

		public void WriteError(string message, params object[] values)
		{
			Console.WriteLine(PrepareString("ERROR", message, values));
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
	}
}