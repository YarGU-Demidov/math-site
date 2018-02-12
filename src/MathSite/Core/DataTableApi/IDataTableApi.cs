using System.Threading.Tasks;
using MathSite.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Core.DataTableApi
{
    public interface IDataTableApi<TEntity, TSortData>
    {
        [HttpPost]
        GetAllResponse<TEntity> GetAll(
            int offset = 0,
            int count = 50,
            FilterAndSortData<TSortData> filterAndSortData = null
        );

        [HttpGet]
        Task<GetCountResponse> GetCount();
    }
}