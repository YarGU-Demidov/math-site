namespace MathSite.Common.Responses
{
    public class SuccessResponse<T> : IResponse
    {
        public SuccessResponse(T jsonData)
        {
            Data = jsonData;
        }

        public T Data { get; }
        public string Status { get; } = "success";
    }
}