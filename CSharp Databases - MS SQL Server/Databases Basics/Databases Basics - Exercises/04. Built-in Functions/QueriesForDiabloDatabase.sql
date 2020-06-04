-- Part III – Queries for Diablo Database

-- Problem 14.Games from 2011 and 2012 year
-- Find the top 50 games ordered by start date, then by name of the game. 
-- Display only games from 2011 and 2012 year. Display start date in the format “yyyy-MM-dd”. 

SELECT TOP(50) [Name], FORMAT([Start], 'yyyy-MM-dd') AS [Start] FROM Games
	WHERE DATEPART(YEAR, [Start]) IN (2011, 2012)
		ORDER BY [Start], [Name]


-- Problem 15. User Email Providers
-- Find all users along with information about their email providers. Display the username and email provider.
-- Sort the results by email provider alphabetically, then by username. 

SELECT [Username], 
	RIGHT([Email], LEN([Email]) - CHARINDEX('@',[Email])) AS [Email Provider] 
	FROM Users
	ORDER BY [Email Provider], [Username]
	

-- Problem 16. Get Users with IPAdress Like Pattern
-- Find all users along with their IP addresses sorted by username alphabetically. 
-- Display only rows that IP address matches the pattern: “***.1^.^.***”. 
-- Legend: * - one symbol, ^ - one or more symbols

SELECT [Username], [IpAddress] FROM Users
	WHERE [IpAddress] LIKE '[1234567890][1234567890][1234567890].1%.%.[1234567890][1234567890][1234567890]'
		ORDER BY [Username] ASC

SELECT Username, IpAddress
    FROM Users
    WHERE IpAddress LIKE '___.1_%._%.___'
    ORDER BY Username


-- Problem 17. Show All Games with Duration and Part of the Day
-- Find all games with part of the day and duration sorted by game name alphabetically then by duration (alphabetically,
-- not by the timespan) and part of the day (all ascending). Parts of the day should be Morning (time is >= 0 and < 12), 
-- Afternoon (time is >= 12 and < 18), Evening (time is >= 18 and < 24). Duration should be Extra Short (smaller or equal to 3),
-- Short (between 4 and 6 including), Long (greater than 6) and Extra Long (without duration). 

SELECT [Name] AS [Game],
	CASE
		WHEN DATEPART(HOUR, [Start]) BETWEEN 0 AND 11 THEN 'Morning'
		WHEN DATEPART(HOUR, [Start]) BETWEEN 12 AND 17 THEN 'Afternoon'
        ELSE 'Evening'
	END AS [Part of the Day],
	CASE 
		WHEN [Duration] BETWEEN 0 AND 3 THEN 'Extra Short'
		WHEN [Duration] BETWEEN 4 AND 6 THEN 'Short'
		WHEN [Duration] > 6 THEN 'Long'
		ELSE 'Extra Long'
	END AS [Duration]
	FROM Games
		ORDER BY [Name], [Duration]