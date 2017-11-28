using MathSite.Core.Responses.ResponseTypes;

namespace MathSite.Core.Responses
{
    public class GetCountResponse : BaseResponse<int?>
    {
        public GetCountResponse(IResponseType result, string error, int? data = null)
            : base(result, error, data)
        {
        }
    }
}