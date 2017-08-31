using System.Collections.Generic;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class CategorySeeder : AbstractSeeder<Category>
	{
		/// <inheritdoc />
		public CategorySeeder(ILogger logger, IMathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(Category);

		/// <inheritdoc />
		protected override void SeedData()
		{
			var educationCategory = CreateCategory(
				"Education",
				"Education at the University",
				"Education"
			);

			var careerCategory = CreateCategory(
				"Career",
				"Career after University",
				"Career"
			);

			var categories = new[]
			{
				educationCategory,
				careerCategory
			};

			Context.Categories.AddRange(categories);
		}

		private static Category CreateCategory(string name, string description, string alias)
		{
			return new Category
			{
				Name = name,
				Description = description,
				Alias = alias,
				PostCategories = new List<PostCategory>()
			};
		}
	}
}