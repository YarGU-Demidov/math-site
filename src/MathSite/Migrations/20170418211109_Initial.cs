using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MathSite.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", "'uuid-ossp', '', ''");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Alias = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.UniqueConstraint("AK_Categories_Alias", x => x.Alias);
                });

            migrationBuilder.CreateTable(
                name: "GroupTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Alias = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Alias = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                    table.UniqueConstraint("AK_Keywords_Alias", x => x.Alias);
                });

            migrationBuilder.CreateTable(
                name: "PostSeoSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PostId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSeoSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DefaultPostsSettingsId = table.Column<Guid>(nullable: true),
                    TypeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Alias = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rights", x => x.Id);
                    table.UniqueConstraint("AK_Rights_Alias", x => x.Alias);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Alias = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GroupTypeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentGroupId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_GroupTypes_GroupTypeId",
                        column: x => x.GroupTypeId,
                        principalTable: "GroupTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Groups_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostKeywords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    KeywordId = table.Column<Guid>(nullable: false),
                    PostSeoSettingsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostKeywords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostKeywords_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostKeywords_PostSeoSettings_PostSeoSettingsId",
                        column: x => x.PostSeoSettingsId,
                        principalTable: "PostSeoSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupsRights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Allowed = table.Column<bool>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    RightId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupsRights_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsRights_Rights_RightId",
                        column: x => x.RightId,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: true, defaultValueSql: "NOW()"),
                    GroupId = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdditionalPhone = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: true, defaultValueSql: "NOW()"),
                    MiddleName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    PhotoId = table.Column<Guid>(nullable: true),
                    Surname = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSettingses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: false),
                    Namespace = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettingses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettingses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersRights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Allowed = table.Column<bool>(nullable: false),
                    RightId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersRights_Rights_RightId",
                        column: x => x.RightId,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRights_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Extension = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CanBeRated = table.Column<bool>(nullable: true),
                    IsCommentsAllowed = table.Column<bool>(nullable: true),
                    PostId = table.Column<Guid>(nullable: true),
                    PostOnStartPage = table.Column<bool>(nullable: true),
                    PostTypeId = table.Column<Guid>(nullable: true),
                    PreviewImageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostSettings_PostTypes_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "PostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostSettings_Files_PreviewImageId",
                        column: x => x.PreviewImageId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: true),
                    Excerpt = table.Column<string>(nullable: false),
                    PostSeoSettingsId = table.Column<Guid>(nullable: true),
                    PostSettingsId = table.Column<Guid>(nullable: true),
                    PostTypeId = table.Column<Guid>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_PostSeoSettings_PostSeoSettingsId",
                        column: x => x.PostSeoSettingsId,
                        principalTable: "PostSeoSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_PostSettings_PostSettingsId",
                        column: x => x.PostSettingsId,
                        principalTable: "PostSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_PostTypes_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "PostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Allowed = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Allowed = table.Column<bool>(nullable: false),
                    FileId = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostAttachments_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostAttachments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCategories_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostGroupsAlloweds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Allowed = table.Column<bool>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: true),
                    PostId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGroupsAlloweds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostGroupsAlloweds_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostGroupsAlloweds_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostOwners",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostOwners_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostOwners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Allowed = table.Column<bool>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Value = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostRatings_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostUserAlloweds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Allowed = table.Column<bool>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUserAlloweds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostUserAlloweds_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUserAlloweds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_PersonId",
                table: "Files",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupTypeId",
                table: "Groups",
                column: "GroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ParentGroupId",
                table: "Groups",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsRights_GroupId",
                table: "GroupsRights",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsRights_RightId",
                table: "GroupsRights",
                column: "RightId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UserId",
                table: "Persons",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostSeoSettingsId",
                table: "Posts",
                column: "PostSeoSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostSettingsId",
                table: "Posts",
                column: "PostSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostTypeId",
                table: "Posts",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostAttachments_FileId",
                table: "PostAttachments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_PostAttachments_PostId",
                table: "PostAttachments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_CategoryId",
                table: "PostCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_PostId",
                table: "PostCategories",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostGroupsAlloweds_GroupId",
                table: "PostGroupsAlloweds",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PostGroupsAlloweds_PostId",
                table: "PostGroupsAlloweds",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostKeywords_KeywordId",
                table: "PostKeywords",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_PostKeywords_PostSeoSettingsId",
                table: "PostKeywords",
                column: "PostSeoSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_PostOwners_PostId",
                table: "PostOwners",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostOwners_UserId",
                table: "PostOwners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostRatings_PostId",
                table: "PostRatings",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostRatings_UserId",
                table: "PostRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostSettings_PostTypeId",
                table: "PostSettings",
                column: "PostTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostSettings_PreviewImageId",
                table: "PostSettings",
                column: "PreviewImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUserAlloweds_PostId",
                table: "PostUserAlloweds",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUserAlloweds_UserId",
                table: "PostUserAlloweds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettingses_UserId",
                table: "UserSettingses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRights_RightId",
                table: "UsersRights",
                column: "RightId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRights_UserId",
                table: "UsersRights",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "GroupsRights");

            migrationBuilder.DropTable(
                name: "PostAttachments");

            migrationBuilder.DropTable(
                name: "PostCategories");

            migrationBuilder.DropTable(
                name: "PostGroupsAlloweds");

            migrationBuilder.DropTable(
                name: "PostKeywords");

            migrationBuilder.DropTable(
                name: "PostOwners");

            migrationBuilder.DropTable(
                name: "PostRatings");

            migrationBuilder.DropTable(
                name: "PostUserAlloweds");

            migrationBuilder.DropTable(
                name: "UserSettingses");

            migrationBuilder.DropTable(
                name: "UsersRights");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Rights");

            migrationBuilder.DropTable(
                name: "PostSeoSettings");

            migrationBuilder.DropTable(
                name: "PostSettings");

            migrationBuilder.DropTable(
                name: "PostTypes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "GroupTypes");
        }
    }
}
