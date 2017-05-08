using MathSite.Core.Responses.ResponseTypes;

namespace MathSite.Core.Responses
{
	public class GetAllResponse<T> : IResponse<T[]>
	{
		public string Error { get; }
		public string Result { get; }
		public T[] Data { get; }

		public GetAllResponse(IResponseType result, T[] data = null, string error = null)
		{
			Error = error;
			Result = result.TypeName;
			Data = data;
		}
	}
}