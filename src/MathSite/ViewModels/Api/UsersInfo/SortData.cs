using MathSite.Common;
using Newtonsoft.Json;

namespace MathSite.ViewModels.Api.UsersInfo
{
	public class SortData
	{
		/// <summary>
		///		Сортировка по группе пользователя
		/// </summary>
		[JsonProperty("group")]
		public SortDirection GroupSort { get; set; }
	}
}