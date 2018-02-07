using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Db.DbExtensions
{
    public static class ClearableDbContext
    {
        public static async Task Clear<TEntity>(this MathSiteDbContext context, string tableName = null) 
            where TEntity : class
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync($"TRUNCATE TABLE \"{tableName ?? typeof(TEntity).Name}\"");
            }
            catch (Exception e) when (e.GetType().Name == "SqliteException")
            {
                var sql = $"DELETE FROM \"{tableName ?? typeof(TEntity).Name}\"";
                await context.Database.ExecuteSqlCommandAsync(sql);
            }
        }
    }
}