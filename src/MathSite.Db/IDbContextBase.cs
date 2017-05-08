using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Db
{
	/// <summary>
	/// Базовый интерфейс контекста базы данных.
	/// </summary>
	public interface IDbContextBase : IDisposable
	{
		/// <summary>
		/// Асинхронно сохраняет в базе данных все изменения, сделанные в данном контексте.
		/// </summary>
		/// <exception cref="DbUpdateException">An error occurred sending updates to the database.</exception>
		/// <exception cref="DbUpdateConcurrencyException">
		///     A database command did not affect the expected number of rows. This usually indicates
		///     an optimistic concurrency violation; that is, a row has been changed in the database
		///     since it was queried.
		/// </exception>
		/// <exception cref="NotSupportedException">
		///     An attempt was made to use unsupported behavior such as executing multiple asynchronous 
		///     commands concurrently on the same context instance.</exception>
		/// <exception cref="ObjectDisposedException">The context or connection have been disposed.</exception>
		/// <exception cref="InvalidOperationException">
		///     Some error occurred attempting to process entities in the context either before 
		///     or after sending commands to the database.</exception>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}