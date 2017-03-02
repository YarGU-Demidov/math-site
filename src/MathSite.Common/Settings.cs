using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MathSite.Common
{
	public class Settings
	{
		[JsonIgnore] private static Settings _settingsInstance;

		[JsonConstructor]
		private Settings()
		{
		}

		[JsonIgnore]
		private static string PathToSettings { get; } = Path.Combine(Directory.GetCurrentDirectory(), "MathSiteSettings.json")
			;

		[JsonIgnore]
		public static Settings Instance
		{
			get
			{
				if (_settingsInstance == null)
				{
					if (!File.Exists(PathToSettings))
						new Settings().Save();

					_settingsInstance = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(PathToSettings));
				}

				return _settingsInstance;
			}
		}

		[JsonProperty("ConnectionString")]
		public string ConnectionString { get; set; } = "Server=127.0.0.1;Port=5432;Database=math;User Id=postgres;Password=0;"
			;

		public void Save()
		{
			var obj = JsonConvert.SerializeObject(this, Formatting.Indented);
			File.WriteAllText(PathToSettings, obj, Encoding.UTF8);
		}
	}
}