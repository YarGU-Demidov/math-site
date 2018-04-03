using System;
using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.Posts
{
    public class PostViewModel : AdminPageBaseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public Guid AuthorId { get; set; }
        public Guid PostTypeId { get; set; }
        public IEnumerable<Guid> SelectedCategories { get; set; }

        #region PostSetting

        public bool IsCommentsAllowed { get; set; }
        public bool CanBeRated { get; set; }
        public bool PostOnStartPage { get; set; }
        public Guid? PreviewImageId { get; set; }

        #endregion

        #region PostSeoSetting

        public string Url { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }

        #endregion

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }
    }
}