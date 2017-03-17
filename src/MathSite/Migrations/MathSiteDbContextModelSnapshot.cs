﻿using System;
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
			modelBuilder
				.HasAnnotation("Npgsql:PostgresExtension:uuid-ossp", "'uuid-ossp', '', ''")
				.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
				.HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

			modelBuilder.Entity("MathSite.Models.Group", b =>
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

				b.ToTable("Groups");
			});

			modelBuilder.Entity("MathSite.Models.GroupsRights", b =>
			{
				b.Property<Guid>("Id")
					.ValueGeneratedOnAdd();

				b.Property<bool>("Allowed");

				b.Property<Guid>("GroupId");

				b.Property<Guid>("RightId");

				b.HasKey("Id");

				b.HasIndex("GroupId");

				b.HasIndex("RightId");

				b.ToTable("GroupsRights");
			});

			modelBuilder.Entity("MathSite.Models.Person", b =>
			{
				b.Property<Guid>("Id")
					.ValueGeneratedOnAdd();

				b.Property<string>("MiddleName");

				b.Property<string>("Name")
					.IsRequired();

				b.Property<string>("Surname")
					.IsRequired();

				b.Property<Guid?>("UserId");

				b.HasKey("Id");

				b.HasIndex("UserId")
					.IsUnique();

				b.ToTable("Persons");
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

				b.Property<Guid>("GroupId");

				b.Property<string>("Login")
					.IsRequired();

				b.Property<string>("PasswordHash")
					.IsRequired();

				b.HasKey("Id");

				b.HasIndex("GroupId");

				b.ToTable("Users");
			});

			modelBuilder.Entity("MathSite.Models.UsersRights", b =>
			{
				b.Property<Guid>("Id")
					.ValueGeneratedOnAdd();

				b.Property<bool>("Allowed");

				b.Property<Guid>("RightId");

				b.Property<Guid>("UserId");

				b.HasKey("Id");

				b.HasIndex("RightId");

				b.HasIndex("UserId");

				b.ToTable("UsersRights");
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

			modelBuilder.Entity("MathSite.Models.User", b =>
			{
				b.HasOne("MathSite.Models.Group", "Group")
					.WithMany("Users")
					.HasForeignKey("GroupId")
					.OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity("MathSite.Models.UsersRights", b =>
			{
				b.HasOne("MathSite.Models.Right", "Right")
					.WithMany("UsersRights")
					.HasForeignKey("RightId")
					.OnDelete(DeleteBehavior.Cascade);

				b.HasOne("MathSite.Models.User", "User")
					.WithMany("UsersRights")
					.HasForeignKey("UserId")
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}