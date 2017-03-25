using MathSite.Core;

namespace MathSite.ViewModels.Api.UsersInfo.Responses
{
	public class GetAllResponse : IResponse<UserInfo[]>
	{
		public string Error { get; }
		public string Result { get; }
		public UserInfo[] Data { get; }

		public GetAllResponse(string result, UserInfo[] data = null, string error = null)
		{
			Error = error;
			Result = result;
			Data = data;
		}
	}
}