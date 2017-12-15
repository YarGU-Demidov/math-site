﻿// <auto-generated />

using System;
using MathSite.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Migrations
{
    [DbContext(typeof(MathSiteDbContext))]
    internal class MathSiteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:uuid-ossp", "'uuid-ossp', '', ''")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("MathSite.Entities.Category", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Alias")
                    .IsRequired();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Description");

                b.Property<string>("Name")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("Alias")
                    .IsUnique();

                b.ToTable("Category");
            });

            modelBuilder.Entity("MathSite.Entities.Comment", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<DateTime>("Date");

                b.Property<bool>("Edited");

                b.Property<Guid>("PostId");

                b.Property<string>("Text")
                    .IsRequired();

                b.Property<Guid>("UserId");

                b.HasKey("Id");

                b.HasIndex("PostId");

                b.HasIndex("UserId");

                b.ToTable("Comment");
            });

            modelBuilder.Entity("MathSite.Entities.Directory", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Name");

                b.Property<Guid?>("RootDirectoryId");

                b.HasKey("Id");

                b.HasIndex("RootDirectoryId");

                b.ToTable("Directory");
            });

            modelBuilder.Entity("MathSite.Entities.File", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid?>("DirectoryId");

                b.Property<string>("Extension");

                b.Property<string>("Hash")
                    .IsRequired();

                b.Property<string>("Name")
                    .IsRequired();

                b.Property<string>("Path")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("DirectoryId");

                b.HasIndex("Hash");

                b.ToTable("File");
            });

            modelBuilder.Entity("MathSite.Entities.Group", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Alias")
                    .IsRequired();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Description");

                b.Property<Guid>("GroupTypeId");

                b.Property<bool>("IsAdmin")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValue(false);

                b.Property<string>("Name")
                    .IsRequired();

                b.Property<Guid?>("ParentGroupId");

                b.HasKey("Id");

                b.HasIndex("Alias")
                    .IsUnique();

                b.HasIndex("GroupTypeId");

                b.HasIndex("ParentGroupId");

                b.ToTable("Group");
            });

            modelBuilder.Entity("MathSite.Entities.GroupsRight", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("Allowed");

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid>("GroupId");

                b.Property<Guid>("RightId");

                b.HasKey("Id");

                b.HasIndex("GroupId");

                b.HasIndex("RightId");

                b.ToTable("GroupsRight");
            });

            modelBuilder.Entity("MathSite.Entities.GroupType", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Alias")
                    .IsRequired();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Description");

                b.Property<string>("Name")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("Alias")
                    .IsUnique();

                b.ToTable("GroupType");
            });

            modelBuilder.Entity("MathSite.Entities.Keyword", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Alias")
                    .IsRequired();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Name")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("Alias")
                    .IsUnique();

                b.ToTable("Keyword");
            });

            modelBuilder.Entity("MathSite.Entities.Person", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("AdditionalPhone");

                b.Property<DateTime>("Birthday");

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("MiddleName");

                b.Property<string>("Name")
                    .IsRequired();

                b.Property<string>("Phone");

                b.Property<Guid?>("PhotoId");

                b.Property<string>("Surname")
                    .IsRequired();

                b.Property<Guid?>("UserId");

                b.HasKey("Id");

                b.HasIndex("PhotoId")
                    .IsUnique();

                b.HasIndex("UserId")
                    .IsUnique();

                b.ToTable("Person");
            });

            modelBuilder.Entity("MathSite.Entities.Post", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<Guid>("AuthorId");

                b.Property<string>("Content")
                    .IsRequired();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<bool>("Deleted");

                b.Property<string>("Excerpt")
                    .IsRequired();

                b.Property<Guid>("PostSeoSettingsId");

                b.Property<Guid?>("PostSettingsId");

                b.Property<Guid>("PostTypeId");

                b.Property<DateTime>("PublishDate");

                b.Property<bool>("Published");

                b.Property<string>("Title")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("AuthorId");

                b.HasIndex("PostSeoSettingsId")
                    .IsUnique();

                b.HasIndex("PostSettingsId")
                    .IsUnique();

                b.HasIndex("PostTypeId");

                b.ToTable("Post");
            });

            modelBuilder.Entity("MathSite.Entities.PostAttachment", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("Allowed");

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid>("FileId");

                b.Property<Guid>("PostId");

                b.HasKey("Id");

                b.HasIndex("FileId");

                b.HasIndex("PostId");

                b.ToTable("PostAttachment");
            });

            modelBuilder.Entity("MathSite.Entities.PostCategory", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<Guid>("CategoryId");

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid>("PostId");

                b.HasKey("Id");

                b.HasIndex("CategoryId");

                b.HasIndex("PostId");

                b.ToTable("PostCategory");
            });

            modelBuilder.Entity("MathSite.Entities.PostGroupsAllowed", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("Allowed");

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid?>("GroupId");

                b.Property<Guid?>("PostId");

                b.HasKey("Id");

                b.HasIndex("GroupId");

                b.HasIndex("PostId");

                b.ToTable("PostGroupsAllowed");
            });

            modelBuilder.Entity("MathSite.Entities.PostKeyword", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid>("KeywordId");

                b.Property<Guid>("PostSeoSettingsId");

                b.HasKey("Id");

                b.HasIndex("KeywordId");

                b.HasIndex("PostSeoSettingsId");

                b.ToTable("PostKeyword");
            });

            modelBuilder.Entity("MathSite.Entities.PostOwner", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid>("PostId");

                b.Property<Guid>("UserId");

                b.HasKey("Id");

                b.HasIndex("PostId");

                b.HasIndex("UserId");

                b.ToTable("PostOwner");
            });

            modelBuilder.Entity("MathSite.Entities.PostRating", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("Allowed");

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid>("PostId");

                b.Property<Guid>("UserId");

                b.Property<bool?>("Value")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("PostId");

                b.HasIndex("UserId");

                b.ToTable("PostRating");
            });

            modelBuilder.Entity("MathSite.Entities.PostSeoSetting", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Description");

                b.Property<string>("Title");

                b.Property<string>("Url");

                b.HasKey("Id");

                b.HasIndex("Url");

                b.ToTable("PostSeoSetting");
            });

            modelBuilder.Entity("MathSite.Entities.PostSetting", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("CanBeRated")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValue(false);

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<bool>("IsCommentsAllowed")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValue(false);

                b.Property<string>("Layout")
                    .IsRequired()
                    .ValueGeneratedOnAdd()
                    .HasDefaultValue("SecondaryLayout");

                b.Property<bool>("PostOnStartPage")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValue(false);

                b.Property<Guid?>("PreviewImageId");

                b.HasKey("Id");

                b.HasIndex("PreviewImageId");

                b.ToTable("PostSetting");
            });

            modelBuilder.Entity("MathSite.Entities.PostType", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Alias")
                    .IsRequired();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid>("DefaultPostsSettingsId");

                b.Property<string>("Name")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("Alias")
                    .IsUnique();

                b.HasIndex("DefaultPostsSettingsId")
                    .IsUnique();

                b.ToTable("PostType");
            });

            modelBuilder.Entity("MathSite.Entities.PostUserAllowed", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("Allowed");

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid>("PostId");

                b.Property<Guid>("UserId");

                b.HasKey("Id");

                b.HasIndex("PostId");

                b.HasIndex("UserId");

                b.ToTable("PostUserAllowed");
            });

            modelBuilder.Entity("MathSite.Entities.Right", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Alias")
                    .IsRequired();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Description");

                b.Property<string>("Name")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("Alias")
                    .IsUnique();

                b.ToTable("Right");
            });

            modelBuilder.Entity("MathSite.Entities.SiteSetting", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Key")
                    .IsRequired();

                b.Property<byte[]>("Value")
                    .IsRequired();

                b.HasKey("Id");

                b.HasAlternateKey("Key");

                b.HasIndex("Key")
                    .IsUnique();

                b.ToTable("SiteSetting");
            });

            modelBuilder.Entity("MathSite.Entities.User", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid>("GroupId");

                b.Property<string>("Login")
                    .IsRequired();

                b.Property<byte[]>("PasswordHash")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("GroupId");

                b.ToTable("User");
            });

            modelBuilder.Entity("MathSite.Entities.UserSetting", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<string>("Key")
                    .IsRequired();

                b.Property<string>("Namespace")
                    .IsRequired();

                b.Property<Guid>("UserId");

                b.Property<string>("Value")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("UserSetting");
            });

            modelBuilder.Entity("MathSite.Entities.UsersRight", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("Allowed");

                b.Property<DateTime>("CreationDate")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                b.Property<Guid>("RightId");

                b.Property<Guid>("UserId");

                b.HasKey("Id");

                b.HasIndex("RightId");

                b.HasIndex("UserId");

                b.ToTable("UserRights");
            });

            modelBuilder.Entity("MathSite.Entities.Comment", b =>
            {
                b.HasOne("MathSite.Entities.Post", "Post")
                    .WithMany("Comments")
                    .HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.User", "User")
                    .WithMany("Comments")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.Directory", b =>
            {
                b.HasOne("MathSite.Entities.Directory", "RootDirectory")
                    .WithMany("Directories")
                    .HasForeignKey("RootDirectoryId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.File", b =>
            {
                b.HasOne("MathSite.Entities.Directory", "Directory")
                    .WithMany("Files")
                    .HasForeignKey("DirectoryId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.Group", b =>
            {
                b.HasOne("MathSite.Entities.GroupType", "GroupType")
                    .WithMany("Groups")
                    .HasForeignKey("GroupTypeId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.Group", "ParentGroup")
                    .WithMany("ChildGroups")
                    .HasForeignKey("ParentGroupId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.GroupsRight", b =>
            {
                b.HasOne("MathSite.Entities.Group", "Group")
                    .WithMany("GroupsRights")
                    .HasForeignKey("GroupId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.Right", "Right")
                    .WithMany("GroupsRights")
                    .HasForeignKey("RightId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.Person", b =>
            {
                b.HasOne("MathSite.Entities.File", "Photo")
                    .WithOne("Person")
                    .HasForeignKey("MathSite.Entities.Person", "PhotoId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.User", "User")
                    .WithOne("Person")
                    .HasForeignKey("MathSite.Entities.Person", "UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.Post", b =>
            {
                b.HasOne("MathSite.Entities.User", "Author")
                    .WithMany("Posts")
                    .HasForeignKey("AuthorId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.PostSeoSetting", "PostSeoSetting")
                    .WithOne("Post")
                    .HasForeignKey("MathSite.Entities.Post", "PostSeoSettingsId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.PostSetting", "PostSettings")
                    .WithOne("Post")
                    .HasForeignKey("MathSite.Entities.Post", "PostSettingsId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.PostType", "PostType")
                    .WithMany("Posts")
                    .HasForeignKey("PostTypeId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.PostAttachment", b =>
            {
                b.HasOne("MathSite.Entities.File", "File")
                    .WithMany("PostAttachments")
                    .HasForeignKey("FileId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.Post", "Post")
                    .WithMany("PostAttachments")
                    .HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.PostCategory", b =>
            {
                b.HasOne("MathSite.Entities.Category", "Category")
                    .WithMany("PostCategories")
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.Post", "Post")
                    .WithMany("PostCategories")
                    .HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.PostGroupsAllowed", b =>
            {
                b.HasOne("MathSite.Entities.Group", "Group")
                    .WithMany("PostGroupsAllowed")
                    .HasForeignKey("GroupId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.Post", "Post")
                    .WithMany("GroupsAllowed")
                    .HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.PostKeyword", b =>
            {
                b.HasOne("MathSite.Entities.Keyword", "Keyword")
                    .WithMany("Posts")
                    .HasForeignKey("KeywordId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.PostSeoSetting", "PostSeoSettings")
                    .WithMany("PostKeywords")
                    .HasForeignKey("PostSeoSettingsId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.PostOwner", b =>
            {
                b.HasOne("MathSite.Entities.Post", "Post")
                    .WithMany("PostOwners")
                    .HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.User", "User")
                    .WithMany("PostsOwner")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.PostRating", b =>
            {
                b.HasOne("MathSite.Entities.Post", "Post")
                    .WithMany("PostRatings")
                    .HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.User", "User")
                    .WithMany("PostsRatings")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.PostSetting", b =>
            {
                b.HasOne("MathSite.Entities.File", "PreviewImage")
                    .WithMany("PostSettings")
                    .HasForeignKey("PreviewImageId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.PostType", b =>
            {
                b.HasOne("MathSite.Entities.PostSetting", "DefaultPostsSettings")
                    .WithOne("PostType")
                    .HasForeignKey("MathSite.Entities.PostType", "DefaultPostsSettingsId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.PostUserAllowed", b =>
            {
                b.HasOne("MathSite.Entities.Post", "Post")
                    .WithMany("UsersAllowed")
                    .HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.User", "User")
                    .WithMany("AllowedPosts")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.User", b =>
            {
                b.HasOne("MathSite.Entities.Group", "Group")
                    .WithMany("Users")
                    .HasForeignKey("GroupId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.UserSetting", b =>
            {
                b.HasOne("MathSite.Entities.User", "User")
                    .WithMany("Settings")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MathSite.Entities.UsersRight", b =>
            {
                b.HasOne("MathSite.Entities.Right", "Right")
                    .WithMany("UsersRights")
                    .HasForeignKey("RightId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("MathSite.Entities.User", "User")
                    .WithMany("UserRights")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}