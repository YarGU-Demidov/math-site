using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MathSite.Migrations
{
    public partial class AddBaseProfessorInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BibliographicIndexOfWorks = table.Column<string[]>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Description = table.Column<string>(nullable: false),
                    Graduated = table.Column<string[]>(nullable: true),
                    MathNetLink = table.Column<string>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    ScientificTitle = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    TermPapers = table.Column<string[]>(nullable: true),
                    Theses = table.Column<string[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professor_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Professor_PersonId",
                table: "Professor",
                column: "PersonId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Professor");
        }
    }
}