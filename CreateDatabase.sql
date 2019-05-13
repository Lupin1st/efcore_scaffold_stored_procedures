CREATE DATABASE [Blogging];
GO

USE [Blogging];
GO

CREATE TABLE [Blog] (
    [BlogId] int NOT NULL IDENTITY,
    [Url] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Blog] PRIMARY KEY ([BlogId])
);
GO

CREATE TABLE [Post] (
    [PostId] int NOT NULL IDENTITY,
    [BlogId] int NOT NULL,
    [Content] nvarchar(max),
    [Title] nvarchar(max),
    CONSTRAINT [PK_Post] PRIMARY KEY ([PostId]),
    CONSTRAINT [FK_Post_Blog_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [Blog] ([BlogId]) ON DELETE CASCADE
);
GO

CREATE PROCEDURE [dbo].SP_GET_POST_URLS
(
	@Top			int,
	@OverallCount	INT OUTPUT
)

AS
BEGIN
	/*
        DECLARE @OverallCount AS INT = 0;
        exec [SP_GET_POST_URLS] 2, @OverallCount OUTPUT
        print @OverallCount
	*/

    SET @OverallCount = (SELECT COUNT(*) FROM Post)
	SELECT TOP (@Top) CONCAT (Blog.Url, '/' , Title) AS Url from Post JOIN Blog ON Post.BlogId = Blog.BlogId
END