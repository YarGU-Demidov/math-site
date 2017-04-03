using System;
using System.Collections.Generic;

namespace MathSite.Models
{
    public class KeyWord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public List<PostKeywords> PostKeywords { get; set; }
    }
}
