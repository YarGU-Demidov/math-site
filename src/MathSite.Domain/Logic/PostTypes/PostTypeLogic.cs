using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.PostTypes
{
	public interface IPostTypeLogic
	{
		Task<PostType> TryGetTypeByAlias(string alias);
	}
	
	public class PostTypeLogic : LogicBase<PostType>, IPostTypeLogic
	{
		public PostTypeLogic(MathSiteDbContext context) : base(context)
		{
		}

		public async Task<PostType> TryGetTypeByAlias(string alias)
		{
			PostType postType = null;
			await UseContextAsync(async context =>
			{
				postType = await GetFromItemsAsync(types => types.FirstOrDefaultAsync(type => type.Alias == alias));
			});

			return postType;
		}
	}
}