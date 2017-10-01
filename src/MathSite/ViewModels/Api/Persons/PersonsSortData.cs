using MathSite.Core.DataTableApi;
using Newtonsoft.Json;

namespace MathSite.ViewModels.Api.Persons
{
    public class PersonsSortData
    {
        [JsonProperty("name")]
        public SortDirection Name { get; set; }

        [JsonProperty("surname")]
        public SortDirection Surame { get; set; }

        [JsonProperty("middlename")]
        public SortDirection Middlename { get; set; }
    }
}