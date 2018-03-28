using AutoMapper;
using MathSite.BasicAdmin.ViewModels.Events;
using MathSite.BasicAdmin.ViewModels.News;
using MathSite.BasicAdmin.ViewModels.Pages;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Dtos
{
    public class EntitiesProfile : Profile
    {
        public EntitiesProfile()
        {
            CreateMap<Post, PageViewModel>();
            CreateMap<Post, NewsViewModel>();
            CreateMap<Post, EventViewModel>();
            CreateMap<PageViewModel, Post>();
            CreateMap<NewsViewModel, Post>();
            CreateMap<EventViewModel, Post>();
        }
    }
}
