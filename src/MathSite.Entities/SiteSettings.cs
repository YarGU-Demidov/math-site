namespace MathSite.Entities
{
	public class SiteSettings
	{
		public SiteSettings()
		{
		}

		public SiteSettings(string key, byte[] value)
		{
			Key = key;
			Value = value;
		}

		public string Key { get; set; }
		public byte[] Value { get; set; }
	}
}