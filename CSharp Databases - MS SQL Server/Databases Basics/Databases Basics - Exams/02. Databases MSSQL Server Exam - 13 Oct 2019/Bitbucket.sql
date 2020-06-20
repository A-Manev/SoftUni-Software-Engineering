-- Section 1. DDL (30 pts)

-- 1. Database Design

CREATE DATABASE [Bitbucket]

USE [Bitbucket]

CREATE TABLE Users(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Username] NVARCHAR(30) NOT NULL,
	[Password] NVARCHAR(30) NOT NULL,
	[Email] NVARCHAR(50) NOT NULL
) 

CREATE TABLE Repositories(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE RepositoriesContributors(
	[RepositoryId] INT FOREIGN KEY REFERENCES Repositories([Id]) NOT NULL,
	[ContributorId] INT FOREIGN KEY REFERENCES Users([Id]) NOT NULL,
	PRIMARY KEY ([RepositoryId], [ContributorId])
)

CREATE TABLE Issues(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Title] NVARCHAR(255) NOT NULL,
	[IssueStatus] NVARCHAR(6) NOT NULL,
		-- CHECK (LEN([IssueStatus]) = 6),
	[RepositoryId] INT FOREIGN KEY REFERENCES Repositories([Id]) NOT NULL,
	[AssigneeId] INT FOREIGN KEY REFERENCES Users([Id]) NOT NULL
) 

CREATE TABLE Commits(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Message] NVARCHAR(255) NOT NULL,
	[IssueId] INT FOREIGN KEY REFERENCES Issues([Id]),
	[RepositoryId] INT FOREIGN KEY REFERENCES Repositories([Id]) NOT NULL,
	[ContributorId] INT FOREIGN KEY REFERENCES Users([Id]) NOT NULL
)

CREATE TABLE Files(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[Size] DECIMAL(18,2) NOT NULL,
	[ParentId] INT FOREIGN KEY REFERENCES Files([Id]),
	[CommitId] INT FOREIGN KEY REFERENCES Commits([Id]) NOT NULL
)


-- Section 2. DML (10 pts)

-- 2. Insert

INSERT INTO Files([Name], [Size], [ParentId], [CommitId])
	VALUES 
		('Trade.idk', 2598.0, 1, 1),
		('menu.net', 9238.31, 2, 2),
		('Administrate.soshy', 1246.93, 3, 3),
		('Controller.php', 7353.15, 4, 4),
		('Find.java', 9957.86, 5, 5),
		('Controller.json', 14034.87, 3, 6),
		('Operate.xix', 7662.92, 7, 7)

INSERT INTO Issues([Title], [IssueStatus], [RepositoryId], [AssigneeId])
	VALUES
		('Critical Problem with HomeController.cs file', 'open', 1, 4),
		('Typo fix in Judge.html', 'open', 4, 3),
		('Implement documentation for UsersService.cs', 'closed', 8, 2),
		('Unreachable code in Index.cs', 'open', 9, 8)


-- 3. Update

UPDATE Issues SET [IssueStatus] = 'closed' WHERE [AssigneeId] = 6


-- 4. Delete

DELETE FROM Issues WHERE RepositoryId = 3
DELETE FROM RepositoriesContributors  WHERE RepositoryId = 3 


-- Section 3. Querying (40 pts)

-- 5. Commits

SELECT [Id], [Message], [RepositoryId], [ContributorId] FROM Commits
	ORDER BY [Id] ASC,
	         [Message] ASC,
			 [RepositoryId] ASC,
			 [ContributorId] ASC


-- 6.	Heavy HTML

SELECT [Id], [Name], [Size] FROM Files
	WHERE [Size] > 1000 AND [Name] LIKE '%.html'
	ORDER BY [Size] DESC,
			 [Id] ASC,
			 [Name] ASC


-- 7. Issues and Users

SELECT Issues.Id,
	   CONCAT(Users.Username, ' : ', Issues.Title ) AS [IssueAssignee]  
	FROM Issues
	JOIN Users ON Issues.AssigneeId = Users.Id
	ORDER BY Issues.Id DESC,
	         [IssueAssignee] ASC


-- 8. Non-Directory Files

SELECT f1.Id,
       f1.Name,
	   CONCAT(f1.Size, 'KB') AS [Size] 
	FROM Files AS f1
	LEFT JOIN Files AS f2
	ON f1.Id = f2.ParentId
	WHERE f2.ParentId IS NULL
	ORDER BY f1.Id ASC, f1.Name ASC, [Size] DESC


-- 9. Most Contributed Repositories

SELECT TOP(5)
		Repositories.Id,
		Repositories.Name,
		COUNT(*) AS [Commits] 
	FROM Repositories
	JOIN RepositoriesContributors
	ON Repositories.Id = RepositoriesContributors.RepositoryId
	JOIN Users
	ON RepositoriesContributors.ContributorId = Users.Id
	JOIN Commits
	ON Repositories.Id = Commits.RepositoryId
		GROUP BY Repositories.Name,
				 Repositories.Id
		ORDER BY [Commits] DESC,
				 Repositories.Id ASC,
				 Repositories.Name 


-- 10. User and Files

SELECT Username,
	   AVG(Size) AS [Size] 
	FROM Commits
	JOIN Users 
	ON Commits.ContributorId = Users.Id
	JOIN Files 
	ON Files.CommitId = Commits.Id
	GROUP BY Users.Username
	ORDER BY [Size] DESC,
			 [Username] ASC


-- Section 4. Programmability (20 pts)

-- 11. User Total Commits

GO
CREATE FUNCTION udf_UserTotalCommits(@username NVARCHAR(50)) 
RETURNS INT
AS 
BEGIN  
	DECLARE @Count INT = 0 

	SET @Count = (SELECT COUNT(*) FROM  Users 
	JOIN Commits ON Users.Id = Commits.ContributorId
	WHERE [Username] = @username)

	 RETURN @Count
END
GO;


-- 12. Find by Extensions

GO
CREATE PROCEDURE usp_FindByExtension(@extension NVARCHAR(50))
AS 
BEGIN 
	SELECT [Id], [Name], CONCAT([Size],'KB') AS [Size] 
	FROM Files 
	WHERE [Name] LIKE '%' + '.' + @extension
	ORDER BY [Id] ASC,
			 [Name] ASC,
			 [Size] DESC
END
GO;