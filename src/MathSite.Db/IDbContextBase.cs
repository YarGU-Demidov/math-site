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
		///		Асинхронно сохраняет в базе данных все изменения, сделанные в данном контексте.
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
		/// <remarks>
		///     <para>
		///         This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
		///         changes to entity instances before saving to the underlying database. This can be disabled via
		///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
		///     </para>
		///     <para>
		///         Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
		///         that any asynchronous operations have completed before calling another method on this context.
		///     </para>
		/// </remarks>
		/// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
		/// <returns>
		///     A task that represents the asynchronous save operation. The task result contains the
		///     number of state entries written to the database.
		/// </returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Асинхронно сохраняет в базе данных все изменения, сделанные в данном контексте.
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
		/// <param name="acceptAllChangesOnSuccess">
		///     Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
		///     been sent successfully to the database.
		/// </param>
		/// <remarks>
		///     <para>
		///         This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
		///         changes to entity instances before saving to the underlying database. This can be disabled via
		///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
		///     </para>
		///     <para>
		///         Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
		///         that any asynchronous operations have completed before calling another method on this context.
		///     </para>
		/// </remarks>
		/// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
		/// <returns>
		///     A task that represents the asynchronous save operation. The task result contains the
		///     number of state entries written to the database.
		/// </returns>
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
			CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Cохраняет в базе данных все изменения, сделанные в данном контексте.
		/// </summary>
		/// <remarks>
		///     This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
		///     changes to entity instances before saving to the underlying database. This can be disabled via
		///     <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
		/// </remarks>
		/// <returns>
		///     The number of state entries written to the database.
		/// </returns>
		int SaveChanges();

		/// <summary>
		///     Cохраняет в базе данных все изменения, сделанные в данном контексте.
		/// </summary>
		/// <param name="acceptAllChangesOnSuccess">
		///     Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
		///     been sent successfully to the database.
		/// </param>
		/// <remarks>
		///     This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
		///     changes to entity instances before saving to the underlying database. This can be disabled via
		///     <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
		/// </remarks>
		/// <returns>
		///     The number of state entries written to the database.
		/// </returns>
		int SaveChanges(bool acceptAllChangesOnSuccess);
	}
}