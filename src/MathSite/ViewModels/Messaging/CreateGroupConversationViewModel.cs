using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MathSite.ViewModels.Messaging
{
    public class CreateGroupConversationViewModel:CommonViewModel
    {
        public string Name { get; set; }

        public IEnumerable<Guid> MembersIds { get; set; }
    }
}
