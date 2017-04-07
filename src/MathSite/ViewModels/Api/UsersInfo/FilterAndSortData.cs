using Newtonsoft.Json;

namespace MathSite.ViewModels.Api.UsersInfo
{
	public class FilterAndSortData
	{
		[JsonProperty("sort")]
		public SortData SortData { get; protected set; }
		[JsonProperty("filter")]
		public FilterData FilterData { get; protected set; }
	}
}