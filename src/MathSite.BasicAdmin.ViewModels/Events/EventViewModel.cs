using System;
using MathSite.BasicAdmin.ViewModels.SharedModels.Posts;

namespace MathSite.BasicAdmin.ViewModels.Events
{
    public class EventViewModel : PostViewModel
    {
        public DateTime? EventTime { get; set; }
        public string EventLocation { get; set; }
    }
}