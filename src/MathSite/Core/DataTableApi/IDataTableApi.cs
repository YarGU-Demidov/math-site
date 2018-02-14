using System.Threading.Tasks;
using MathSite.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Core.DataTableApi
{
    public interface IDataTableApi<TSortData>
    {
        [HttpPost]
        IResponse GetAll(
            int offset = 0,
            int count = 50,
            FilterAndSortData<TSortData> filterAndSortData = null
        );

        [HttpGet]
        Task<IResponse> GetCount();
    }
}