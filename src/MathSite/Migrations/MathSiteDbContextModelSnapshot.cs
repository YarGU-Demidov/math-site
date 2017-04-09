using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MathSite.Db;

namespace MathSite.Migrations
{
    [DbContext(typeof(MathSiteDbContext))]
    partial class MathSiteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:uuid-ossp", "'uuid-ossp', '', ''")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("MathSite.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("Alias");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MathSite.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Allowed");

                    b.Property<DateTime>("Date");

                    b.Property<Guid?>("PostId")
                        .IsRequired();

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MathSite.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Extension");

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.Property<Guid?>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Files");
                });

            modelBuilder.Entity("MathSite.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<string>("Description");

                    b.Property<Guid?>("GroupTypeId");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ParentGroupId");

                    b.HasKey("Id");

                    b.HasIndex("GroupTypeId");

                    b.HasIndex("ParentGroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("MathSite.Models.GroupsRights", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Allowed");

                    b.Property<Guid?>("GroupId");

                    b.Property<Guid?>("RightId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("RightId");

                    b.ToTable("GroupsRights");
                });

            modelBuilder.Entity("MathSite.Models.GroupType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("GroupTypes");
                });

            modelBuilder.Entity("MathSite.Models.Keywords", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("Alias");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("MathSite.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalPhone");

                    b.Property<DateTime>("Birthday");

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("MiddleName");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<Guid?>("PhotoId");

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("MathSite.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<bool?>("Deleted");

                    b.Property<string>("Excerpt")
                        .IsRequired();

                    b.Property<Guid?>("PostTypeId")
                        .IsRequired();

                    b.Property<DateTime>("PublishDate");

                    b.Property<bool>("Published");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PostTypeId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("MathSite.Models.PostAttachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Allowed");

                    b.Property<Guid?>("FileId")
                        .IsRequired();

                    b.Property<Guid?>("PostId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("PostId");

                    b.ToTable("PostAttachments");
                });

            modelBuilder.Entity("MathSite.Models.PostCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoryId")
                        .IsRequired();

                    b.Property<Guid?>("PostId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PostId");

                    b.ToTable("PostCategories");
                });

            modelBuilder.Entity("MathSite.Models.PostGroupsAllowed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Allowed");

                    b.Property<Guid?>("GroupId");

                    b.Property<Guid?>("PostId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PostId");

                    b.ToTable("PostGroupsAlloweds");
                });

            modelBuilder.Entity("MathSite.Models.PostKeywords", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("KeywordId")
                        .IsRequired();

                    b.Property<Guid?>("PostSeoSettingsId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("KeywordId");

                    b.HasIndex("PostSeoSettingsId");

                    b.ToTable("PostKeywords");
                });

            modelBuilder.Entity("MathSite.Models.PostOwner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("PostId")
                        .IsRequired();

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostOwners");
                });

            modelBuilder.Entity("MathSite.Models.PostRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Allowed");

                    b.Property<Guid?>("PostId")
                        .IsRequired();

                    b.Property<Guid?>("UserId");

                    b.Property<bool?>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostRatings");
                });

            modelBuilder.Entity("MathSite.Models.PostSeoSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid?>("PostId")
                        .IsRequired();

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostSeoSettings");
                });

            modelBuilder.Entity("MathSite.Models.PostSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("CanBeRated");

                    b.Property<bool?>("IsCommentsAllowed");

                    b.Property<Guid?>("PostId");

                    b.Property<bool?>("PostOnStartPage");

                    b.Property<Guid?>("PostTypeId");

                    b.Property<Guid?>("PreviewImageId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("PostTypeId");

                    b.HasIndex("PreviewImageId");

                    b.ToTable("PostSettings");
                });

            modelBuilder.Entity("MathSite.Models.PostType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TypeName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PostTypes");
                });

            modelBuilder.Entity("MathSite.Models.PostUserAllowed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Allowed");

                    b.Property<Guid?>("PostId")
                        .IsRequired();

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostUserAlloweds");
                });

            modelBuilder.Entity("MathSite.Models.Right", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("Alias");

                    b.ToTable("Rights");
                });

            modelBuilder.Entity("MathSite.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("GroupId");

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<Guid>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MathSite.Models.UserSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<string>("Namespace")
                        .IsRequired();

                    b.Property<Guid?>("UserId");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSettingses");
                });

            modelBuilder.Entity("MathSite.Models.UsersRights", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Allowed");

                    b.Property<Guid?>("RightId")
                        .IsRequired();

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RightId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersRights");
                });

            modelBuilder.Entity("MathSite.Models.Comment", b =>
                {
                    b.HasOne("MathSite.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.File", b =>
                {
                    b.HasOne("MathSite.Models.Person", "Person")
                        .WithOne("Photo")
                        .HasForeignKey("MathSite.Models.File", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.Group", b =>
                {
                    b.HasOne("MathSite.Models.GroupType", "GroupType")
                        .WithMany("Groups")
                        .HasForeignKey("GroupTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.Group", "ParentGroup")
                        .WithMany()
                        .HasForeignKey("ParentGroupId");
                });

            modelBuilder.Entity("MathSite.Models.GroupsRights", b =>
                {
                    b.HasOne("MathSite.Models.Group", "Group")
                        .WithMany("GroupsRights")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.Right", "Right")
                        .WithMany("GroupsRights")
                        .HasForeignKey("RightId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.Person", b =>
                {
                    b.HasOne("MathSite.Models.User", "User")
                        .WithOne("Person")
                        .HasForeignKey("MathSite.Models.Person", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.Post", b =>
                {
                    b.HasOne("MathSite.Models.PostType", "PostType")
                        .WithMany("Posts")
                        .HasForeignKey("PostTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.PostAttachment", b =>
                {
                    b.HasOne("MathSite.Models.File", "File")
                        .WithMany("PostAttachments")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.Post", "Post")
                        .WithMany("PostAttachments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.PostCategory", b =>
                {
                    b.HasOne("MathSite.Models.Category", "Category")
                        .WithMany("PostCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.Post", "Post")
                        .WithMany("PostCategories")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.PostGroupsAllowed", b =>
                {
                    b.HasOne("MathSite.Models.Group", "Group")
                        .WithMany("PostGroupsAllowed")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.Post", "Post")
                        .WithMany("GroupsAllowed")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.PostKeywords", b =>
                {
                    b.HasOne("MathSite.Models.Keywords", "Keyword")
                        .WithMany("Posts")
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.PostSeoSettings", "PostSeoSettings")
                        .WithMany("PostKeywords")
                        .HasForeignKey("PostSeoSettingsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.PostOwner", b =>
                {
                    b.HasOne("MathSite.Models.Post", "Post")
                        .WithMany("PostOwners")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.User", "User")
                        .WithMany("PostsOwner")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.PostRating", b =>
                {
                    b.HasOne("MathSite.Models.Post", "Post")
                        .WithMany("PostRatings")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.User", "User")
                        .WithMany("PostsRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.PostSeoSettings", b =>
                {
                    b.HasOne("MathSite.Models.Post", "Post")
                        .WithMany("PostSeoSettings")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.PostSettings", b =>
                {
                    b.HasOne("MathSite.Models.Post", "Post")
                        .WithMany("PostSettings")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.PostType", "PostType")
                        .WithMany("DefaultPostsSettings")
                        .HasForeignKey("PostTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.File", "PreviewImage")
                        .WithMany("PostSettings")
                        .HasForeignKey("PreviewImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.PostUserAllowed", b =>
                {
                    b.HasOne("MathSite.Models.Post", "Post")
                        .WithMany("UsersAllowed")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.User", "User")
                        .WithMany("AllowedPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.User", b =>
                {
                    b.HasOne("MathSite.Models.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.UserSettings", b =>
                {
                    b.HasOne("MathSite.Models.User", "User")
                        .WithMany("Settings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathSite.Models.UsersRights", b =>
                {
                    b.HasOne("MathSite.Models.Right", "Right")
                        .WithMany("UsersRights")
                        .HasForeignKey("RightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathSite.Models.User", "User")
                        .WithMany("UserRights")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
