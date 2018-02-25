using System;
using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;
using MathSite.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public class PageViewModel : AdminPageBaseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthorId { get; set; }
        public string SelectedAuthor { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }
        public Guid PostTypeId { get; set; }
        public Guid? PostSettingsId { get; set; }
        public Guid PostSeoSettingsId { get; set; }
        public IEnumerable<PostCategory> PostCategories { get; set; }
    }
}
