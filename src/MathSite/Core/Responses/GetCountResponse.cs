using MathSite.Core.Responses.ResponseTypes;

namespace MathSite.Core.Responses
{
    public class GetCountResponse : IResponse<int?>
    {
        public GetCountResponse(IResponseType result, string error, int? data = null)
        {
            Error = error;
            Result = result.TypeName;
            Data = data;
        }

        public string Error { get; }
        public string Result { get; }
        public int? Data { get; }
    }
}