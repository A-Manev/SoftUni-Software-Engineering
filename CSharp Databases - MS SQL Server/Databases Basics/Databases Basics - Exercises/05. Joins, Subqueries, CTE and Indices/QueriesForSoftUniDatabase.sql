-- Problem 1. Employee Address

SELECT TOP(5) 
		Employees.EmployeeID,
		Employees.JobTitle,
		Addresses.AddressID,
		Addresses.AddressText
	FROM Employees 
	JOIN Addresses ON Employees.AddressID = Addresses.AddressID
		ORDER BY AddressID


-- Problem 2. Addresses with Towns

SELECT TOP(50)
		Employees.FirstName, 
		Employees.LastName,
		Towns.Name AS Town,
		Addresses.AddressText
    FROM Employees
	JOIN Addresses ON Employees.AddressID = Addresses.AddressID
	JOIN Towns ON Addresses.TownID = Towns.TownID
		ORDER BY FirstName ASC,
		         LastName ASC


-- Problem 3. Sales Employee

SELECT
		Employees.EmployeeID,
		Employees.FirstName, 
		Employees.LastName,
		Departments.Name AS DepartmentName
	FROM Employees
	JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID
		WHERE Departments.Name = 'Sales'
		ORDER BY EmployeeID ASC


-- Problem 4. Employee Departments

SELECT TOP(5)
		Employees.EmployeeID,
		Employees.FirstName, 
		Employees.Salary,
		Departments.Name AS DepartmentName
	FROM Employees
	JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID
		WHERE Salary > 15000 AND Departments.Name = 'Engineering'
		ORDER BY EmployeeID ASC

-- Problem 5. Employees Without Project

SELECT TOP(3)
		Employees.EmployeeID,
		Employees.FirstName
	FROM Employees 
	LEFT JOIN EmployeesProjects ON Employees.EmployeeID = EmployeesProjects.EmployeeID
		WHERE EmployeesProjects.ProjectID IS NULL
		ORDER BY Employees.EmployeeID ASC
		

-- Problem 6. Employees Hired After

SELECT 
		Employees.FirstName, 
		Employees.LastName,
		Employees.HireDate,
		Departments.Name AS DeptName
	FROM Employees
	JOIN Departments ON Employees.DepartmentID = Departments.DepartmentID
		WHERE HireDate > '1999.1.1' AND Departments.Name IN ('Sales', 'Finance')
		ORDER BY HireDate ASC


-- Problem 7. Employees with Project

SELECT TOP(5)
		Employees.EmployeeID,
		Employees.FirstName,
		Projects.Name AS ProjectName
	FROM Employees
	JOIN EmployeesProjects ON Employees.EmployeeID = EmployeesProjects.EmployeeID
	JOIN Projects ON EmployeesProjects.ProjectID = Projects.ProjectID
		WHERE StartDate > '2002.08.13' AND EndDate IS NULL
		ORDER BY Employees.EmployeeID ASC


-- Problem 8. Employee 24

SELECT Employees.EmployeeID, Employees.FirstName,
CASE
    WHEN DATEPART(YEAR, Projects.StartDate) >= 2005 THEN NULL
    ELSE Projects.Name
 END AS ProjectName
	FROM Employees
	JOIN EmployeesProjects ON Employees.EmployeeID = EmployeesProjects.EmployeeID
	JOIN Projects ON EmployeesProjects.ProjectID = Projects.ProjectID
		WHERE Employees.EmployeeID = 24 

-- Problem 9. Employee Manager

SELECT 
		e1.EmployeeID,
		e1.FirstName,
		e1.ManagerID,
		e2.FirstName AS ManagerName
	FROM Employees e1
	JOIN Employees e2 ON e1.ManagerID = e2.EmployeeID 
		WHERE e1.ManagerID IN (3, 7)
		ORDER BY e1.EmployeeID ASC


-- Problem 10. Employee Summary

SELECT TOP(50)
		e1.EmployeeID,
		CONCAT(e1.FirstName, ' ', e1.LastName) AS EmployeeName,
		CONCAT(e2.FirstName, ' ', e2.LastName) AS ManagerName,
		Departments.Name AS DepartmentName
	FROM Employees e1
	JOIN Employees e2 ON e1.ManagerID = e2.EmployeeID
	JOIN Departments ON e1.DepartmentID = Departments.DepartmentID
		ORDER BY e1.EmployeeID ASC


-- Problem 11. Min Average Salary

SELECT MIN(AverageSalaryByDepartment) AS MinAverageSalary 
	FROM (
			SELECT AVG(Salary) AS AverageSalaryByDepartment 
				FROM Employees
					GROUP BY DepartmentID
		 ) AS AverageSalaryQuery

SELECT TOP(1) AVG(Salary) AS MinAverageSalary  
	FROM Employees
		GROUP BY DepartmentID
		ORDER BY MinAverageSalary  
