using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	/// <summary>
	///		Файл.
	/// </summary>
	public class File
	{
		public File() { }

		/// <summary>
		///		Создает сущность.
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
		/// 
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string FilePath { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Extension { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime DateAdded { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Person Person { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public List<PostSettings> PostSettings { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public List<PostAttachment> PostAttachments { get; set; }
	}
}