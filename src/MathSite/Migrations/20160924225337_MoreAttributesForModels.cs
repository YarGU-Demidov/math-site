using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MathSite.Migrations
{
    public partial class MoreAttributesForModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Persons",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Persons",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Persons",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Persons",
                nullable: true);
        }
    }
}
