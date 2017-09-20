using System;
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
                "Category",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Alias = table.Column<string>("text", nullable: false),
                    Description = table.Column<string>("text", nullable: true),
                    Name = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.UniqueConstraint("AK_Category_Alias", x => x.Alias);
                });

            migrationBuilder.CreateTable(
                "File",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    DateAdded = table.Column<DateTime>("timestamp", nullable: false),
                    Extension = table.Column<string>("text", nullable: true),
                    FileName = table.Column<string>("text", nullable: false),
                    FilePath = table.Column<string>("text", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_File", x => x.Id); });

            migrationBuilder.CreateTable(
                "GroupType",
                table => new
                {
                    Alias = table.Column<string>("text", nullable: false),
                    Description = table.Column<string>("text", nullable: true),
                    Name = table.Column<string>("text", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_GroupType", x => x.Alias); });

            migrationBuilder.CreateTable(
                "Keyword",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Alias = table.Column<string>("text", nullable: false),
                    Name = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.Id);
                    table.UniqueConstraint("AK_Keyword_Alias", x => x.Alias);
                });

            migrationBuilder.CreateTable(
                "PostSeoSetting",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Description = table.Column<string>("text", nullable: true),
                    Title = table.Column<string>("text", nullable: true),
                    Url = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_PostSeoSetting", x => x.Id); });

            migrationBuilder.CreateTable(
                "Right",
                table => new
                {
                    Alias = table.Column<string>("text", nullable: false),
                    Description = table.Column<string>("text", nullable: true),
                    Name = table.Column<string>("text", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Right", x => x.Alias); });

            migrationBuilder.CreateTable(
                "SiteSetting",
                table => new
                {
                    Key = table.Column<string>("text", nullable: false),
                    Value = table.Column<byte[]>("bytea", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_SiteSetting", x => x.Key); });

            migrationBuilder.CreateTable(
                "PostSetting",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    CanBeRated = table.Column<bool>("bool", nullable: false, defaultValue: false),
                    IsCommentsAllowed = table.Column<bool>("bool", nullable: false, defaultValue: false),
                    PostOnStartPage = table.Column<bool>("bool", nullable: false, defaultValue: false),
                    PreviewImageId = table.Column<Guid>("uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSetting", x => x.Id);
                    table.ForeignKey(
                        "FK_PostSetting_File_PreviewImageId",
                        x => x.PreviewImageId,
                        "File",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Group",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Alias = table.Column<string>("text", nullable: false),
                    Description = table.Column<string>("text", nullable: true),
                    GroupTypeAlias = table.Column<string>("text", nullable: false),
                    IsAdmin = table.Column<bool>("bool", nullable: false, defaultValue: false),
                    Name = table.Column<string>("text", nullable: false),
                    ParentGroupId = table.Column<Guid>("uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        "FK_Group_GroupType_GroupTypeAlias",
                        x => x.GroupTypeAlias,
                        "GroupType",
                        "Alias",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Group_Group_ParentGroupId",
                        x => x.ParentGroupId,
                        "Group",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PostKeyword",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    KeywordId = table.Column<Guid>("uuid", nullable: false),
                    PostSeoSettingsId = table.Column<Guid>("uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostKeyword", x => x.Id);
                    table.ForeignKey(
                        "FK_PostKeyword_Keyword_KeywordId",
                        x => x.KeywordId,
                        "Keyword",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PostKeyword_PostSeoSetting_PostSeoSettingsId",
                        x => x.PostSeoSettingsId,
                        "PostSeoSetting",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PostType",
                table => new
                {
                    Alias = table.Column<string>("text", nullable: false),
                    DefaultPostsSettingsId = table.Column<Guid>("uuid", nullable: false),
                    TypeName = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostType", x => x.Alias);
                    table.ForeignKey(
                        "FK_PostType_PostSetting_DefaultPostsSettingsId",
                        x => x.DefaultPostsSettingsId,
                        "PostSetting",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "GroupsRight",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Allowed = table.Column<bool>("bool", nullable: false),
                    GroupId = table.Column<Guid>("uuid", nullable: false),
                    RightAlias = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsRight", x => x.Id);
                    table.ForeignKey(
                        "FK_GroupsRight_Group_GroupId",
                        x => x.GroupId,
                        "Group",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_GroupsRight_Right_RightAlias",
                        x => x.RightAlias,
                        "Right",
                        "Alias",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "User",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    CreationDate = table.Column<DateTime>("timestamp", nullable: true, defaultValueSql: "NOW()"),
                    GroupId = table.Column<Guid>("uuid", nullable: false),
                    Login = table.Column<string>("text", nullable: false),
                    PasswordHash = table.Column<byte[]>("bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        "FK_User_Group_GroupId",
                        x => x.GroupId,
                        "Group",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Person",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    AdditionalPhone = table.Column<string>("text", nullable: true),
                    Birthday = table.Column<DateTime>("timestamp", nullable: false),
                    CreationDate = table.Column<DateTime>("timestamp", nullable: true, defaultValueSql: "NOW()"),
                    MiddleName = table.Column<string>("text", nullable: true),
                    Name = table.Column<string>("text", nullable: false),
                    Phone = table.Column<string>("text", nullable: true),
                    PhotoId = table.Column<Guid>("uuid", nullable: true),
                    Surname = table.Column<string>("text", nullable: false),
                    UserId = table.Column<Guid>("uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        "FK_Person_File_PhotoId",
                        x => x.PhotoId,
                        "File",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Person_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Post",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    AuthorId = table.Column<Guid>("uuid", nullable: false),
                    Content = table.Column<string>("text", nullable: false),
                    Deleted = table.Column<bool>("bool", nullable: false),
                    Excerpt = table.Column<string>("text", nullable: false),
                    PostSeoSettingsId = table.Column<Guid>("uuid", nullable: false),
                    PostSettingsId = table.Column<Guid>("uuid", nullable: true),
                    PostTypeAlias = table.Column<string>("text", nullable: false),
                    PublishDate = table.Column<DateTime>("timestamp", nullable: false),
                    Published = table.Column<bool>("bool", nullable: false),
                    Title = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        "FK_Post_User_AuthorId",
                        x => x.AuthorId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Post_PostSeoSetting_PostSeoSettingsId",
                        x => x.PostSeoSettingsId,
                        "PostSeoSetting",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Post_PostSetting_PostSettingsId",
                        x => x.PostSettingsId,
                        "PostSetting",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Post_PostType_PostTypeAlias",
                        x => x.PostTypeAlias,
                        "PostType",
                        "Alias",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserRights",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Allowed = table.Column<bool>("bool", nullable: false),
                    RightAlias = table.Column<string>("text", nullable: false),
                    UserId = table.Column<Guid>("uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRights", x => x.Id);
                    table.ForeignKey(
                        "FK_UserRights_Right_RightAlias",
                        x => x.RightAlias,
                        "Right",
                        "Alias",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UserRights_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserSetting",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Key = table.Column<string>("text", nullable: false),
                    Namespace = table.Column<string>("text", nullable: false),
                    UserId = table.Column<Guid>("uuid", nullable: false),
                    Value = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSetting", x => x.Id);
                    table.ForeignKey(
                        "FK_UserSetting_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Comment",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Date = table.Column<DateTime>("timestamp", nullable: false),
                    Edited = table.Column<bool>("bool", nullable: false),
                    PostId = table.Column<Guid>("uuid", nullable: false),
                    Text = table.Column<string>("text", nullable: false),
                    UserId = table.Column<Guid>("uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        "FK_Comment_Post_PostId",
                        x => x.PostId,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Comment_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PostAttachment",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Allowed = table.Column<bool>("bool", nullable: false),
                    FileId = table.Column<Guid>("uuid", nullable: false),
                    PostId = table.Column<Guid>("uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAttachment", x => x.Id);
                    table.ForeignKey(
                        "FK_PostAttachment_File_FileId",
                        x => x.FileId,
                        "File",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PostAttachment_Post_PostId",
                        x => x.PostId,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PostCategory",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    CategoryId = table.Column<Guid>("uuid", nullable: false),
                    PostId = table.Column<Guid>("uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategory", x => x.Id);
                    table.ForeignKey(
                        "FK_PostCategory_Category_CategoryId",
                        x => x.CategoryId,
                        "Category",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PostCategory_Post_PostId",
                        x => x.PostId,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PostGroupsAllowed",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Allowed = table.Column<bool>("bool", nullable: false),
                    GroupId = table.Column<Guid>("uuid", nullable: true),
                    PostId = table.Column<Guid>("uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGroupsAllowed", x => x.Id);
                    table.ForeignKey(
                        "FK_PostGroupsAllowed_Group_GroupId",
                        x => x.GroupId,
                        "Group",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PostGroupsAllowed_Post_PostId",
                        x => x.PostId,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PostOwner",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    PostId = table.Column<Guid>("uuid", nullable: false),
                    UserId = table.Column<Guid>("uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostOwner", x => x.Id);
                    table.ForeignKey(
                        "FK_PostOwner_Post_PostId",
                        x => x.PostId,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PostOwner_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PostRating",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Allowed = table.Column<bool>("bool", nullable: false),
                    PostId = table.Column<Guid>("uuid", nullable: false),
                    UserId = table.Column<Guid>("uuid", nullable: false),
                    Value = table.Column<bool>("bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostRating", x => x.Id);
                    table.ForeignKey(
                        "FK_PostRating_Post_PostId",
                        x => x.PostId,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PostRating_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PostUserAllowed",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Allowed = table.Column<bool>("bool", nullable: false),
                    PostId = table.Column<Guid>("uuid", nullable: false),
                    UserId = table.Column<Guid>("uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUserAllowed", x => x.Id);
                    table.ForeignKey(
                        "FK_PostUserAllowed_Post_PostId",
                        x => x.PostId,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PostUserAllowed_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Comment_PostId",
                "Comment",
                "PostId");

            migrationBuilder.CreateIndex(
                "IX_Comment_UserId",
                "Comment",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_Group_GroupTypeAlias",
                "Group",
                "GroupTypeAlias");

            migrationBuilder.CreateIndex(
                "IX_Group_ParentGroupId",
                "Group",
                "ParentGroupId");

            migrationBuilder.CreateIndex(
                "IX_GroupsRight_GroupId",
                "GroupsRight",
                "GroupId");

            migrationBuilder.CreateIndex(
                "IX_GroupsRight_RightAlias",
                "GroupsRight",
                "RightAlias");

            migrationBuilder.CreateIndex(
                "IX_Person_PhotoId",
                "Person",
                "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Person_UserId",
                "Person",
                "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Post_AuthorId",
                "Post",
                "AuthorId");

            migrationBuilder.CreateIndex(
                "IX_Post_PostSeoSettingsId",
                "Post",
                "PostSeoSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Post_PostSettingsId",
                "Post",
                "PostSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Post_PostTypeAlias",
                "Post",
                "PostTypeAlias");

            migrationBuilder.CreateIndex(
                "IX_PostAttachment_FileId",
                "PostAttachment",
                "FileId");

            migrationBuilder.CreateIndex(
                "IX_PostAttachment_PostId",
                "PostAttachment",
                "PostId");

            migrationBuilder.CreateIndex(
                "IX_PostCategory_CategoryId",
                "PostCategory",
                "CategoryId");

            migrationBuilder.CreateIndex(
                "IX_PostCategory_PostId",
                "PostCategory",
                "PostId");

            migrationBuilder.CreateIndex(
                "IX_PostGroupsAllowed_GroupId",
                "PostGroupsAllowed",
                "GroupId");

            migrationBuilder.CreateIndex(
                "IX_PostGroupsAllowed_PostId",
                "PostGroupsAllowed",
                "PostId");

            migrationBuilder.CreateIndex(
                "IX_PostKeyword_KeywordId",
                "PostKeyword",
                "KeywordId");

            migrationBuilder.CreateIndex(
                "IX_PostKeyword_PostSeoSettingsId",
                "PostKeyword",
                "PostSeoSettingsId");

            migrationBuilder.CreateIndex(
                "IX_PostOwner_PostId",
                "PostOwner",
                "PostId");

            migrationBuilder.CreateIndex(
                "IX_PostOwner_UserId",
                "PostOwner",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_PostRating_PostId",
                "PostRating",
                "PostId");

            migrationBuilder.CreateIndex(
                "IX_PostRating_UserId",
                "PostRating",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_PostSeoSetting_Url",
                "PostSeoSetting",
                "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_PostSetting_PreviewImageId",
                "PostSetting",
                "PreviewImageId");

            migrationBuilder.CreateIndex(
                "IX_PostType_DefaultPostsSettingsId",
                "PostType",
                "DefaultPostsSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_PostUserAllowed_PostId",
                "PostUserAllowed",
                "PostId");

            migrationBuilder.CreateIndex(
                "IX_PostUserAllowed_UserId",
                "PostUserAllowed",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_SiteSetting_Key",
                "SiteSetting",
                "Key");

            migrationBuilder.CreateIndex(
                "IX_User_GroupId",
                "User",
                "GroupId");

            migrationBuilder.CreateIndex(
                "IX_UserRights_RightAlias",
                "UserRights",
                "RightAlias");

            migrationBuilder.CreateIndex(
                "IX_UserRights_UserId",
                "UserRights",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_UserSetting_UserId",
                "UserSetting",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Comment");

            migrationBuilder.DropTable(
                "GroupsRight");

            migrationBuilder.DropTable(
                "Person");

            migrationBuilder.DropTable(
                "PostAttachment");

            migrationBuilder.DropTable(
                "PostCategory");

            migrationBuilder.DropTable(
                "PostGroupsAllowed");

            migrationBuilder.DropTable(
                "PostKeyword");

            migrationBuilder.DropTable(
                "PostOwner");

            migrationBuilder.DropTable(
                "PostRating");

            migrationBuilder.DropTable(
                "PostUserAllowed");

            migrationBuilder.DropTable(
                "SiteSetting");

            migrationBuilder.DropTable(
                "UserRights");

            migrationBuilder.DropTable(
                "UserSetting");

            migrationBuilder.DropTable(
                "Category");

            migrationBuilder.DropTable(
                "Keyword");

            migrationBuilder.DropTable(
                "Post");

            migrationBuilder.DropTable(
                "Right");

            migrationBuilder.DropTable(
                "User");

            migrationBuilder.DropTable(
                "PostSeoSetting");

            migrationBuilder.DropTable(
                "PostType");

            migrationBuilder.DropTable(
                "Group");

            migrationBuilder.DropTable(
                "PostSetting");

            migrationBuilder.DropTable(
                "GroupType");

            migrationBuilder.DropTable(
                "File");
        }
    }
}