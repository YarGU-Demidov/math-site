using AutoMapper;
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
            CreateMap<PageViewModel, Post>();
            CreateMap<Post, NewsViewModel>();
            CreateMap<NewsViewModel, Post>();
        }
    }
}
