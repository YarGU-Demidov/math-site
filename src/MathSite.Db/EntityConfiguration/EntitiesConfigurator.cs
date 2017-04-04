using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.EntityConfiguration
{
	/// <inheritdoc />
	public class EntitiesConfigurator : IEntitiesConfigurator
	{
		private readonly ILogger<EntitiesConfigurator> _logger;
		private readonly List<IEntityConfiguration> _configurations;

		/// <inheritdoc />
		public EntitiesConfigurator(ILogger<EntitiesConfigurator> logger)
		{
			_logger = logger;
			_configurations = new List<IEntityConfiguration>();
		}

		/// <inheritdoc />
		public void AddConfiguration(IEntityConfiguration configuration)
		{
			_configurations.Add(configuration);
		}

		/// <inheritdoc />
		public void Configure(ModelBuilder modelBuilder)
		{
			_logger.LogInformation("Configuring Entities starts!");
			foreach (var configuration in _configurations)
			{
				_logger.LogInformation($"Configuring {configuration.ConfigurationName}...");
				configuration.Configure(modelBuilder);
				_logger.LogInformation($"Configuring {configuration.ConfigurationName} complete!");
			}
			_logger.LogInformation("Done! Configuring Entities complete!");
		}
	}
}