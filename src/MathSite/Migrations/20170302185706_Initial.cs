using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MathSite.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterDatabase()
				.Annotation("Npgsql:PostgresExtension:uuid-ossp", "'uuid-ossp', '', ''");

			migrationBuilder.CreateTable(
				"Groups",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Alias = table.Column<string>(nullable: false),
					Description = table.Column<string>(nullable: true),
					Name = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Groups", x => x.Id);
					table.UniqueConstraint("AK_Groups_Alias", x => x.Alias);
				});

			migrationBuilder.CreateTable(
				"Rights",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Alias = table.Column<string>(nullable: false),
					Description = table.Column<string>(nullable: true),
					Name = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Rights", x => x.Id);
					table.UniqueConstraint("AK_Rights_Alias", x => x.Alias);
				});

			migrationBuilder.CreateTable(
				"Users",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					GroupId = table.Column<Guid>(nullable: false),
					Login = table.Column<string>(nullable: false),
					PasswordHash = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
					table.ForeignKey(
						"FK_Users_Groups_GroupId",
						x => x.GroupId,
						"Groups",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"GroupsRights",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Allowed = table.Column<bool>(nullable: false),
					GroupId = table.Column<Guid>(nullable: false),
					RightId = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_GroupsRights", x => x.Id);
					table.ForeignKey(
						"FK_GroupsRights_Groups_GroupId",
						x => x.GroupId,
						"Groups",
						"Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						"FK_GroupsRights_Rights_RightId",
						x => x.RightId,
						"Rights",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"Persons",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					MiddleName = table.Column<string>(nullable: true),
					Name = table.Column<string>(nullable: false),
					Surname = table.Column<string>(nullable: false),
					UserId = table.Column<Guid>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Persons", x => x.Id);
					table.ForeignKey(
						"FK_Persons_Users_UserId",
						x => x.UserId,
						"Users",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"UsersRights",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Allowed = table.Column<bool>(nullable: false),
					RightId = table.Column<Guid>(nullable: false),
					UserId = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UsersRights", x => x.Id);
					table.ForeignKey(
						"FK_UsersRights_Rights_RightId",
						x => x.RightId,
						"Rights",
						"Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						"FK_UsersRights_Users_UserId",
						x => x.UserId,
						"Users",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				"IX_GroupsRights_GroupId",
				"GroupsRights",
				"GroupId");

			migrationBuilder.CreateIndex(
				"IX_GroupsRights_RightId",
				"GroupsRights",
				"RightId");

			migrationBuilder.CreateIndex(
				"IX_Persons_UserId",
				"Persons",
				"UserId",
				unique: true);

			migrationBuilder.CreateIndex(
				"IX_Users_GroupId",
				"Users",
				"GroupId");

			migrationBuilder.CreateIndex(
				"IX_UsersRights_RightId",
				"UsersRights",
				"RightId");

			migrationBuilder.CreateIndex(
				"IX_UsersRights_UserId",
				"UsersRights",
				"UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"GroupsRights");

			migrationBuilder.DropTable(
				"Persons");

			migrationBuilder.DropTable(
				"UsersRights");

			migrationBuilder.DropTable(
				"Rights");

			migrationBuilder.DropTable(
				"Users");

			migrationBuilder.DropTable(
				"Groups");
		}
	}
}