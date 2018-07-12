using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathSite.ViewModels.Messaging
{
    public class ChatUserViewModel: CommonViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Login { get; set; }
    }
}
