-- Section 1. DDL (30 pts)

CREATE DATABASE [Service]

USE [Service]

CREATE TABLE Users(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Username] NVARCHAR(30) UNIQUE NOT NULL,
	[Password] NVARCHAR(50) NOT NULL,
	[Name] NVARCHAR(50),
	[Birthdate] DATETIME2,
	[Age] INT,
		CHECK ([Age] BETWEEN 14 AND 110),
	[Email] NVARCHAR(50) NOT NULL
)

CREATE TABLE Departments(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[FirstName] NVARCHAR(25),
	[LastName] NVARCHAR(25),
	[Birthdate] DATETIME2,
	[Age] INT,
		CHECK ([Age] BETWEEN 18 AND 110),
	[DepartmentId] INT FOREIGN KEY REFERENCES Departments([Id])
)

CREATE TABLE Categories(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[DepartmentId] INT FOREIGN KEY REFERENCES Departments([Id]) NOT NULL
)

CREATE TABLE [Status](
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Label] NVARCHAR(30) NOT NULL
)

CREATE TABLE Reports(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[CategoryId] INT FOREIGN KEY REFERENCES Categories([Id]) NOT NULL,
	[StatusId] INT FOREIGN KEY REFERENCES [Status]([Id]),
	[OpenDate] DATETIME2 NOT NULL,
	[CloseDate]  DATETIME2,
	[Description] NVARCHAR(200) NOT NULL,
	[UserId] INT FOREIGN KEY REFERENCES Users([Id]),
	[EmployeeId] INT FOREIGN KEY REFERENCES Employees([Id])
)

-- Section 2. DML (10 pts)

INSERT INTO Employees([FirstName], [LastName], [Birthdate],[DepartmentId])
	VALUES
		  ('Marlo', 'O''Malley', '1958-9-21', 1),
		  ('Niki', 'Stanaghan', '1969-11-26', 4),
		  ('Ayrton', 'Senna', '1960-03-21', 9),
		  ('Ronnie', 'Peterson', '1944-02-14', 9),
		  ('Giovanna', 'Amati', '1959-07-20', 5)

INSERT INTO Reports([CategoryId], [StatusId], [OpenDate], [CloseDate], [Description], [UserId], [EmployeeId])
	VALUES
		  (1, 1, '2017-04-13', NULL ,'Stuck Road on Str.133', 6, 2),
		  (6, 3, '2015-09-05', '2015-12-06','Charity trail running', 3, 5),
		  (14, 2, '2015-09-07', NULL ,'Falling bricks on Str.58', 5, 2),
		  (4, 3, '2017-07-03', '2017-07-06','Cut off streetlight on Str.11', 1, 1)

UPDATE Reports
	SET [CloseDate] =  GETDATE()
		WHERE [CloseDate] IS NULL

DELETE FROM Reports WHERE [StatusId] = 4


-- Section 3. Querying (40 pts)

-- 5. Unassigned Reports
SELECT [Description], FORMAT([OpenDate], 'dd-MM-yyyy') FROM Reports
	WHERE [EmployeeId] IS NULL
		ORDER BY [OpenDate] ASC, 
		         [Description] ASC


-- 6. Reports & Categories
SELECT Reports.[Description], Categories.[Name]
	FROM Reports
	JOIN Categories ON Categories.[Id] = Reports.[CategoryId]
	ORDER BY [Description] ASC,
	Categories.[Name] ASC


-- 7. Most Reported Category

SELECT TOP(5) Categories.Name, 
	COUNT(CategoryId) AS ReportsNumber 
	FROM Reports 
    INNER JOIN Categories ON Reports.CategoryId = Categories.Id
	GROUP BY Categories.Name
	HAVING COUNT(CategoryId) > 1
    ORDER BY COUNT(CategoryId) DESC, 
	               Categories.Name ASC


-- 8. Birthday Report

SELECT Users.Username,
	   Categories.[Name]
	FROM Users
	JOIN Reports ON Users.Id = Reports.UserId
	JOIN Categories ON Reports.CategoryId = Categories.Id
	WHERE FORMAT(Users.Birthdate, 'dd-MM') = FORMAT(Reports.OpenDate, 'dd-MM')
	ORDER BY Users.Username ASC,
			 Categories.[Name] ASC


-- 9. Users per Employee 

SELECT CONCAT(Employees.FirstName, ' ', Employees.LastName) AS [FullName],
	   COUNT(Users.Id) AS [UsersCount]
	FROM Employees
	LEFT JOIN Reports ON Employees.Id = Reports.EmployeeId
	LEFT JOIN Users ON Reports.UserId = Users.Id
	GROUP BY Employees.FirstName,
			 Employees.LastName
	ORDER BY [UsersCount] DESC,
			 [FullName] ASC


-- 10.	Full Info

SELECT CASE 
			WHEN Employees.FirstName IS NULL THEN 'None'
			ELSE CONCAT(Employees.FirstName, ' ', Employees.LastName) 
	   END AS [Employee],
       CASE 
       		WHEN Departments.[Name] IS NULL THEN 'None'
       		ELSE Departments.[Name]
       END AS [Department],
	   Categories.[Name] AS [Category],
	   Reports.[Description],
	   FORMAT(Reports.OpenDate, 'dd.MM.yyyy') AS [OpenDate],
	   [Status].[Label] AS [Status],
	   Users.[Name] AS [User]
	FROM Reports
	LEFT JOIN Employees ON Reports.EmployeeId = Employees.Id
	LEFT JOIN Users ON Reports.UserId = Users.Id
	LEFT JOIN Categories ON Reports.CategoryId = Categories.Id
	LEFT JOIN Status ON Reports.StatusId = Status.Id
	LEFT JOIN Departments ON Employees.DepartmentId = Departments.Id
	ORDER BY Employees.FirstName DESC,
				 Employees.LastName DESC,
				 Departments.[Name],
				 Categories.[Name],
				 Reports.[Description],
				 Reports.OpenDate,
				 [Status].[Label],
				 Users.[Name]

-- Section 4. Programmability (20 pts)

-- 11.	Hours to Complete

GO
CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME)
RETURNS INT
AS
BEGIN
	IF (@StartDate IS NULL OR @EndDate IS NULL)
		BEGIN
		RETURN 0;
		END

	RETURN DATEDIFF(HOUR, @StartDate, @EndDate)
END
GO;


-- 12. Assign Employee

CREATE PROC usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
BEGIN
      DECLARE @employeeDepartmentId INT = (SELECT DepartmentId
                                          FROM Employees
										 WHERE Id = @EmployeeId)
   
   DECLARE @departmentId INT = (SELECT c.DepartmentId
                                  FROM Reports AS r
								  JOIN Categories AS c ON c.Id = r.CategoryId
								 WHERE r.Id = @ReportId) 
                                       
BEGIN TRANSACTION

   IF(@employeeDepartmentId <> @departmentId)
   BEGIN
		   ROLLBACK
		   RAISERROR ('Employee doesn''t belong to the appropriate department!', 16, 1) 
		   RETURN
     END

	 UPDATE Reports
	    SET EmployeeId = @EmployeeId
	  WHERE Id = @ReportId
COMMIT
END