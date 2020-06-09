-- Problem 1. Records’ Count

SELECT COUNT(*) AS [Count] 
	FROM WizzardDeposits


-- Problem 2. Longest Magic Wand

SELECT MAX(MagicWandSize) AS  [LongestMagicWand]
	FROM WizzardDeposits


-- Problem 3. Longest Magic Wand per Deposit Groups

SELECT DepositGroup,
	   MAX(MagicWandSize) AS LongestMagicWand
	FROM WizzardDeposits
	GROUP BY DepositGroup


-- Problem 4. * Smallest Deposit Group per Magic Wand Size

SELECT DepositGroup 
	FROM (
	     SELECT TOP(2) 
			DepositGroup,
			AVG(MagicWandSize) AS LongestMagicWand
		  FROM WizzardDeposits
	      GROUP BY DepositGroup
	ORDER BY LongestMagicWand, DepositGroup ASC
	     ) AS [AverageWandSizeQuery] 

	
-- Problem 5. Deposits Sum

SELECT DepositGroup,
	   SUM(DepositAmount) AS [TotalSum]
	FROM WizzardDeposits
	GROUP BY DepositGroup


-- Problem 6. Deposits Sum for Ollivander Family

SELECT DepositGroup,
	   SUM(DepositAmount) AS [TotalSum]
	FROM WizzardDeposits
	WHERE MagicWandCreator = 'Ollivander family'
	GROUP BY DepositGroup


-- Problem 7. Deposits Filter

SELECT DepositGroup,
	   SUM(DepositAmount) AS [TotalSum]
	FROM WizzardDeposits
	WHERE MagicWandCreator = 'Ollivander family'
	GROUP BY DepositGroup
	HAVING SUM(DepositAmount) < 150000
	ORDER BY [TotalSum] DESC


-- Problem 8. Deposit Charge

SELECT DepositGroup, 
       MagicWandCreator,
	   MIN(DepositCharge) AS MinDepositCharge
FROM WizzardDeposits
	GROUP BY DepositGroup, MagicWandCreator


-- Problem 9. Age Groups

SELECT AgeGroup,
       COUNT(AgeGroup) AS WizardCount  
	   FROM (SELECT
				CASE
				WHEN Age BETWEEN 0 AND 10 THEN '[0-10]' 
				WHEN Age BETWEEN 11 AND 20 THEN '[11-20]' 
				WHEN Age BETWEEN 21 AND 30 THEN '[21-30]' 
				WHEN Age BETWEEN 31 AND 40 THEN '[31-40]' 
				WHEN Age BETWEEN 41 AND 50 THEN '[41-50]' 
				WHEN Age BETWEEN 51 AND 60 THEN '[51-60]' 
				ELSE '[61+]'
				END AS AgeGroup
			 FROM WizzardDeposits
			) AS [AgeGroupQuery]
GROUP BY AgeGroup
	

-- Problem 10. First Letter

SELECT DISTINCT SUBSTRING(FirstName, 1, 1) AS [FirstLetter]
	FROM WizzardDeposits
	WHERE DepositGroup = 'Troll Chest'
	ORDER BY [FirstLetter] ASC
	

-- Problem 11.	Average Interest 

SELECT DepositGroup,
       IsDepositExpired,
	   AVG(DepositInterest) AS [AverageInterest]
FROM WizzardDeposits
	WHERE DepositStartDate > '1985.01.01'
	GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired ASC


-- Problem 12. * Rich Wizard, Poor Wizard

SELECT SUM([Host Wizard Deposit] - [Guest Wizard Deposit]) AS SumDifference FROM (SELECT FirstName AS [Host Wizard],
       DepositAmount AS [Host Wizard Deposit],
	   LEAD(FirstName) OVER (ORDER BY Id ASC) [Guest Wizard],
	   LEAD(DepositAmount) OVER (ORDER BY Id ASC) [Guest Wizard Deposit]
FROM WizzardDeposits) AS [LeadQuery]