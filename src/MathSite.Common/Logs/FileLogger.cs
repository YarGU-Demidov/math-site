using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace MathSite.Common.Logs
{
	public class FileLogger : ILogger
	{
		private readonly string _filepath;

		public FileLogger(string filepath)
		{
			_filepath = filepath;
		}

		public void WriteInfo(string message, params object[] values)
		{
			WriteToFile(PrepareString("INFO", message, values));
		}

		public void WriteDebug(string message, params object[] values)
		{
			WriteToFile(PrepareString("DEBUG", message, values));
		}

		public void WriteError(string message, params object[] values)
		{
			WriteToFile(PrepareString("ERROR", message, values));
		}

		private void WriteToFile(string message)
		{
			using (var fileStream = new FileStream(_filepath, FileMode.Append))
			{
				using (var file = new StreamWriter(fileStream, Encoding.UTF8))
				{
					file.WriteLineAsync(message);
				}
			}
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