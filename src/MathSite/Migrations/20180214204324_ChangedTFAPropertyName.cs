using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MathSite.Migrations
{
    public partial class ChangedTFAPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwoFactorAutentificationKey",
                table: "User",
                newName: "TwoFactorAuthenticationKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwoFactorAuthenticationKey",
                table: "User",
                newName: "TwoFactorAutentificationKey");
        }
    }
}
