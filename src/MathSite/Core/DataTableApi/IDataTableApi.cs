using MathSite.Core.Responses;
using MathSite.ViewModels.Api.UsersInfo;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Core.DataTableApi
{
	public interface IDataTableApi<TEntity, TSortData>
	{
		[HttpPost]
		GetAllResponse<TEntity> GetAll(int offset = 0, int count = 50, [FromBody] FilterAndSortData<TSortData> filterAndSortData = null);
		[HttpGet]
		GetCountResponse GetCount();
	}
}