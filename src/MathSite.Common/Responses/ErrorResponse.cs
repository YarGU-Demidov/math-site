namespace MathSite.Common.Responses
{
    public class ErrorResponse : IResponse
    {
        public ErrorResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public ErrorResponse(string message)
            : this(-1, message)
        {
        }

        public int Code { get; }
        public string Message { get; }
        public string Status { get; } = "error";
    }
}