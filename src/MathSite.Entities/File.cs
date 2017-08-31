using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	/// <summary>
	///     Файл.
	/// </summary>
	public class File
	{
		public File()
			: this(null, null, null, null)
		{
		}

		/// <summary>
		///     Создает сущность.
		/// </summary>
		/// <param name="fileName">Название файла.</param>
		/// <param name="filePath">Путь к файлу.</param>
		/// <param name="extension">Расширение файла.</param>
		/// <param name="person">Личность.</param>
		public File(string fileName, string filePath, string extension, Person person)
		{
			FileName = fileName;
			FilePath = filePath;
			Extension = extension;
			Person = person;
			DateAdded = DateTime.Now;
		}

		/// <summary>
		///     Идентификатор.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		///     Имя файла.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		///     Путь к файлу в файловой системе
		/// </summary>
		public string FilePath { get; set; }

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
		public ICollection<PostSettings> PostSettings { get; set; } = new List<PostSettings>();

		/// <summary>
		///     Список вложений поста, к которым привязан этот файл.
		/// </summary>
		public ICollection<PostAttachment> PostAttachments { get; set; } = new List<PostAttachment>();
	}
}