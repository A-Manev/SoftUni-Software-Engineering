-- Part II – Queries for Geography Database 

-- Problem 12. Countries Holding ‘A’ 3 or More Times
-- Find all countries that holds the letter 'A' in their name at least 3 times (case insensitively), sorted by ISO code. Display the country name and ISO code. 

SELECT [CountryName], [IsoCode] FROM Countries
	WHERE [CountryName] LIKE '%a%a%a%'
		ORDER BY [IsoCode]

SELECT [CountryName], [IsoCode] FROM Countries
	WHERE LEN([CountryName]) - LEN(REPLACE([CountryName], 'A', '')) >= 3
		ORDER BY [IsoCode]

-- Problem 13. Mix of Peak and River Names
-- Combine all peak names with all river names, so that the last letter of each peak name is the same as the first letter of its corresponding river name. 
-- Display the peak names, river names, and the obtained mix (mix should be in lowercase). Sort the results by the obtained mix.

SELECT Peaks.[PeakName], Rivers.[RiverName], 
	LOWER([PeakName] + RIGHT(RiverName, LEN(RiverName) - 1)) AS Mix
	FROM Peaks 
	INNER JOIN Rivers 
		ON RIGHT(PeakName, 1) = LEFT(RiverName, 1)
			ORDER BY Mix

SELECT p.PeakName, r.RiverName, LOWER(
CONCAT(p.PeakName, SUBSTRING(r.RiverName, 2, LEN(r.RiverName) -1))) AS Mix
	FROM Peaks AS p, Rivers AS r
	WHERE RIGHT(p.PeakName, 1) = LEFT(r.RiverName, 1)
		ORDER BY [Mix]