using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Rights
{
    public class RightsLogic : LogicBase<Right>, IRightsLogic
    {
        public RightsLogic(MathSiteDbContext context)
            : base(context)
        {
        }

        public async Task CreateAsync(string alias, string name, string description)
        {
            await UseContextWithSaveAsync(async context =>
            {
                var right = new Right
                {
                    Alias = alias,
                    Description = description,
                    Name = name
                };

                await context.Rights.AddAsync(right);
            });
        }

        public async Task UpdateAsync(string alias, string name, string description)
        {
            await UseContextWithSaveAsync(async context =>
            {
                var right = await context.Rights.FirstAsync(r => r.Alias == alias);

                right.Alias = alias;
                right.Name = name;
                right.Description = description;

                context.Rights.Update(right);
            });
        }

        public async Task DeleteAsync(string alias)
        {
            await UseContextWithSaveAsync(async context =>
            {
                var right = await context.Rights.FirstAsync(r => r.Alias == alias);

                context.Rights.Remove(right);
            });
        }

        public async Task<Right> TryGetByAliasAsync(string alias)
        {
            Right right = null;
            await UseContextAsync(async context =>
            {
                right = await context.Rights.FirstOrDefaultAsync(r => r.Alias == alias);
            });

            return right;
        }
    }
}