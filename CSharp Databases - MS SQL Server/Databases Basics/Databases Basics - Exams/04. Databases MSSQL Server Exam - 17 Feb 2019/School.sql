-- Section 1. DDL (30 pts)

-- 1. Database Design

CREATE DATABASE School

USE School

CREATE TABLE Students(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[FirstName] NVARCHAR(30) NOT NULL,
	[MiddleName] NVARCHAR(25),
	[LastName] NVARCHAR(30) NOT NULL,
	[Age] INT, CHECK ([Age] BETWEEN 5 AND 100),
	[Address] NVARCHAR(50),
	[Phone]  NVARCHAR(10)
)

CREATE TABLE Subjects(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(20) NOT NULL,
	[Lessons] INT NOT NULL, CHECK ([Lessons] > 0) 
)

CREATE TABLE StudentsSubjects(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[StudentId] INT FOREIGN KEY REFERENCES Students([Id]) NOT NULL,
	[SubjectId] INT FOREIGN KEY REFERENCES Subjects([Id]) NOT NULL,
	[Grade] DECIMAL(3,2) NOT NULL, CHECK ([Grade] BETWEEN 2 AND 6)
)

CREATE TABLE Exams(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Date] DATETIME2,
	[SubjectId] INT FOREIGN KEY REFERENCES Subjects([Id]) NOT NULL
)

CREATE TABLE StudentsExams(
	[StudentId] INT FOREIGN KEY REFERENCES Students([Id]) NOT NULL,
	[ExamId] INT FOREIGN KEY REFERENCES Exams([Id]) NOT NULL,
	[Grade] DECIMAL(3,2) NOT NULL, CHECK ([Grade] BETWEEN 2 AND 6),
	PRIMARY KEY([StudentId], [ExamId])
)

CREATE TABLE Teachers(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[FirstName] NVARCHAR(20) NOT NULL,
	[LastName] NVARCHAR(20) NOT NULL,
	[Address] NVARCHAR(20) NOT NULL,
	[Phone] NVARCHAR(10),
	[SubjectId] INT FOREIGN KEY REFERENCES Subjects([Id]) NOT NULL
)

CREATE TABLE StudentsTeachers(
	[StudentId] INT FOREIGN KEY REFERENCES Students([Id]) NOT NULL,
	[TeacherId] INT FOREIGN KEY REFERENCES Teachers([Id]),
	PRIMARY KEY([StudentId], [TeacherId])
)

-- Section 2. DML (10 pts)

-- 2. Insert

INSERT INTO Teachers(FirstName,	LastName, [Address], Phone,	SubjectId)
	VALUES
		('Ruthanne', 'Bamb', '84948 Mesta Junction', '3105500146', 6),
		('Gerrard', 'Lowin', '370 Talisman Plaza', '3324874824', 2),
		('Merrile', 'Lambdin', '81 Dahle Plaza', '4373065154', 5),
		('Bert', 'Ivie', '2 Gateway Circle', '4409584510', 4)

INSERT INTO Subjects([Name], Lessons)
	VALUES
		('Geometry', 12),
		('Health', 10),
		('Drama', 7),
		('Sports', 9)


-- 3. Update

UPDATE StudentsSubjects 
	SET [Grade] = 6.00 
	WHERE Grade >= 5.50 AND SubjectId IN(1, 2)

-- 4. Delete

DELETE StudentsTeachers
	FROM StudentsTeachers 
	INNER JOIN Teachers ON StudentsTeachers.TeacherId = Teachers.Id
	WHERE Phone LIKE '%72%'

DELETE FROM Teachers WHERE Phone LIKE '%72%'

-- Section 3. Querying (40 pts)

-- 5. Teen Students

SELECT FirstName, LastName, Age FROM Students
	WHERE Age >= 12
	ORDER BY FirstName ASC,
			 LastName ASC


-- 6. Cool Addresses

SELECT CONCAT(FirstName, ' ', MiddleName, ' ', LastName) AS [Full Name],
	   [Address]
	FROM Students
		WHERE Address LIKE '%road%'
		ORDER BY FirstName ASC,
				 LastName ASC,
				 [Address] ASC
	

-- 7. 42 Phones

SELECT FirstName,
	   [Address], 
	   Phone 
	FROM Students
		WHERE Phone LIKE '42%' AND MiddleName IS NOT NULL
		ORDER BY FirstName ASC

-- 8. Students Teachers

SELECT Students.FirstName,
	   Students.LastName, 
	   COUNT(*) AS [TeachersCount]
	FROM Students 
	JOIN StudentsTeachers
	ON Students.Id = StudentsTeachers.StudentId
		GROUP BY Students.FirstName, Students.LastName

-- 9. Subjects with Students

SELECT [FullName],
	   [Subjects],
	   COUNT(*) AS Students
	FROM (
			SELECT 
				CONCAT(Teachers.FirstName, ' ', Teachers.LastName) AS [FullName],
				CONCAT(Subjects.Name, '-',  Subjects.Lessons) AS [Subjects]
			FROM Teachers
			JOIN Subjects ON Teachers.SubjectId = Subjects.Id
			JOIN StudentsTeachers ON Teachers.Id = StudentsTeachers.TeacherId
		 ) AS TEMP
	GROUP BY [FullName], [Subjects]
	ORDER BY Students DESC,
			 [FullName] ASC,
			 [Subjects] ASC


-- 10. Students to Go	

SELECT CONCAT(FirstName, ' ', LastName) AS [Full Name]
	FROM Students
	LEFT JOIN StudentsExams ON Students.Id = StudentsExams.StudentId
	LEFT JOIN Exams ON StudentsExams.ExamId = Exams.Id
		WHERE StudentId IS NULL
		ORDER BY [Full Name] ASC


-- 11. Busiest Teachers

SELECT TOP(10) FirstName,
	   LastName,
	   COUNT(*) AS [StudentsCount]
	FROM (
			SELECT 
				Teachers.FirstName,
				Teachers.LastName
			FROM Teachers
			JOIN Subjects ON Teachers.SubjectId = Subjects.Id
			JOIN StudentsTeachers ON Teachers.Id = StudentsTeachers.TeacherId
		 ) AS TEMP
	GROUP BY FirstName, LastName
	ORDER BY [StudentsCount] DESC,
			 FirstName ASC,
			 LastName ASC

-- 12. Top Students

SELECT TOP(10)
		FirstName,
		LastName,
		CAST(AVG(StudentsExams.Grade) AS DECIMAL(3,2)) AS [Grade] 
	FROM Students
	JOIN StudentsExams ON Students.Id = StudentsExams.StudentId
		GROUP BY FirstName, LastName
		ORDER BY [Grade] DESC,
				 FirstName ASC,
				 LastName ASC


-- 13. Second Highest Grade

SELECT FirstName,
	   LastName,
	   Grade
			FROM (
				  SELECT FirstName,
				  	     LastName,
				  	     Grade, 
				  	     ROW_NUMBER() OVER (PARTITION BY FirstName, LastName ORDER BY Grade DESC ) AS [Rank]
				  	FROM Students
				  	JOIN StudentsSubjects 
				  	ON Students.Id = StudentsSubjects.StudentId
				 ) AS TEMP
WHERE [Rank] = 2
ORDER BY FirstName ASC,
		 LastName ASC


-- 14. Not So In The Studying

SELECT CONCAT(FirstName, ' ', ISNULL(MiddleName + ' ', ''), LastName) AS [Full Name]
	FROM Students 
	LEFT JOIN StudentsSubjects 
	ON Students.Id = StudentsSubjects.StudentId
	WHERE SubjectId IS NULL
	ORDER BY [Full Name]


-- 15. Top Student per Teacher

SELECT j.[Teacher Full Name], j.SubjectName ,j.[Student Full Name], FORMAT(j.TopGrade, 'N2') AS Grade
  FROM (
SELECT k.[Teacher Full Name],k.SubjectName, k.[Student Full Name], k.AverageGrade  AS TopGrade,
	   ROW_NUMBER() OVER (PARTITION BY k.[Teacher Full Name] ORDER BY k.AverageGrade DESC) AS RowNumber
  FROM (
  SELECT t.FirstName + ' ' + t.LastName AS [Teacher Full Name],
  	   s.FirstName + ' ' + s.LastName AS [Student Full Name],
  	   AVG(ss.Grade) AS AverageGrade,
  	   su.Name AS SubjectName
    FROM Teachers AS t 
    JOIN StudentsTeachers AS st ON st.TeacherId = t.Id
    JOIN Students AS s ON s.Id = st.StudentId
    JOIN StudentsSubjects AS ss ON ss.StudentId = s.Id
    JOIN Subjects AS su ON su.Id = ss.SubjectId AND su.Id = t.SubjectId
GROUP BY t.FirstName, t.LastName, s.FirstName, s.LastName, su.Name
) AS k
) AS j
   WHERE j.RowNumber = 1 
ORDER BY j.SubjectName,j.[Teacher Full Name], TopGrade DESC

-- 16. Average Grade per Subject

SELECT [Name],
	   [AverageGrade]
	FROM (
			SELECT Subjects.[Name],
				   Subjects.Id,
	               AVG(StudentsSubjects.Grade) AS [AverageGrade]
				FROM Subjects
				JOIN StudentsSubjects ON Subjects.Id = StudentsSubjects.SubjectId
				GROUP BY Subjects.[Name], Subjects.Id
				ORDER BY Subjects.Id ASC OFFSET 0 ROWS
	     ) AS TEMP


-- 17. Exams Information

SELECT [Quarter],
	   [SubjectName],
	   COUNT(*) AS [StudentsCount] 
	FROM (
		   SELECT 
		   		CASE
		   		    WHEN MONTH([Date]) >= 1 AND MONTH([Date]) <= 3 THEN 'Q1'
		   		    WHEN MONTH([Date]) >= 4 AND MONTH([Date]) <= 6 THEN 'Q2'
		   		    WHEN MONTH([Date]) >= 7 AND MONTH([Date]) <= 9 THEN 'Q3'
		   		    WHEN MONTH([Date]) >= 10 AND MONTH([Date]) <= 12 THEN 'Q4'
		   			ELSE 'TBA'
		   		END AS [Quarter],
		   			   Subjects.[Name] AS [SubjectName]
		   		 FROM Exams
		   		 JOIN Subjects ON Exams.SubjectId = Subjects.Id
		   		 JOIN StudentsExams ON Exams.Id = StudentsExams.ExamId
		   		 WHERE Grade >= 4.00
		 ) AS TEMP
	 GROUP BY [Quarter], [SubjectName]
	 ORDER BY [Quarter] ASC

-- Section 4. Programmability (20 pts)

-- 18. Exam Grades

GO
CREATE FUNCTION udf_ExamGradesToUpdate(@studentId INT , @grade DECIMAL(3,2))
RETURNS NVARCHAR(100)
AS 
BEGIN
	DECLARE @currentStudentId INT = (
										SELECT TOP(1) Id 
										FROM Students 
										WHERE Id = @studentId
									)
	IF (@currentStudentId IS NULL)
		BEGIN
			RETURN 'The student with provided id does not exist in the school!'
		END

	IF (@grade > 6)
		BEGIN
			RETURN 'Grade cannot be above 6.00!' 
		END

	DECLARE @gradesCount INT = (
								SELECT COUNT(*) FROM Students 
								JOIN StudentsExams ON Students.Id = StudentsExams.StudentId
								WHERE StudentId = @studentId AND Grade BETWEEN @grade AND @grade + 0.50
								)
	DECLARE @studentName NVARCHAR(100) = (
										   SELECT FirstName 
										   FROM Students
										   WHERE Id = @studentId	
										 )

		RETURN CONCAT('You have to update ', @gradesCount, ' grades for the student ', @studentName)
END
GO;

SELECT dbo.udf_ExamGradesToUpdate(121, 5.50)

SELECT dbo.udf_ExamGradesToUpdate(12, 6.20)

SELECT dbo.udf_ExamGradesToUpdate(12, 5.50)

-- 19. Exclude from school

GO
CREATE PROCEDURE usp_ExcludeFromSchool(@StudentId INT)
AS 
BEGIN
	DECLARE @currentStudentId INT = (
									  SELECT TOP(1) Id 
									  FROM Students
									  WHERE Id = @StudentId
									)
	IF (@currentStudentId IS NULL)
		BEGIN
			RAISERROR('This school has no student with the provided id!', 16, 1)
			RETURN 
		END

	DELETE FROM StudentsTeachers
		WHERE StudentId = @StudentId

	DELETE FROM StudentsSubjects
		WHERE StudentId = @StudentId

	DELETE FROM StudentsExams 
		WHERE StudentId = @StudentId

	DELETE FROM Students
		WHERE Id = @StudentId
END
GO;

EXEC usp_ExcludeFromSchool 301

-- 20. Deleted Students
CREATE TABLE ExcludedStudents
(
StudentId INT, 
StudentName VARCHAR(30)
)

GO
CREATE TRIGGER tr_StudentsDelete ON Students
INSTEAD OF DELETE
AS
INSERT INTO ExcludedStudents(StudentId, StudentName)
		SELECT Id, FirstName + ' ' + LastName FROM deleted