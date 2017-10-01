using System;
using Newtonsoft.Json;

namespace MathSite.ViewModels.Api.Persons
{
    public class Person
    {
        public Person(Guid id, string name, string surname, string middleName, string phone, string additionalPhone,
            DateTime birthday, DateTime creationDate, Guid? photoId, bool isUser)
        {
            Id = id;
            Name = name;
            Surname = surname;
            MiddleName = middleName;
            Phone = phone;
            AdditionalPhone = additionalPhone;
            Birthday = birthday;
            CreationDate = creationDate;
            PhotoId = photoId;
            IsUser = isUser;
        }

        public Person(Entities.Person person)
            : this(person.Id, person.Name, person.Surname, person.MiddleName, person.Phone, person.AdditionalPhone,
                person.Birthday, person.CreationDate, person.PhotoId, person.UserId != null)
        {
            if(CreationDate == default(DateTime))
                CreationDate = DateTime.UtcNow;
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("additionalPhone")]
        public string AdditionalPhone { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("photoId")]
        public Guid? PhotoId { get; set; }

        [JsonProperty("isUser")]
        public bool IsUser { get; set; }
    }
}