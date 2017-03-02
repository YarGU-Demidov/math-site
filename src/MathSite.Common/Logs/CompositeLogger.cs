using System;
using System.Collections.Generic;

namespace MathSite.Common.Logs
{
	public class CompositeLogger : ILogger
	{
		private readonly List<ILogger> _loggers = new List<ILogger>();

		public CompositeLogger()
		{
		}

		public CompositeLogger(ILogger logger)
		{
			_loggers.Add(logger);
		}

		public CompositeLogger(IEnumerable<ILogger> loggers)
		{
			_loggers.AddRange(loggers);
		}

		public void WriteInfo(string message, params object[] values)
		{
			WriteToAll(logger => logger.WriteInfo(message, values));
		}

		public void WriteDebug(string message, params object[] values)
		{
			WriteToAll(logger => logger.WriteDebug(message, values));
		}

		public void WriteError(string message, params object[] values)
		{
			WriteToAll(logger => logger.WriteError(message, values));
		}

		public void Add(ILogger logger)
		{
			_loggers.Add(logger);
		}

		public void AddRange(IEnumerable<ILogger> loggers)
		{
			_loggers.AddRange(loggers);
		}

		private void WriteToAll(Action<ILogger> log)
		{
			foreach (var logger in _loggers)
				log(logger);
		}
	}
}