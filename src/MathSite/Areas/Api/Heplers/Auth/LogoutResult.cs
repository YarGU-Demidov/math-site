namespace MathSite.Areas.Api.Heplers.Auth
{
	public class LogoutResult
	{
		public LogoutResult(LogoutStatus status)
		{
			LogoutStatus = status;
			Description = status.ToString();
		}

		public LogoutStatus LogoutStatus { get; set; }
		public string Description { get; set; }
	}
}