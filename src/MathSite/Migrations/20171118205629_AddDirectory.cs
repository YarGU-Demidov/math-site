using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MathSite.Migrations
{
    public partial class AddDirectory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                "DirectoryId",
                "File",
                "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                "Directory",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    CreationDate =
                        table.Column<DateTime>("timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Name = table.Column<string>("text", nullable: true),
                    RootDirectoryId = table.Column<Guid>("uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directory", x => x.Id);
                    table.ForeignKey(
                        "FK_Directory_Directory_RootDirectoryId",
                        x => x.RootDirectoryId,
                        "Directory",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_File_DirectoryId",
                "File",
                "DirectoryId");

            migrationBuilder.CreateIndex(
                "IX_Directory_RootDirectoryId",
                "Directory",
                "RootDirectoryId");

            migrationBuilder.AddForeignKey(
                "FK_File_Directory_DirectoryId",
                "File",
                "DirectoryId",
                "Directory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_File_Directory_DirectoryId",
                "File");

            migrationBuilder.DropTable(
                "Directory");

            migrationBuilder.DropIndex(
                "IX_File_DirectoryId",
                "File");

            migrationBuilder.DropColumn(
                "DirectoryId",
                "File");
        }
    }
}