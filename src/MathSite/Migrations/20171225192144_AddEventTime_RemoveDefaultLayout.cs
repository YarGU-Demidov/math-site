using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MathSite.Migrations
{
    public partial class AddEventTime_RemoveDefaultLayout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Layout",
                "PostSetting",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "SecondaryLayout");

            migrationBuilder.AddColumn<DateTime>(
                "EventTime",
                "PostSetting",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "EventTime",
                "PostSetting");

            migrationBuilder.AlterColumn<string>(
                "Layout",
                "PostSetting",
                nullable: false,
                defaultValue: "SecondaryLayout",
                oldClrType: typeof(string));
        }
    }
}