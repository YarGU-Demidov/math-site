using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MathSite.Migrations
{
    public partial class RemoveDeprecatedFieldFromFileEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DateAdded",
                "File");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                "DateAdded",
                "File",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}