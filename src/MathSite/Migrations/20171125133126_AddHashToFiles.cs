using Microsoft.EntityFrameworkCore.Migrations;

namespace MathSite.Migrations
{
    public partial class AddHashToFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Hash",
                "File",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                "IX_File_Hash",
                "File",
                "Hash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_File_Hash",
                "File");

            migrationBuilder.DropColumn(
                "Hash",
                "File");
        }
    }
}