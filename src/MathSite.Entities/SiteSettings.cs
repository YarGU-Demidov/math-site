using System;

namespace MathSite.Entities
{
	public class SiteSettings: IEquatable<SiteSettings>
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

		public bool Equals(SiteSettings other)
		{
			return string.Equals(Key, other.Key) && Equals(Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((SiteSettings) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Key != null ? Key.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
			}
		}
	}
}