using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathSite.ViewModels.Messaging
{
    public class MessageViewModel : CommonViewModel
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public string CreationDate { get; set; }
    }
}
