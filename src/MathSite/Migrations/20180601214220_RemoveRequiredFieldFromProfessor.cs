using Microsoft.EntityFrameworkCore.Migrations;

namespace MathSite.Migrations
{
    public partial class RemoveRequiredFieldFromProfessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Professor",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Professor",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}