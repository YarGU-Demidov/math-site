using System;
using System.Collections.Generic;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    /// <summary>
    ///     Файл.
    /// </summary>
    public class File : EntityWithName
    {
        public File()
            : this(null, null, null)
        {
        }

        /// <summary>
        ///     Создает сущность.
        /// </summary>
        /// <param name="name">Название файла.</param>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="extension">Расширение файла.</param>
        public File(string name, string path, string extension)
        {
            Name = name;
            Path = path;
            Extension = extension;
            DateAdded = DateTime.Now;
        }

        /// <summary>
        ///     Путь к файлу в файловой системе
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     Расширение файла
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        ///     Дата добавления файла.
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        ///     Личность добавившего файл.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        ///     Список настроек поста, к которым привязан файл.
        /// </summary>
        public ICollection<PostSetting> PostSettings { get; set; } = new List<PostSetting>();

        /// <summary>
        ///     Список вложений поста, к которым привязан этот файл.
        /// </summary>
        public ICollection<PostAttachment> PostAttachments { get; set; } = new List<PostAttachment>();
    }
}