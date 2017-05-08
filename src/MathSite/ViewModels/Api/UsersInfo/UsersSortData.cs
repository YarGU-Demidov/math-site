using MathSite.Core.DataTableApi;
using Newtonsoft.Json;

namespace MathSite.ViewModels.Api.UsersInfo
{
	public class UsersSortData
	{
		/// <summary>
		///		Сортировка по группе пользователя
		/// </summary>
		[JsonProperty("group")]
		public SortDirection GroupSort { get; set; }
	}
}