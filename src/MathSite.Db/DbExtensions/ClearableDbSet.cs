using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Db.DbExtensions
{
    public static class ClearableDbSet
    {
        public static async Task Clear<TEntity>(this DbSet<TEntity> self, string tableName = null) 
            where TEntity : class
        {
            await self.FromSql($"TRUNCATE TABLE {tableName ?? typeof(TEntity).Name}").ToArrayAsync();
        }
    }
}