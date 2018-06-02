using System;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    public class Professor : Entity
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }

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
    }
}