-- Problem 12. Highest Peaks in Bulgaria

SELECT 
		Countries.CountryCode,
		Mountains.MountainRange,
		Peaks.PeakName,
		Peaks.Elevation
	FROM Countries
	JOIN MountainsCountries 
	ON	Countries.CountryCode = MountainsCountries.CountryCode
	JOIN Mountains
	ON MountainsCountries.MountainId = Mountains.Id
	JOIN Peaks
	ON Mountains.Id = Peaks.MountainId
		WHERE Countries.CountryCode = 'BG' AND Peaks.Elevation > 2835
		ORDER BY Peaks.Elevation DESC


-- Problem 13. Count Mountain Ranges
 
SELECT	Countries.CountryCode,
		COUNT(Countries.CountryCode) AS MountainRanges
	FROM Countries	
	JOIN MountainsCountries 
	ON	Countries.CountryCode = MountainsCountries.CountryCode
	JOIN Mountains
	ON MountainsCountries.MountainId = Mountains.Id
		WHERE Countries.CountryCode IN ('BG', 'RU', 'US')
		GROUP BY Countries.CountryCode
		HAVING COUNT (Countries.CountryCode) > 0 

SELECT CountryCode,
	   COUNT(MountainId) AS MountainRanges
	FROM MountainsCountries
		WHERE CountryCode IN ('BG', 'RU', 'US')
		GROUP BY CountryCode


-- Problem 14. Countries with Rivers

SELECT TOP(5)
		Countries.CountryName,
        Rivers.RiverName
	FROM Countries
	LEFT JOIN CountriesRivers 
	ON Countries.CountryCode = CountriesRivers.CountryCode
	LEFT JOIN Rivers
	ON CountriesRivers.RiverId = Rivers.Id
		WHERE Countries.ContinentCode = 'AF'
		ORDER BY Countries.CountryName ASC


-- Problem 15. *Continents and Currencies
 
 SELECT ContinentCode, 
        CurrencyCode, 
			CurrencyCount AS CurrencyUsage
				  FROM (
						SELECT ContinentCode,
							   CurrencyCode, 
							   CurrencyCount,
							   DENSE_RANK() OVER 
							    (
								  PARTITION BY ContinentCode
								  ORDER BY CurrencyCount DESC
								) AS [CurrencyRank]
								FROM (
									  SELECT ContinentCode, 
											 CurrencyCode,
											 COUNT(*) AS CurrencyCount
										   FROM Countries	
										   GROUP BY ContinentCode, CurrencyCode
									 ) AS [CurrencyCountQuery]
WHERE CurrencyCount > 1) AS [CurrencyRankingQuery]
	WHERE CurrencyRank = 1
		ORDER BY ContinentCode


-- Problem 16. Countries without any Mountains

SELECT COUNT(*) AS [Count] 
	FROM Countries
	LEFT JOIN MountainsCountries 
	ON Countries.CountryCode = MountainsCountries.CountryCode
	LEFT JOIN Mountains
	ON MountainsCountries.MountainId = Mountains.Id
		WHERE Mountains.Id IS NULL


-- Problem 17. Highest Peak and Longest River by Country

SELECT TOP(5) Countries.CountryName,
	   MAX(Peaks.Elevation) AS [HighestPeakElevation],
	   MAX(Rivers.Length) AS [LongestRiverLength]
	   FROM Countries
	LEFT JOIN MountainsCountries ON Countries.CountryCode = MountainsCountries.CountryCode
	LEFT JOIN Mountains ON MountainsCountries.MountainId = Mountains.Id
	LEFT JOIN Peaks ON Mountains.Id = Peaks.MountainId
	LEFT JOIN CountriesRivers ON Countries.CountryCode = CountriesRivers.CountryCode
	LEFT JOIN Rivers ON CountriesRivers.RiverId = Rivers.Id
	GROUP BY Countries.CountryName
	ORDER BY [HighestPeakElevation] DESC,
			 [LongestRiverLength] DESC, 
			 Countries.CountryName ASC


-- Problem 18. * Highest Peak Name and Elevation by Country

SELECT TOP(5) [Country],
		CASE 
			WHEN PeakName IS NULL THEN '(no highest peak)'
			ELSE PeakName
		END AS [Highest Peak Name],
		CASE 
			WHEN Elevation IS NULL THEN 0
			ELSE Elevation
		END AS [Highest Peak Elevation],
		CASE
			WHEN MountainRange IS NULL THEN '(no mountain)' 
			ELSE MountainRange
		END AS [Mountain]
		   FROM (
				 SELECT *,
						DENSE_RANK() OVER
							(PARTITION BY [Country] ORDER BY Elevation DESC) AS [PeakRank]
					FROM (
						  SELECT Countries.CountryName AS [Country],
								 Peaks.PeakName,
								 Peaks.Elevation,
								 Mountains.MountainRange
							FROM Countries
							LEFT JOIN MountainsCountries ON Countries.CountryCode = MountainsCountries.CountryCode
							LEFT JOIN Mountains ON MountainsCountries.MountainId = Mountains.Id
							LEFT JOIN Peaks ON Mountains.Id = Peaks.MountainId
						 ) AS [FullInfoQuery] 
				) AS [PeakRankingQuery]
WHERE [PeakRank] = 1
ORDER BY [Country] ASC,
		 [Highest Peak Name] ASC