using Newtonsoft.Json;

namespace MathSite.Core.DataTableApi
{
    public class FilterData
    {
        [JsonProperty("globalFilter")]
        public string GlobalFilter { get; set; }
    }
}