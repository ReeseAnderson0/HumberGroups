
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/13/2022 21:02:55
-- Generated from EDMX file: C:\Users\Reese\Desktop\GithubProjects\HumberGroups\HumberGroupsMain\HumberStudentGroup\ADO\HumberADO.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GroupUser_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupUser] DROP CONSTRAINT [FK_GroupUser_Group];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupUser] DROP CONSTRAINT [FK_GroupUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chats] DROP CONSTRAINT [FK_ChatGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageChat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_MessageChat];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_MessageUser];
GO
IF OBJECT_ID(N'[dbo].[FK_PostUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_PostUser];
GO
IF OBJECT_ID(N'[dbo].[FK_PostGroup_Post]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostGroup] DROP CONSTRAINT [FK_PostGroup_Post];
GO
IF OBJECT_ID(N'[dbo].[FK_PostGroup_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostGroup] DROP CONSTRAINT [FK_PostGroup_Group];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Chats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chats];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[GroupUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupUser];
GO
IF OBJECT_ID(N'[dbo].[PostGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostGroup];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Desc] nvarchar(max)  NOT NULL,
    [AuthorId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Chats'
CREATE TABLE [dbo].[Chats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Group_Id] int  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ChatId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [SentDate] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'GroupUser'
CREATE TABLE [dbo].[GroupUser] (
    [Groups_Id] int  NOT NULL,
    [Users_Id] int  NOT NULL
);
GO

-- Creating table 'PostGroup'
CREATE TABLE [dbo].[PostGroup] (
    [Posts_Id] int  NOT NULL,
    [Groups_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [PK_Chats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Groups_Id], [Users_Id] in table 'GroupUser'
ALTER TABLE [dbo].[GroupUser]
ADD CONSTRAINT [PK_GroupUser]
    PRIMARY KEY CLUSTERED ([Groups_Id], [Users_Id] ASC);
GO

-- Creating primary key on [Posts_Id], [Groups_Id] in table 'PostGroup'
ALTER TABLE [dbo].[PostGroup]
ADD CONSTRAINT [PK_PostGroup]
    PRIMARY KEY CLUSTERED ([Posts_Id], [Groups_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Groups_Id] in table 'GroupUser'
ALTER TABLE [dbo].[GroupUser]
ADD CONSTRAINT [FK_GroupUser_Group]
    FOREIGN KEY ([Groups_Id])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'GroupUser'
ALTER TABLE [dbo].[GroupUser]
ADD CONSTRAINT [FK_GroupUser_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupUser_User'
CREATE INDEX [IX_FK_GroupUser_User]
ON [dbo].[GroupUser]
    ([Users_Id]);
GO

-- Creating foreign key on [Group_Id] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [FK_ChatGroup]
    FOREIGN KEY ([Group_Id])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatGroup'
CREATE INDEX [IX_FK_ChatGroup]
ON [dbo].[Chats]
    ([Group_Id]);
GO

-- Creating foreign key on [ChatId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessageChat]
    FOREIGN KEY ([ChatId])
    REFERENCES [dbo].[Chats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageChat'
CREATE INDEX [IX_FK_MessageChat]
ON [dbo].[Messages]
    ([ChatId]);
GO

-- Creating foreign key on [UserId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessageUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageUser'
CREATE INDEX [IX_FK_MessageUser]
ON [dbo].[Messages]
    ([UserId]);
GO

-- Creating foreign key on [User_Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_PostUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostUser'
CREATE INDEX [IX_FK_PostUser]
ON [dbo].[Posts]
    ([User_Id]);
GO

-- Creating foreign key on [Posts_Id] in table 'PostGroup'
ALTER TABLE [dbo].[PostGroup]
ADD CONSTRAINT [FK_PostGroup_Post]
    FOREIGN KEY ([Posts_Id])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Groups_Id] in table 'PostGroup'
ALTER TABLE [dbo].[PostGroup]
ADD CONSTRAINT [FK_PostGroup_Group]
    FOREIGN KEY ([Groups_Id])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostGroup_Group'
CREATE INDEX [IX_FK_PostGroup_Group]
ON [dbo].[PostGroup]
    ([Groups_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
