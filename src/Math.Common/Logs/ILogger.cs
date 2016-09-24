namespace Math.Common.Logs
{
	public interface ILogger
	{
		void WriteInfo(string message, params object[] values);
		void WriteDebug(string message, params object[] values);
		void WriteError(string message, params object[] values);
	}
}