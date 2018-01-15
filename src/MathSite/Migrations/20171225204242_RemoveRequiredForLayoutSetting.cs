using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MathSite.Migrations
{
    public partial class RemoveRequiredForLayoutSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Layout",
                table: "PostSetting",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Layout",
                table: "PostSetting",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
