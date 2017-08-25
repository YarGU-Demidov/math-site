using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	/// <summary>
	///     Тип группы.
	///     Например: пользовательская, студенчаская, сотрудников вуза.
	/// </summary>
	public class GroupType
	{
		/// <summary>
		///     Идентификатор группы.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		///     Имя типа группы.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Описание типа группы.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		///     Алиас типа группы.
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		///     Список групп этого типа.
		/// </summary>
		public ICollection<Group> Groups { get; set; } = new List<Group>();
	}
}