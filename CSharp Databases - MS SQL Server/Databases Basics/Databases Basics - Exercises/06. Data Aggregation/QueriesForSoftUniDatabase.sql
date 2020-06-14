-- Problem 13. Departments Total Salaries

SELECT DepartmentID,
       SUM(Salary) AS TotalSalary
FROM Employees
	GROUP BY DepartmentID
	ORDER BY DepartmentID


-- Problem 14. Employees Minimum Salaries

SELECT DepartmentID,
	   MIN(Salary)
FROM Employees
	WHERE DepartmentID IN (2, 5, 7) AND HireDate > '2000.01.01'
	GROUP BY DepartmentID


-- Problem 15. Employees Average Salaries

SELECT * INTO EmployeesBackup2020 FROM Employees
WHERE Salary > 30000

DELETE FROM EmployeesBackup2020 WHERE ManagerID = 42

UPDATE EmployeesBackup2020 SET Salary += 5000 WHERE DepartmentID = 1

SELECT DepartmentID,
	   AVG(Salary) AS  AverageSalary
	   FROM EmployeesBackup2020
	   GROUP BY DepartmentID


-- Problem 16. Employees Maximum Salaries

SELECT * FROM (SELECT DepartmentID,
       MAX(Salary) AS MaxSalary
FROM Employees 
	GROUP BY DepartmentID) AS SalaryQuery
	WHERE MaxSalary < 30000 OR MaxSalary > 70000


-- Problem 17. Employees Count Salaries

SELECT COUNT(Salary) AS [Count] FROM Employees
	WHERE ManagerID IS NULL


-- Problem 18. *3rd Highest Salary

SELECT DepartmentID, Salary AS [ThirdHighestSalary]
	FROM (
	      SELECT DepartmentID,
                 Salary,
                 RANK() OVER(Partition BY DepartmentID ORDER BY Salary DESC ) AS [SalaryRank]
			FROM Employees
	        GROUP BY DepartmentID, Salary
		 ) AS SalaryQuery 
WHERE SalaryRank = 3


-- Problem 19. **Salary Challenge

SELECT TOP(10) 
		e1.FirstName,
		e1.LastName,
	    e1.DepartmentID
	FROM Employees AS e1
	WHERE e1.Salary > ( SELECT AVG(Salary) AS [AverageSalary]
							FROM Employees AS e2
							WHERE e2.DepartmentID = e1.DepartmentID
							GROUP BY DepartmentID
                      )
	ORDER BY DepartmentID ASC
