using MathSite.Core.Responses.ResponseTypes;

namespace MathSite.Core.Responses
{
    public class GetAllResponse<T> : BaseResponse<T[]>
    {
        public GetAllResponse(IResponseType result, string error = null, T[] data = null)
            : base(result, error, data)
        {
        }
    }
}