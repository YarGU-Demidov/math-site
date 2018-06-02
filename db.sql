CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "Category" (
        "Id" uuid NOT NULL,
        "Alias" text NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Description" text NULL,
        "Name" text NOT NULL,
        CONSTRAINT "PK_Category" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "File" (
        "Id" uuid NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "DateAdded" timestamp NOT NULL,
        "Extension" text NULL,
        "Name" text NOT NULL,
        "Path" text NOT NULL,
        CONSTRAINT "PK_File" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "GroupType" (
        "Id" uuid NOT NULL,
        "Alias" text NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Description" text NULL,
        "Name" text NOT NULL,
        CONSTRAINT "PK_GroupType" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "Keyword" (
        "Id" uuid NOT NULL,
        "Alias" text NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Name" text NOT NULL,
        CONSTRAINT "PK_Keyword" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "PostSeoSetting" (
        "Id" uuid NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Description" text NULL,
        "Title" text NULL,
        "Url" text NULL,
        CONSTRAINT "PK_PostSeoSetting" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "Right" (
        "Id" uuid NOT NULL,
        "Alias" text NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Description" text NULL,
        "Name" text NOT NULL,
        CONSTRAINT "PK_Right" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "SiteSetting" (
        "Id" uuid NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Key" text NOT NULL,
        "Value" bytea NOT NULL,
        CONSTRAINT "PK_SiteSetting" PRIMARY KEY ("Id"),
        CONSTRAINT "AK_SiteSetting_Key" UNIQUE ("Key")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "PostSetting" (
        "Id" uuid NOT NULL,
        "CanBeRated" bool NOT NULL DEFAULT FALSE,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "IsCommentsAllowed" bool NOT NULL DEFAULT FALSE,
        "Layout" text NOT NULL DEFAULT 'SecondaryLayout',
        "PostOnStartPage" bool NOT NULL DEFAULT FALSE,
        "PreviewImageId" uuid NULL,
        CONSTRAINT "PK_PostSetting" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PostSetting_File_PreviewImageId" FOREIGN KEY ("PreviewImageId") REFERENCES "File" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "Group" (
        "Id" uuid NOT NULL,
        "Alias" text NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Description" text NULL,
        "GroupTypeId" uuid NOT NULL,
        "IsAdmin" bool NOT NULL DEFAULT FALSE,
        "Name" text NOT NULL,
        "ParentGroupId" uuid NULL,
        CONSTRAINT "PK_Group" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Group_GroupType_GroupTypeId" FOREIGN KEY ("GroupTypeId") REFERENCES "GroupType" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Group_Group_ParentGroupId" FOREIGN KEY ("ParentGroupId") REFERENCES "Group" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "PostKeyword" (
        "Id" uuid NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "KeywordId" uuid NOT NULL,
        "PostSeoSettingsId" uuid NOT NULL,
        CONSTRAINT "PK_PostKeyword" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PostKeyword_Keyword_KeywordId" FOREIGN KEY ("KeywordId") REFERENCES "Keyword" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_PostKeyword_PostSeoSetting_PostSeoSettingsId" FOREIGN KEY ("PostSeoSettingsId") REFERENCES "PostSeoSetting" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "PostType" (
        "Id" uuid NOT NULL,
        "Alias" text NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "DefaultPostsSettingsId" uuid NOT NULL,
        "Name" text NOT NULL,
        CONSTRAINT "PK_PostType" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PostType_PostSetting_DefaultPostsSettingsId" FOREIGN KEY ("DefaultPostsSettingsId") REFERENCES "PostSetting" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "GroupsRight" (
        "Id" uuid NOT NULL,
        "Allowed" bool NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "GroupId" uuid NOT NULL,
        "RightId" uuid NOT NULL,
        CONSTRAINT "PK_GroupsRight" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_GroupsRight_Group_GroupId" FOREIGN KEY ("GroupId") REFERENCES "Group" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_GroupsRight_Right_RightId" FOREIGN KEY ("RightId") REFERENCES "Right" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "User" (
        "Id" uuid NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "GroupId" uuid NOT NULL,
        "Login" text NOT NULL,
        "PasswordHash" bytea NOT NULL,
        CONSTRAINT "PK_User" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_User_Group_GroupId" FOREIGN KEY ("GroupId") REFERENCES "Group" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "Person" (
        "Id" uuid NOT NULL,
        "AdditionalPhone" text NULL,
        "Birthday" timestamp NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "MiddleName" text NULL,
        "Name" text NOT NULL,
        "Phone" text NULL,
        "PhotoId" uuid NULL,
        "Surname" text NOT NULL,
        "UserId" uuid NULL,
        CONSTRAINT "PK_Person" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Person_File_PhotoId" FOREIGN KEY ("PhotoId") REFERENCES "File" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Person_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "Post" (
        "Id" uuid NOT NULL,
        "AuthorId" uuid NOT NULL,
        "Content" text NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Deleted" bool NOT NULL,
        "Excerpt" text NOT NULL,
        "PostSeoSettingsId" uuid NOT NULL,
        "PostSettingsId" uuid NULL,
        "PostTypeId" uuid NOT NULL,
        "PublishDate" timestamp NOT NULL,
        "Published" bool NOT NULL,
        "Title" text NOT NULL,
        CONSTRAINT "PK_Post" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Post_User_AuthorId" FOREIGN KEY ("AuthorId") REFERENCES "User" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Post_PostSeoSetting_PostSeoSettingsId" FOREIGN KEY ("PostSeoSettingsId") REFERENCES "PostSeoSetting" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Post_PostSetting_PostSettingsId" FOREIGN KEY ("PostSettingsId") REFERENCES "PostSetting" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Post_PostType_PostTypeId" FOREIGN KEY ("PostTypeId") REFERENCES "PostType" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "UserRights" (
        "Id" uuid NOT NULL,
        "Allowed" bool NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "RightId" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        CONSTRAINT "PK_UserRights" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_UserRights_Right_RightId" FOREIGN KEY ("RightId") REFERENCES "Right" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_UserRights_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "UserSetting" (
        "Id" uuid NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Key" text NOT NULL,
        "Namespace" text NOT NULL,
        "UserId" uuid NOT NULL,
        "Value" text NOT NULL,
        CONSTRAINT "PK_UserSetting" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_UserSetting_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "Comment" (
        "Id" uuid NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Date" timestamp NOT NULL,
        "Edited" bool NOT NULL,
        "PostId" uuid NOT NULL,
        "Text" text NOT NULL,
        "UserId" uuid NOT NULL,
        CONSTRAINT "PK_Comment" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Comment_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Comment_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "PostAttachment" (
        "Id" uuid NOT NULL,
        "Allowed" bool NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "FileId" uuid NOT NULL,
        "PostId" uuid NOT NULL,
        CONSTRAINT "PK_PostAttachment" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PostAttachment_File_FileId" FOREIGN KEY ("FileId") REFERENCES "File" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_PostAttachment_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "PostCategory" (
        "Id" uuid NOT NULL,
        "CategoryId" uuid NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "PostId" uuid NOT NULL,
        CONSTRAINT "PK_PostCategory" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PostCategory_Category_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Category" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_PostCategory_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "PostGroupsAllowed" (
        "Id" uuid NOT NULL,
        "Allowed" bool NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "GroupId" uuid NULL,
        "PostId" uuid NULL,
        CONSTRAINT "PK_PostGroupsAllowed" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PostGroupsAllowed_Group_GroupId" FOREIGN KEY ("GroupId") REFERENCES "Group" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_PostGroupsAllowed_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "PostOwner" (
        "Id" uuid NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "PostId" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        CONSTRAINT "PK_PostOwner" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PostOwner_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_PostOwner_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "PostRating" (
        "Id" uuid NOT NULL,
        "Allowed" bool NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "PostId" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        "Value" bool NOT NULL,
        CONSTRAINT "PK_PostRating" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PostRating_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_PostRating_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE TABLE "PostUserAllowed" (
        "Id" uuid NOT NULL,
        "Allowed" bool NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "PostId" uuid NOT NULL,
        "UserId" uuid NOT NULL,
        CONSTRAINT "PK_PostUserAllowed" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PostUserAllowed_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_PostUserAllowed_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_Category_Alias" ON "Category" ("Alias");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_Comment_PostId" ON "Comment" ("PostId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_Comment_UserId" ON "Comment" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_Group_Alias" ON "Group" ("Alias");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_Group_GroupTypeId" ON "Group" ("GroupTypeId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_Group_ParentGroupId" ON "Group" ("ParentGroupId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_GroupsRight_GroupId" ON "GroupsRight" ("GroupId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_GroupsRight_RightId" ON "GroupsRight" ("RightId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_GroupType_Alias" ON "GroupType" ("Alias");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_Keyword_Alias" ON "Keyword" ("Alias");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_Person_PhotoId" ON "Person" ("PhotoId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_Person_UserId" ON "Person" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_Post_AuthorId" ON "Post" ("AuthorId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_Post_PostSeoSettingsId" ON "Post" ("PostSeoSettingsId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_Post_PostSettingsId" ON "Post" ("PostSettingsId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_Post_PostTypeId" ON "Post" ("PostTypeId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostAttachment_FileId" ON "PostAttachment" ("FileId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostAttachment_PostId" ON "PostAttachment" ("PostId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostCategory_CategoryId" ON "PostCategory" ("CategoryId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostCategory_PostId" ON "PostCategory" ("PostId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostGroupsAllowed_GroupId" ON "PostGroupsAllowed" ("GroupId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostGroupsAllowed_PostId" ON "PostGroupsAllowed" ("PostId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostKeyword_KeywordId" ON "PostKeyword" ("KeywordId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostKeyword_PostSeoSettingsId" ON "PostKeyword" ("PostSeoSettingsId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostOwner_PostId" ON "PostOwner" ("PostId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostOwner_UserId" ON "PostOwner" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostRating_PostId" ON "PostRating" ("PostId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostRating_UserId" ON "PostRating" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostSeoSetting_Url" ON "PostSeoSetting" ("Url");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostSetting_PreviewImageId" ON "PostSetting" ("PreviewImageId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_PostType_Alias" ON "PostType" ("Alias");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_PostType_DefaultPostsSettingsId" ON "PostType" ("DefaultPostsSettingsId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostUserAllowed_PostId" ON "PostUserAllowed" ("PostId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_PostUserAllowed_UserId" ON "PostUserAllowed" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_Right_Alias" ON "Right" ("Alias");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE UNIQUE INDEX "IX_SiteSetting_Key" ON "SiteSetting" ("Key");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_User_GroupId" ON "User" ("GroupId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_UserRights_RightId" ON "UserRights" ("RightId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_UserRights_UserId" ON "UserRights" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    CREATE INDEX "IX_UserSetting_UserId" ON "UserSetting" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20170926004454_Initial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20170926004454_Initial', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171118205629_AddDirectory') THEN
    ALTER TABLE "File" ADD "DirectoryId" uuid NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171118205629_AddDirectory') THEN
    CREATE TABLE "Directory" (
        "Id" uuid NOT NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Name" text NULL,
        "RootDirectoryId" uuid NULL,
        CONSTRAINT "PK_Directory" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Directory_Directory_RootDirectoryId" FOREIGN KEY ("RootDirectoryId") REFERENCES "Directory" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171118205629_AddDirectory') THEN
    CREATE INDEX "IX_File_DirectoryId" ON "File" ("DirectoryId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171118205629_AddDirectory') THEN
    CREATE INDEX "IX_Directory_RootDirectoryId" ON "Directory" ("RootDirectoryId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171118205629_AddDirectory') THEN
    ALTER TABLE "File" ADD CONSTRAINT "FK_File_Directory_DirectoryId" FOREIGN KEY ("DirectoryId") REFERENCES "Directory" ("Id") ON DELETE CASCADE;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171118205629_AddDirectory') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20171118205629_AddDirectory', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171125133126_AddHashToFiles') THEN
    ALTER TABLE "File" ADD "Hash" text NOT NULL DEFAULT '';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171125133126_AddHashToFiles') THEN
    CREATE INDEX "IX_File_Hash" ON "File" ("Hash");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171125133126_AddHashToFiles') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20171125133126_AddHashToFiles', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171129011851_RemoveDeprecatedFieldFromFileEntity') THEN
    ALTER TABLE "File" DROP COLUMN "DateAdded";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171129011851_RemoveDeprecatedFieldFromFileEntity') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20171129011851_RemoveDeprecatedFieldFromFileEntity', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171225192144_AddEventTime_RemoveDefaultLayout') THEN
    ALTER TABLE "PostSetting" ALTER COLUMN "Layout" TYPE text;
    ALTER TABLE "PostSetting" ALTER COLUMN "Layout" SET NOT NULL;
    ALTER TABLE "PostSetting" ALTER COLUMN "Layout" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171225192144_AddEventTime_RemoveDefaultLayout') THEN
    ALTER TABLE "PostSetting" ADD "EventTime" timestamp NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171225192144_AddEventTime_RemoveDefaultLayout') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20171225192144_AddEventTime_RemoveDefaultLayout', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171225204242_RemoveRequiredForLayoutSetting') THEN
    ALTER TABLE "PostSetting" ALTER COLUMN "Layout" TYPE text;
    ALTER TABLE "PostSetting" ALTER COLUMN "Layout" DROP NOT NULL;
    ALTER TABLE "PostSetting" ALTER COLUMN "Layout" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171225204242_RemoveRequiredForLayoutSetting') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20171225204242_RemoveRequiredForLayoutSetting', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171225211729_AddEventLocation') THEN
    ALTER TABLE "PostSetting" ADD "EventLocation" text NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20171225211729_AddEventLocation') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20171225211729_AddEventLocation', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180221213004_Update-User-And-Person-Relations') THEN
    ALTER TABLE "Person" DROP CONSTRAINT "FK_Person_User_UserId";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180221213004_Update-User-And-Person-Relations') THEN
    DROP INDEX "IX_Person_UserId";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180221213004_Update-User-And-Person-Relations') THEN
    ALTER TABLE "Person" DROP COLUMN "UserId";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180221213004_Update-User-And-Person-Relations') THEN
    ALTER TABLE "User" ADD "PersonId" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180221213004_Update-User-And-Person-Relations') THEN
    CREATE UNIQUE INDEX "IX_User_PersonId" ON "User" ("PersonId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180221213004_Update-User-And-Person-Relations') THEN
    ALTER TABLE "User" ADD CONSTRAINT "FK_User_Person_PersonId" FOREIGN KEY ("PersonId") REFERENCES "Person" ("Id") ON DELETE SET NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180221213004_Update-User-And-Person-Relations') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20180221213004_Update-User-And-Person-Relations', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180301215907_RemoveDefaults') THEN
    ALTER TABLE "PostSetting" ALTER COLUMN "PostOnStartPage" TYPE bool;
    ALTER TABLE "PostSetting" ALTER COLUMN "PostOnStartPage" SET NOT NULL;
    ALTER TABLE "PostSetting" ALTER COLUMN "PostOnStartPage" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180301215907_RemoveDefaults') THEN
    ALTER TABLE "PostSetting" ALTER COLUMN "IsCommentsAllowed" TYPE bool;
    ALTER TABLE "PostSetting" ALTER COLUMN "IsCommentsAllowed" SET NOT NULL;
    ALTER TABLE "PostSetting" ALTER COLUMN "IsCommentsAllowed" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180301215907_RemoveDefaults') THEN
    ALTER TABLE "PostSetting" ALTER COLUMN "CanBeRated" TYPE bool;
    ALTER TABLE "PostSetting" ALTER COLUMN "CanBeRated" SET NOT NULL;
    ALTER TABLE "PostSetting" ALTER COLUMN "CanBeRated" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180301215907_RemoveDefaults') THEN
    ALTER TABLE "Group" ALTER COLUMN "IsAdmin" TYPE bool;
    ALTER TABLE "Group" ALTER COLUMN "IsAdmin" SET NOT NULL;
    ALTER TABLE "Group" ALTER COLUMN "IsAdmin" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180301215907_RemoveDefaults') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20180301215907_RemoveDefaults', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180420222556_AddBaseProfessorInfo') THEN
    CREATE TABLE "Professor" (
        "Id" uuid NOT NULL,
        "BibliographicIndexOfWorks" text[] NULL,
        "CreationDate" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "Description" text NOT NULL,
        "Graduated" text[] NULL,
        "MathNetLink" text NULL,
        "PersonId" uuid NOT NULL,
        "ScientificTitle" text NULL,
        "Status" text NOT NULL,
        "TermPapers" text[] NULL,
        "Theses" text[] NULL,
        CONSTRAINT "PK_Professor" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Professor_Person_PersonId" FOREIGN KEY ("PersonId") REFERENCES "Person" ("Id") ON DELETE SET NULL
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180420222556_AddBaseProfessorInfo') THEN
    CREATE UNIQUE INDEX "IX_Professor_PersonId" ON "Professor" ("PersonId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180420222556_AddBaseProfessorInfo') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20180420222556_AddBaseProfessorInfo', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180422182800_ExtendProfessorsInfo') THEN
    ALTER TABLE "Professor" ADD "Department" text NOT NULL DEFAULT '';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180422182800_ExtendProfessorsInfo') THEN
    ALTER TABLE "Professor" ADD "Faculty" text NOT NULL DEFAULT '';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180422182800_ExtendProfessorsInfo') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20180422182800_ExtendProfessorsInfo', '2.0.2-rtm-10011');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180601214220_RemoveRequiredFieldFromProfessor') THEN
    ALTER TABLE "Professor" ALTER COLUMN "Status" TYPE text;
    ALTER TABLE "Professor" ALTER COLUMN "Status" DROP NOT NULL;
    ALTER TABLE "Professor" ALTER COLUMN "Status" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180601214220_RemoveRequiredFieldFromProfessor') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20180601214220_RemoveRequiredFieldFromProfessor', '2.0.2-rtm-10011');
    END IF;
END $$;
