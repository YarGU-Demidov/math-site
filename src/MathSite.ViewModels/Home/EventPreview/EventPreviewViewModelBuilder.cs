using System;
using MathSite.Entities;
using MathSite.ViewModels.Home.PostPreview;

namespace MathSite.ViewModels.Home.EventPreview
{
    public interface IEventPreviewViewModelBuilder
    {
        EventPreviewViewModel Build(Post post);
    }

    public class EventPreviewViewModelBuilder : PostPreviewViewModelBuilder<EventPreviewViewModel>, IEventPreviewViewModelBuilder
    {
        public EventPreviewViewModel Build(Post post)
        {
            var model = BuildPreviewModel(post);

            model.DateAndTime = post.PostSettings.EventTime ?? DateTime.Today;
            model.Location = post.PostSettings.EventLocation;
            
            return model;
        }
    }
}