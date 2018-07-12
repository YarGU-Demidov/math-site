using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MathSite.ViewModels.Messaging
{
    public class ConversationViewModel : CommonViewModel
    {
        public string Id { get; set; }
   
        public string Name { get; set; }

        public string CreatorsId { get; set; }

        public int UnreadMessagesCount { get; set; }

        public string Type { get; set; }


        //Private
        public string InterlocutorLogin { get; set; }
    }
}
