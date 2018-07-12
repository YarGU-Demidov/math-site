using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.ViewModels.Messaging
{
    public class ConversationMemberViewModel: CommonViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Status { get; set; }
    }
}
