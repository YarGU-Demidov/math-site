using System;
using System.Collections.Generic;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MathSite.BasicAdmin.ViewModels.Professors
{
    public class CreateProfessorViewModel : BaseProfessorViewModel {}

    public class EditProfessorViewModel : BaseProfessorViewModel
    {
    }

    public class BaseProfessorViewModel : AdminPageBaseViewModel
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Факультет
        /// </summary>
        public string Faculty { get; set; }

        /// <summary>
        ///     Кафедра
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        ///     Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Ссылка на Mathnet
        /// </summary>
        public string MathNetLink { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     Научное звание
        /// </summary>
        public string ScientificTitle { set; get; }

        /// <summary>
        ///     Что окончил
        /// </summary>
        public string[] Graduated { get; set; } = new string[0];

        /// <summary>
        ///     Диссертации
        /// </summary>
        public string[] Theses { get; set; } = new string[0];

        /// <summary>
        ///     Темы курсовых и дипломных работ
        /// </summary>
        public string[] TermPapers { get; set; } = new string[0];


        /// <summary>
        ///     Библиографический указатель трудов
        /// </summary>
        public string[] BibliographicIndexOfWorks { get; set; } = new string[0];

        public IEnumerable<SelectListItem> AvailablePersons { get; set; }
        public Guid PersonId { get; set; }
    }
}