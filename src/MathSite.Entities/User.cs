using System;
using System.Collections.Generic;
using MathSite.Common.Entities;

namespace MathSite.Entities
{
    /// <summary>
    ///     Пользователь.
    /// </summary>
    public class User : Entity
    {
        public User()
        {
        }

        /// <summary>
        ///     Создает сущность.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="passwordHash">Пароль.</param>
        /// <param name="groupId">Идентификатор группы.</param>
        public User(string login, byte[] passwordHash, byte[] twoFactorAutentificationHash, Guid groupId)
        {
            Login = login;
            PasswordHash = passwordHash;
            TwoFactorAutentificationHash = twoFactorAutentificationHash;
            GroupId = groupId;
            CreationDate = DateTime.Now;
        }

        /// <summary>
        ///     Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ///     Хэш пароля.
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        ///     Хэш кюча для двухфакторной авторизации.
        /// </summary>
        public byte[] TwoFactorAutentificationHash { get; set; }

        /// <summary>
        ///     Личность.
        /// </summary>
        public Person Person { get; set; }
        
        /// <summary>
        ///     Идентификатор пользователя.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        ///     Идентификатор группы.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        ///     Группа, к которой относится пользователь.
        /// </summary>
        public Group Group { get; set; }
        
        /// <summary>
        ///     Перечень постов пользователя.
        /// </summary>
        public ICollection<PostOwner> PostsOwner { get; set; } = new List<PostOwner>();

        /// <summary>
        ///     Список постов, написаных этим пользователем.
        /// </summary>
        public ICollection<Post> Posts { get; set; } = new List<Post>();

        /// <summary>
        ///     Перечень постов, к которым разрешен доступ.
        /// </summary>
        public ICollection<PostUserAllowed> AllowedPosts { get; set; } = new List<PostUserAllowed>();

        /// <summary>
        ///     Перечень оценок пользователя к постам, которые он оценил.
        /// </summary>
        public ICollection<PostRating> PostsRatings { get; set; } = new List<PostRating>();

        /// <summary>
        ///     Перечень комментариев пользователя.
        /// </summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        /// <summary>
        ///     Перечень настроек пользователя.
        /// </summary>
        public ICollection<UserSetting> Settings { get; set; } = new List<UserSetting>();

        /// <summary>
        ///     Перечень прав пользователя.
        /// </summary>
        public ICollection<UsersRight> UserRights { get; set; } = new List<UsersRight>();
        
    }
}