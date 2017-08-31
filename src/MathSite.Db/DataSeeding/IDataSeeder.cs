namespace MathSite.Db.DataSeeding
{
	/// <summary>
	///     Позволяет добавить данные в базу
	/// </summary>
	public interface IDataSeeder
	{
		/// <summary>
		///     Запуск добавления данных
		/// </summary>
		void Seed();
	}
}