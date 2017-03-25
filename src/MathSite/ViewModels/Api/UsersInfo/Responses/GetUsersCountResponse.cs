using MathSite.Core;

namespace MathSite.ViewModels.Api.UsersInfo.Responses
{
	public class GetUsersCountResponse : IResponse<int?>
	{
		public string Error { get; }
		public string Result { get; }
		public int? Data { get; }

		public GetUsersCountResponse(string result, string error, int? data = null)
		{
			Error = error;
			Result = result;
			Data = data;
		}
	}
}