IF NOT EXISTS(
  SELECT * FROM sys.databases WHERE name = 'Blog')
BEGIN
  CREATE DATABASE [Blog]
END
--SELECT * FROM sys.databases WHERE name = 'Blog'
GO

USE [Blog]
GO

--DROP TABLE [dbo].[Posts]
--GO

--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO

IF NOT EXISTS (
  SELECT * FROM sysobjects WHERE name='Posts' and xtype='U'
)
BEGIN
CREATE TABLE
  Posts (
    Id INT IDENTITY (1, 1) NOT NULL,
    Subject NVARCHAR(200),
    Text NVARCHAR(MAX),
    Created DATETIME,
    CONSTRAINT
      [PK_Posts]
      PRIMARY KEY CLUSTERED(
        [Id] ASC
      )
      WITH (
        PAD_INDEX = OFF, 
        STATISTICS_NORECOMPUTE = OFF, 
        IGNORE_DUP_KEY = OFF, 
        ALLOW_ROW_LOCKS = ON, 
        ALLOW_PAGE_LOCKS = ON
      )
      ON [PRIMARY]
  )
  ON [PRIMARY]
END
GO

ALTER TABLE Posts ADD CONSTRAINT DF_Posts_Created DEFAULT GETDATE() FOR Created
GO

--DROP TABLE [dbo].[Comments]
--GO

IF NOT EXISTS (
  SELECT * FROM sysobjects WHERE name='Comments' and xtype='U'
)
BEGIN
CREATE TABLE
  Comments (
    Id INT IDENTITY (1, 1) NOT NULL,
    Text NVARCHAR(MAX),
    Author NVARCHAR(200),
    PostId INT NOT NULL,
    Created DATETIME,
    CONSTRAINT
      [PK_Comments]
      PRIMARY KEY CLUSTERED(
        [Id] ASC
      )
      WITH (
        PAD_INDEX = OFF, 
        STATISTICS_NORECOMPUTE = OFF, 
        IGNORE_DUP_KEY = OFF, 
        ALLOW_ROW_LOCKS = ON, 
        ALLOW_PAGE_LOCKS = ON
      )
      ON [PRIMARY]
  )
  ON [PRIMARY]
END
GO

ALTER TABLE Comments ADD CONSTRAINT DF_Comments_Created DEFAULT GETDATE() FOR Created
GO

--GO
--ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_Posts]

ALTER TABLE 
  [dbo].[Comments]
WITH CHECK 
ADD 
  CONSTRAINT 
    [FK_Comments_Posts] 
  FOREIGN KEY(
    [PostId]
  )
  REFERENCES 
    [dbo].[Posts] ([Id])
GO

ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Posts]
GO

INSERT 
  [dbo].[Posts] ([Subject], [Text]) 
VALUES 
  (N'DB Created!', N'DB up and running')
GO

INSERT
  [dbo].[Comments] ([Text], [Author], [PostId]) VALUES (N'Finally, DB is ready', N'Software developer', 1)
GO