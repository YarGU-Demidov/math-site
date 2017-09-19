using System;

namespace MathSite.Entities
{
    /// <summary>
    ///     Персона, она же сущность.
    ///     Представляет собой конкретного человека с его личными данными.
    /// </summary>
    public class Person
    {
        public Person()
        {
        }

        /// <summary>
        ///     Создает сущность.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="surname">Фамилия.</param>
        /// <param name="middlename">Отчество.</param>
        /// <param name="birthday">Дата рождения.</param>
        /// <param name="phoneNumber">Телефонный номер.</param>
        /// <param name="additionalPhoneNumber">Дополнительный телефонный номер.</param>
        /// <param name="photoId">Идентификатор изображения личности.</param>
        public Person(string name, string surname, string middlename,
            DateTime birthday, string phoneNumber, string additionalPhoneNumber, Guid? photoId)
        {
            Name = name;
            Surname = surname;
            MiddleName = middlename;
            Phone = phoneNumber;
            AdditionalPhone = additionalPhoneNumber;
            Birthday = birthday;
            CreationDate = DateTime.Now;
            PhotoId = photoId;
        }

        /// <summary>
        ///     Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

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
        ///     Дата регистрации.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        ///     Идентификатор изображения личности.
        /// </summary>
        public Guid? PhotoId { get; set; }

        /// <summary>
        ///     Файл.
        /// </summary>
        public File Photo { get; set; }

        /// <summary>
        ///     Идентификатор пользователя.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        ///     Пользователь.
        /// </summary>
        public User User { get; set; }
    }
}