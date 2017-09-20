namespace MathSite.Areas.Api.Heplers.Auth
{
    public class LoginResult
    {
        public LoginResult(LoginStatus loginStatus)
        {
            LoginStatus = loginStatus;
            Description = loginStatus.ToString();
        }

        public LoginStatus LoginStatus { get; }
        public string Description { get; set; }
    }
}