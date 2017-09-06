using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.PostTypes
{
	public interface IPostTypeLogic
	{
		Task<PostType> TryGetTypeByName(string typeName);
	}
	
	public class PostTypeLogic : LogicBase<PostType>, IPostTypeLogic
	{
		public PostTypeLogic(MathSiteDbContext context) : base(context)
		{
		}

		public async Task<PostType> TryGetTypeByName(string typeName)
		{
			PostType postType = null;
			await UseContextAsync(async context =>
			{
				postType = await GetFromItemsAsync(types => types.FirstOrDefaultAsync(type => type.TypeName == typeName));
			});

			return postType;
		}
	}
}