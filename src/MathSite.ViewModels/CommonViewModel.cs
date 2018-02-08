using System;
using System.Collections.Generic;
using System.Reflection;
using MathSite.ViewModels.SharedModels;

namespace MathSite.ViewModels
{
    public abstract class CommonViewModel
    {
        public PageTitleViewModel PageTitle { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public IEnumerable<MenuItemViewModel> TopMenuLinks { get; set; } = new List<MenuItemViewModel>();

        public IEnumerable<IEnumerable<MenuItemViewModel>> MainMenuLinks { get; set; }

        public static string AssemblyVersion { get; } = typeof(CommonViewModel).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
    }
}