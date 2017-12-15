using MathSite.Core.Responses.ResponseTypes;

namespace MathSite.Core.Responses
{
    public class BaseResponse<T> : IResponse<T>
    {
        public BaseResponse(IResponseType result, string error, T data = default(T))
        {
            Error = error;
            Result = result.TypeName;
            Data = data;
        }

        public string Error { get; protected set; }
        public string Result { get; protected set; }
        public T Data { get; protected set; }
    }
}