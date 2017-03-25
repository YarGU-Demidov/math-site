using Newtonsoft.Json;

namespace MathSite.Core
{
	public interface IResponse<T>
	{
		[JsonProperty("error")]
		string Error { get; }

		[JsonProperty("result")]
		string Result { get; }
		
		[JsonProperty("data")]
		T Data { get; }
	}
}