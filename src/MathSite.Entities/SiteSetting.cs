using MathSite.Common.Entities;

namespace MathSite.Entities
{
    public class SiteSetting : Entity
    {
        public SiteSetting()
        {
        }

        public SiteSetting(string key, byte[] value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public byte[] Value { get; set; }
    }
}