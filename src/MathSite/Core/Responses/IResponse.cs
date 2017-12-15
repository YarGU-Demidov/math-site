using Newtonsoft.Json;

namespace MathSite.Core.Responses
{
    public interface IResponse<out T>
    {
        [JsonProperty("error")]
        string Error { get; }

        [JsonProperty("result")]
        string Result { get; }

        [JsonProperty("data")]
        T Data { get; }
    }
}