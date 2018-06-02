using Microsoft.EntityFrameworkCore.Migrations;

namespace MathSite.Migrations
{
    public partial class ExtendProfessorsInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Professor",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Faculty",
                table: "Professor",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "Faculty",
                table: "Professor");
        }
    }
}