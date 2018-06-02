using System;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    /// <summary>
    ///     Персона, она же сущность.
    ///     Представляет собой конкретного человека с его личными данными.
    /// </summary>
    public class Person : Entity
    {
        /// <summary>
        ///     Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Фамилия.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        ///     Отчество.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        ///     Телефонный номер.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     Дополнительный телефонный номер.
        /// </summary>
        public string AdditionalPhone { get; set; }

        /// <summary>
        ///     Дата рождения.
        /// </summary>
        public DateTime Birthday { get; set; }
        
        /// <summary>
        ///     Идентификатор изображения личности.
        /// </summary>
        public Guid? PhotoId { get; set; }

        /// <summary>
        ///     Файл.
        /// </summary>
        public File Photo { get; set; }

        /// <summary>
        ///     Пользователь.
        /// </summary>
        public User User { get; set; }

        public Professor Professor { get; set; }
    }
}