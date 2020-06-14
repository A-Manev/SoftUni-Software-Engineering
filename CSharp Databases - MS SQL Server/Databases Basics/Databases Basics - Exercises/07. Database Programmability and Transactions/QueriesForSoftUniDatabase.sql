-- Section I. Functions and Procedures

-- Problem 1. Employees with Salary Above 35000

GO
CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000 
AS
BEGIN
	SELECT FirstName, LastName
		FROM Employees
		WHERE Salary > 35000
END		
GO;


-- Problem 2. Employees with Salary Above Number

GO
CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber(@minSalary DECIMAL(18,4))
AS
BEGIN
	SELECT FirstName, LastName
		FROM Employees
		WHERE Salary >= @minSalary
END	
GO;	


-- Problem 3. Town Names Starting With

GO
CREATE PROCEDURE usp_GetTownsStartingWith(@inputString NVARCHAR(MAX))
AS
BEGIN 
	SELECT [Name]
		FROM Towns
		WHERE LEFT([Name], LEN(@inputString)) LIKE LEFT(@inputString, LEN(@inputString))
END
GO;


-- Problem 4. Employees from Town

GO
CREATE PROCEDURE usp_GetEmployeesFromTown(@townName NVARCHAR(MAX))
AS
BEGIN 
	SELECT Employees.FirstName, Employees.LastName 
		FROM Employees
		JOIN Addresses ON Employees.AddressID = Addresses.AddressID
		JOIN Towns ON Addresses.TownID = Towns.TownID
		WHERE Towns.[Name] = @townName
END
GO;


-- Problem 5. Salary Level Function

GO
CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(10)
AS
BEGIN
	DECLARE @salaryLevel NVARCHAR(10)

	IF (@salary < 30000)
	BEGIN
		SET @salaryLevel = 'Low';
	END
	ELSE IF (@salary BETWEEN 30000 AND 50000)
	BEGIN
		SET @salaryLevel = 'Average';
	END
	ELSE IF (@salary > 50000)
	BEGIN
		SET @salaryLevel = 'High';
	END
	
	RETURN @salaryLevel
END
GO;

SELECT Salary,
       dbo.ufn_GetSalaryLevel(Salary) AS [SalaryLevel]
FROM Employees


-- Problem 6. Employees by Salary Level

GO
CREATE PROCEDURE usp_EmployeesBySalaryLevel(@levelOfSalary NVARCHAR(10))
AS
BEGIN
	SELECT FirstName,
	       LastName
		 FROM (SELECT Salary,
		              FirstName,
					  LastName,
                      dbo.ufn_GetSalaryLevel(Salary) AS [SalaryLevel]
               FROM Employees
			  ) AS TMPS
	WHERE [SalaryLevel] = @levelOfSalary
END
GO;


-- Problem 7. Define Function

CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(50), @word NVARCHAR(50))
RETURNS BIT
AS
BEGIN
    DECLARE @result BIT = 1
	DECLARE @COUNT INT = 1
	DECLARE @currentChar NVARCHAR(1) = NULL
	 
	WHILE @COUNT <= LEN(@word) 
	BEGIN
		SET @currentChar = SUBSTRING(@word, @COUNT, 1) 

		IF (@setOfLetters LIKE '%' + @currentChar + '%')
			BEGIN 
				SET @COUNT += 1;	
			END
		ELSE 
			BEGIN
				SET @result = 0
		        BREAK
			END
	END
	RETURN @result 
END
GO;

SELECT dbo.ufn_IsWordComprised('bobr', 'bobr')


-- Problem 8. * Delete Employees and Departments

GO
CREATE PROCEDURE usp_DeleteEmployeesFromDepartment(@departmentId INT) 
AS
BEGIN
	DELETE FROM EmployeesProjects
	WHERE EmployeeID IN (
						  SELECT EmployeeID FROM Employees
						  WHERE DepartmentID = @departmentId
	                    )

	UPDATE Employees
	SET ManagerID = NULL
	WHERE ManagerID IN 
						(
						  SELECT EmployeeID FROM Employees
						  WHERE DepartmentID = @departmentId
	                    )

	ALTER TABLE Departments ALTER COLUMN ManagerID INT 

	UPDATE Departments
	SET ManagerID = NULL
	WHERE ManagerID IN 
						(
						  SELECT EmployeeID FROM Employees
						  WHERE DepartmentID = @departmentId
	                    )

	DELETE FROM Employees
	WHERE DepartmentID = @departmentId

	DELETE FROM Departments
	WHERE DepartmentID = @departmentId

	SELECT COUNT(*) AS [Count]
	FROM Employees
	WHERE DepartmentID = @departmentId
END
GO;
