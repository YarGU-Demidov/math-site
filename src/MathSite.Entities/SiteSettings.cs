using System;

namespace MathSite.Entities
{
	public class SiteSettings
	{
		public SiteSettings()
		{
		}

		public SiteSettings(Guid id, string key, byte[] value)
		{
			Id = id;
			Key = key;
			Value = value;
		}

		public SiteSettings(string key, byte[] value)
			: this(Guid.NewGuid(), key, value)
		{
		}

		public Guid Id { get; set; }
		public string Key { get; set; }
		public byte[] Value { get; set; }
	}
}