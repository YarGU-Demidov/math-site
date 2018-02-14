using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MathSite.Migrations
{
    public partial class ChangingTFAkeyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwoFactorAutentificationHash",
                table: "User",
                newName: "TwoFactorAutentificationKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwoFactorAutentificationKey",
                table: "User",
                newName: "TwoFactorAutentificationHash");
        }
    }
}
