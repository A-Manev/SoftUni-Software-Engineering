-- Part I – Queries for SoftUni Database

-- Problem 1. Find Names of All Employees by First Name
-- Write a SQL query to find first and last names of all employees whose first name starts with “SA”. 

SELECT [FirstName], [LastName] FROM Employees
	WHERE [FirstName] LIKE 'Sa%'

SELECT [FirstName], [LastName] FROM Employees
WHERE LEFT([FirstName], 2) = 'Sa'

-- Problem 2. Find Names of All employees by Last Name 
-- Write a SQL query to find first and last names of all employees whose last name contains “ei”. 

SELECT [FirstName], [LastName] FROM Employees
	WHERE [LastName] LIKE '%ei%'

SELECT [FirstName], [LastName] FROM Employees
	WHERE NOT CHARINDEX('ei', [LastName]) = 0 


-- Problem 3. Find First Names of All Employees
-- Write a SQL query to find the first names of all employees in the departments with ID 3 or 10 and whose hire year is between 1995 and 2005 inclusive.

SELECT [FirstName] FROM Employees
	WHERE [DepartmentID] IN (3, 10) 
		AND DATEPART(YEAR, HireDate) BETWEEN 1995 AND 2005


-- Problem 4. Find All Employees Except Engineers
-- Write a SQL query to find the first and last names of all employees whose job titles does not contain “engineer”. 

SELECT [FirstName], [LastName] FROM Employees
	WHERE [JobTitle] NOT LIKE '%engineer%'


-- Problem 5. Find Towns with Name Length
-- Write a SQL query to find town names that are 5 or 6 symbols long and order them alphabetically by town name. 

SELECT [Name] FROM Towns
	WHERE LEN([Name]) IN (5, 6)
	ORDER BY [Name] ASC


-- Problem 6. Find Towns Starting With
-- Write a SQL query to find all towns that start with letters M, K, B or E. Order them alphabetically by town name. 

SELECT * FROM Towns
	WHERE [Name] LIKE 'M%'OR 
	[Name] LIKE 'K%'OR 
	[Name] LIKE'B%'OR 
	[Name] LIKE'E%'
	ORDER BY [Name] ASC

SELECT * FROM Towns
	WHERE [Name] LIKE '[MKBE]%' 
	ORDER BY [Name] ASC

SELECT * FROM Towns
	WHERE LEFT([Name], 1) IN ('M', 'K', 'B', 'E')
	ORDER BY [Name] ASC

SELECT * FROM Towns
	WHERE SUBSTRING([Name], 1, 1) IN ('M', 'K', 'B', 'E')
	ORDER BY [Name] ASC

-- Problem 7. Find Towns Not Starting With
-- Write a SQL query to find all towns that does not start with letters R, B or D. Order them alphabetically by name. 

SELECT * FROM Towns
	WHERE [Name] LIKE '[^RBD]%'
	ORDER BY [Name] ASC


--Problem 8. Create View Employees Hired After 2000 Year
--Write a SQL query to create view V_EmployeesHiredAfter2000 with first and last name to all employees hired after 2000 year. 

GO
CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT [FirstName], [LastName] 
FROM Employees
WHERE YEAR([HireDate]) > 2000 -- WHERE DATEPART(YEAR, HireDate) > 2000
GO

SELECT * from V_EmployeesHiredAfter2000


-- Problem 9. Length of Last Name
-- Write a SQL query to find the names of all employees whose last name is exactly 5 characters long.

SELECT [FirstName], [LastName] FROM Employees
	WHERE LEN([LastName]) = 5


-- Problem 10. Rank Employees by Salary
-- Write a query that ranks all employees using DENSE_RANK. In the DENSE_RANK function, employees need to be partitioned by Salary and ordered by EmployeeID. 
-- You need to find only the employees whose Salary is between 10000 and 50000 and order them by Salary in descending order.

SELECT [EmployeeID], [FirstName], [LastName], [Salary],
	DENSE_RANK () OVER ( 
	        PARTITION BY [Salary]
			ORDER BY [EmployeeID] 
	) AS [Rank]
FROM Employees
	WHERE [Salary] BETWEEN 10000 AND 50000 
	ORDER BY [Salary] DESC


-- Problem 11. Find All Employees with Rank 2 *
-- Use the query from the previous problem and upgrade it, so that it finds only the employees whose Rank is 2 and again, order them by Salary (descending).

SELECT * FROM
	(SELECT [EmployeeID], [FirstName], [LastName], [Salary],
		DENSE_RANK () OVER ( 
				PARTITION BY [Salary]
				ORDER BY [EmployeeID] 
		) AS [Rank]
	FROM Employees
		WHERE [Salary] BETWEEN 10000 AND 50000) 
		 AS temp
WHERE [Rank] = 2
	ORDER BY [Salary] DESC