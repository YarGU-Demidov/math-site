using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MathSite.Migrations
{
    public partial class AddDirectory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DirectoryId",
                table: "File",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Name = table.Column<string>(type: "text", nullable: true),
                    RootDirectoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Directory_Directory_RootDirectoryId",
                        column: x => x.RootDirectoryId,
                        principalTable: "Directory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_DirectoryId",
                table: "File",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Directory_RootDirectoryId",
                table: "Directory",
                column: "RootDirectoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Directory_DirectoryId",
                table: "File",
                column: "DirectoryId",
                principalTable: "Directory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Directory_DirectoryId",
                table: "File");

            migrationBuilder.DropTable(
                name: "Directory");

            migrationBuilder.DropIndex(
                name: "IX_File_DirectoryId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "DirectoryId",
                table: "File");
        }
    }
}
