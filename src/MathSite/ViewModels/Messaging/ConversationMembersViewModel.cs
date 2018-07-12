using System.Collections.Generic;
using System.Net;

namespace MathSite.ViewModels.Messaging
{
    public class ConversationMembersViewModel : CommonViewModel
    {
        public ConversationMemberViewModel Creator { get; set; }
        public IEnumerable<ConversationMemberViewModel> Members { get; set; }
    }
}
