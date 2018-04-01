using System;
using MathSite.BasicAdmin.ViewModels.SharedModels.Posts;

namespace MathSite.BasicAdmin.ViewModels.Events
{
    public class EventViewModel : PostViewModel
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}