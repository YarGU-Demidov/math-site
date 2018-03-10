using System;
using System.Collections.Generic;
using System.Text;
using MathSite.Entities;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public class PageModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public DateTime PublishDate { get; set; }
        public User Author { get; set; }
        public string SelectedAuthor { get; set; }
        public IEnumerable<User> Authors { get; set; }
        public PostType PostType { get; set; }
        public PostSetting PostSettings { get; set; }
        public PostSeoSetting PostSeoSetting { get; set; }
        public IEnumerable<PostCategory> PostCategories { get; set; }
    }
}
