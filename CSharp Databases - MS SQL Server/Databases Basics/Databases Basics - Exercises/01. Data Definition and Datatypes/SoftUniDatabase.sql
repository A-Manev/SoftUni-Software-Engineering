CREATE DATABASE SoftUni

USE SoftUni

CREATE TABLE Towns(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
)

CREATE TABLE Addresses(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[AddressText] NVARCHAR(50),
	[TownId] INT FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Departments(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	[MiddleName] NVARCHAR(50) NOT NULL, 
	[LastName] NVARCHAR(50) NOT NULL, 
	[JobTitle] NVARCHAR(150) NOT NULL, 
	[DepartmentId] INT FOREIGN KEY REFERENCES Departments(Id) NOT NULL, 
	[HireDate] DATETIME2 NOT NULL, 
	[Salary] DECIMAL(9,2) NOT NULL, 
	[AddressId] INT FOREIGN KEY REFERENCES Addresses(Id)
)

INSERT INTO Towns([Name])
	VALUES
		  ('Sofia'),
		  ('Plovdiv'),
		  ('Varna'),
		  ('Burgas')

INSERT INTO Departments([Name])
	VALUES
	      ('Engineering'),
		  ('Sales'),
		  ('Marketing'),
		  ('Software Development'),
		  ('Quality Assurance')

SELECT * FROM Departments

INSERT INTO Employees([FirstName], [MiddleName], [LastName], [JobTitle], [DepartmentId], [HireDate], [Salary])
	VALUES
		  ('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '02.01.2013', 3500.00),
		  ('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '03.02.2004', 4000.00),
          ('Maria', 'Petrova', 'Ivanova', 'Intern',5, '08.28.2016', 525.25),
          ('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '12.09.2007', 3000.00),
          ('Peter', 'Pan', 'Pan', 'Intern', 3, '08.28.2016', 599.88)


-- Problem 19.	Basic Select All Fields 
-- Use the SoftUni database and first select all records from the Towns,
-- then from Departments and finally from Employees table. 
-- Use SQL queries and submit them to Judge at once. 
-- Submit your query statements as Prepare DB & Run queries.

SELECT * FROM Towns
SELECT * FROM Departments 
SELECT * FROM Employees 

-- Problem 20.	Basic Select All Fields and Order Them
-- Modify queries from previous problem by sorting:
--    Towns - alphabetically by name
--    Departments - alphabetically by name
--    Employees - descending by salary

SELECT * FROM Towns ORDER BY [Name] ASC
SELECT * FROM Departments ORDER BY [Name] ASC
SELECT * FROM Employees ORDER BY [Salary] DESC

-- Problem 21.	Basic Select Some Fields
-- Modify queries from previous problem to show only some of the columns. For table:
--    Towns – Name
--    Departments – Name
--    Employees – FirstName, LastName, JobTitle, Salary

SELECT [Name] FROM Towns ORDER BY [Name] ASC
SELECT [Name] FROM Departments ORDER BY [Name] ASC
SELECT [FirstName], [LastName], [JobTitle], [Salary] FROM Employees ORDER BY [Salary] DESC


-- Problem 22. Increase Employees Salary
-- Use SoftUni database and increase the salary of all employees by 10%. 
-- Then show only Salary column for all in the Employees table. 
-- Submit your query statements as Prepare DB & Run queries.

UPDATE Employees
SET Salary = Salary  * 1.1
SELECT [Salary] FROM Employees
