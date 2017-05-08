using Newtonsoft.Json;

namespace MathSite.Core.DataTableApi
{
	public class FilterAndSortData<TSortData>
	{
		[JsonProperty("sort")]
		public TSortData SortData { get; protected set; }
		[JsonProperty("filter")]
		public FilterData FilterData { get; protected set; }
	}
}