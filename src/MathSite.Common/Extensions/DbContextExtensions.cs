using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Common.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        ///     Allows to execute raw SQL and map it into POCO-class.
        /// </summary>
        /// <typeparam name="T">The type to which the data is mapped.</typeparam>
        /// <param name="context">Current context.</param>
        /// <param name="query">Plain text with RAW sql.</param>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteSql<T>(this DbContext context, string query)
            where T : class, new()
        {
            var task = ExecuteSqlAsync<T>(context, query);

            task.ConfigureAwait(false);

            Task.WaitAll(task);

            return task.Result;
        }

        /// <summary>
        ///     Allows to execute raw SQL and map it into POCO-class.
        /// </summary>
        /// <typeparam name="T">The type to which the data is mapped.</typeparam>
        /// <param name="context">Current context.</param>
        /// <param name="query">Plain text with RAW sql.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecuteSqlAsync<T>(this DbContext context, string query)
            where T : class, new()
        {
            using (context)
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    await context.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        var resultList = new List<T>();

                        while (await result.ReadAsync())
                            resultList.Add(GetObject<T>(result));

                        return resultList;
                    }
                }
            }
        }

        /// <summary>
        ///     Allows to execute raw SQL.
        /// </summary>
        /// <param name="context">Current context.</param>
        /// <param name="query">Plain text with RAW sql.</param>
        /// <returns></returns>
        public static async Task ExecuteSqlAsync(this DbContext context, string query)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                await context.Database.OpenConnectionAsync();

                await command.ExecuteNonQueryAsync();
            }

            await context.SaveChangesAsync();
        }

        private static T GetObject<T>(DbDataReader reader)
        {
            var obj = Activator.CreateInstance<T>();
            foreach (var prop in typeof(T).GetProperties())
            {
                var val = GetFieldOrDefaultValue(reader, prop);
                if (!Equals(val, DBNull.Value))
                    prop.SetValue(obj, val, null);
            }

            return obj;
        }

        private static object GetFieldOrDefaultValue(DbDataReader reader, PropertyInfo propertyInfo)
        {
            try
            {
                return reader[propertyInfo.Name];
            }
            catch (Exception e) when (e is ArgumentOutOfRangeException || e is IndexOutOfRangeException)
            {
                var propType = propertyInfo.GetType();
                return propType.IsValueType
                    ? Activator.CreateInstance(propType)
                    : null;
            }
        }
    }
}