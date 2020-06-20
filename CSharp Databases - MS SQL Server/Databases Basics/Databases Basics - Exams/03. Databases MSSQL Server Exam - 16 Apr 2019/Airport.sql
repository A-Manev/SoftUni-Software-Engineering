-- Section 1. DDL (30 pts)

-- 1. Database Design

CREATE DATABASE [Airport]

USE [Airport]

CREATE TABLE Planes(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(30) NOT NULL,
	[Seats] INT NOT NULL,
	[Range] INT NOT NULL
)

CREATE TABLE Flights(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[DepartureTime] DATETIME,
	[ArrivalTime] DATETIME,
	[Origin] NVARCHAR(50) NOT NULL,
	[Destination] NVARCHAR(50) NOT NULL,
	[PlaneId] INT FOREIGN KEY REFERENCES Planes([Id]) NOT NULL
)

CREATE TABLE Passengers(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[FirstName] NVARCHAR(30) NOT NULL,
	[LastName] NVARCHAR(30) NOT NULL,
	[Age] INT NOT NULL,
	[Address] NVARCHAR(30) NOT NULL,
	[PassportId] NVARCHAR(11) NOT NULL,
		-- CHECK(LEN([PassportId]) = 11) ?
)

CREATE TABLE LuggageTypes(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[Type] NVARCHAR(30) NOT NULL
)

CREATE TABLE Luggages(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[LuggageTypeId] INT FOREIGN KEY REFERENCES LuggageTypes([Id]) NOT NULL,
	[PassengerId] INT FOREIGN KEY REFERENCES Passengers([Id]) NOT NULL
)

CREATE TABLE Tickets(
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
	[PassengerId] INT FOREIGN KEY REFERENCES Passengers([Id]) NOT NULL,
	[FlightId] INT FOREIGN KEY REFERENCES Flights([Id]) NOT NULL,
	[LuggageId] INT FOREIGN KEY REFERENCES Luggages([Id]) NOT NULL,
	[Price] DECIMAL(18,2) NOT NULL
)

-- Section 2. DML (10 pts)

-- 2. Insert

INSERT INTO Planes([Name], [Seats], [Range])
	VALUES
		('Airbus 336',112 , 5132),
		('Airbus 330', 432,	5325),
		('Boeing 369', 231, 2355),
		('Stelt 297', 254, 2143),
		('Boeing 338', 165,	5111),
		('Airbus 558', 387,	1342),
		('Boeing 128', 345,	5541)

INSERT INTO LuggageTypes([Type])
	VALUES
		('Crossbody Bag'),
		('School Backpack'),
		('Shoulder Bag')


-- 3. Update

UPDATE Tickets
	SET 
		Tickets.Price *= 1.13
	FROM Tickets
	JOIN Flights ON Tickets.FlightId = Flights.Id
	WHERE Destination = 'Carlsbad'

-- 4. Delete 

DELETE FROM Tickets WHERE FlightId  = 30
DELETE FROM Flights WHERE Destination = 'Ayn Halagim'


-- Section 3. Querying (40 pts)

-- 5.Trips

SELECT [Origin], [Destination] FROM Flights
	ORDER BY [Origin] ASC,
			 [Destination] ASC


-- 6. The "Tr" Planes

SELECT * FROM Planes
	WHERE [Name] LIKE '%tr%'
	ORDER BY [Id] ASC,
	         [Name] ASC,
			 [Seats] ASC,
			 [Range] ASC


-- 7. Flight Profits

SELECT FlightId, SUM(Price) AS [Price] FROM Tickets
	GROUP BY FlightId
	ORDER BY [Price] DESC,
			 FlightId ASC


-- 8. Passengers and Prices	

SELECT TOP (10) 
	   FirstName,
	   LastName, 
	   Price  
	FROM Passengers 
	JOIN Tickets ON Passengers.Id = Tickets.PassengerId
	ORDER BY Price DESC,
			 FirstName ASC,
			 LastName ASC

-- 9. Most Used Luggage's

SELECT LuggageTypes.[Type],
		COUNT(*) AS [MostUsedLuggage]
FROM LuggageTypes
	JOIN Luggages ON LuggageTypes.Id = Luggages.LuggageTypeId
	GROUP BY LuggageTypes.[Type]
	ORDER BY [MostUsedLuggage] DESC,
			 LuggageTypes.Type ASC


-- 10. Passenger Trips

SELECT CONCAT(Passengers.FirstName, ' ', Passengers.LastName) AS [Full Name],
	   Flights.Origin,
	   Flights.Destination
FROM Passengers
	JOIN Tickets ON Passengers.Id = Tickets.PassengerId
	JOIN Flights ON Tickets.FlightId = Flights.Id
	ORDER BY [Full Name] ASC, 
	         Flights.Origin ASC,
			 Flights.Destination ASC


-- 11.	Non Adventures People

SELECT Passengers.FirstName,
	   Passengers.LastName,
	   Passengers.Age
FROM Passengers
	LEFT JOIN Tickets ON Passengers.Id = Tickets.PassengerId
	WHERE Tickets.Id IS NULL
	ORDER BY  Passengers.Age DESC,
	          Passengers.FirstName ASC,
	          Passengers.LastName ASC


-- 12. Lost Luggage's

SELECT Passengers.PassportId AS [Passport Id],
	   Passengers.[Address]
	FROM Passengers
	LEFT JOIN Luggages ON Passengers.Id = Luggages.PassengerId
	WHERE Luggages.Id IS NULL 
	ORDER BY Passengers.PassportId ASC,
			 Passengers.[Address] ASC


-- 13. Count of Trips

SELECT Passengers.FirstName,
	   Passengers.LastName,
	   COUNT(Tickets.Id) AS [Total Trips]
	FROM Passengers 
	LEFT JOIN Tickets ON Passengers.Id = Tickets.PassengerId
	GROUP BY Passengers.FirstName, Passengers.LastName
	ORDER BY [Total Trips] DESC,
			 Passengers.FirstName ASC,
	         Passengers.LastName ASC


-- 14.	Full Info 

SELECT CONCAT(Passengers.FirstName, ' ', Passengers.LastName) AS [Full Name],
	   Planes.[Name],
	   CONCAT(Flights.Origin, ' - ', Flights.Destination),
	   LuggageTypes.[Type]
	FROM Passengers
	JOIN Tickets ON Passengers.Id = Tickets.PassengerId
	JOIN Flights ON Tickets.FlightId = Flights.Id
	JOIN Planes ON Flights.PlaneId = Planes.Id
	JOIN Luggages ON Tickets.LuggageId = Luggages.Id
	JOIN LuggageTypes ON Luggages.LuggageTypeId = LuggageTypes.Id
		ORDER BY [Full Name] ASC,
				 Planes.[Name] ASC,
				 Flights.Origin ASC,
				 Flights.Destination ASC,
				  LuggageTypes.[Type] ASC


-- 15. Most Expensive Trips

SELECT FirstName,
       LastName,
	   Destination,
	   Price
	   FROM (
		      SELECT 
				   Passengers.FirstName,
				   Passengers.LastName,
				   Flights.Destination,
				   Tickets.Price,
				   RANK() OVER (PARTITION BY FirstName ORDER BY Price DESC) AS [Rank]
				FROM Passengers 
				JOIN Tickets ON Passengers.Id = Tickets.PassengerId
				JOIN Flights ON Tickets.FlightId = Flights.Id
			) AS [all PassengersQuery]
WHERE [Rank] = 1
	ORDER BY Price DESC,
	         FirstName ASC,
	         LastName ASC,
	         Destination ASC


-- 16. Destinations Info
 
SELECT Flights.Destination,
       COUNT(Tickets.Id) AS [FilesCount]
		FROM Flights
	LEFT JOIN Tickets ON Flights.Id = Tickets.FlightId
	GROUP BY Flights.Destination
	ORDER BY [FilesCount] DESC,
			 Flights.Destination ASC

-- 17. PSP

SELECT Planes.[Name],
	   Planes.Seats,
	   COUNT(Tickets.Id) AS [Passengers Count]
		FROM Planes 
	LEFT JOIN Flights ON Planes.Id = Flights.PlaneId
	LEFT JOIN Tickets ON Flights.Id = Tickets.FlightId
	LEFT JOIN Passengers ON Tickets.PassengerId = Passengers.Id
	GROUP BY Planes.Name, Planes.Seats
	ORDER BY [Passengers Count] DESC,
			 Planes.[Name] ASC,
			 Planes.Seats ASC
	
-- Section 4. Programmability (20 pts)

-- 18. Vacation

GO
CREATE FUNCTION udf_CalculateTickets(@origin NVARCHAR(50), @destination NVARCHAR(50), @peopleCount INT) 
RETURNS NVARCHAR(100)
AS
BEGIN 
	DECLARE @returnValue NVARCHAR(100)
	DECLARE @price DECIMAL(18,2) = 0

	DECLARE @tripId INT = (SELECT Flights.Id FROM Flights
					JOIN Tickets ON Flights.Id = Tickets.FlightId
					WHERE Flights.Origin = @origin AND Flights.Destination = @destination)

		IF(@peopleCount <= 0)
			BEGIN
				SET @returnValue = 'Invalid people count!'
			END 
		ELSE IF (@tripId IS NULL)
			BEGIN 
				SET @returnValue = 'Invalid flight!'
			END
		ELSE
			BEGIN
				SET @price  = @peopleCount * (SELECT Price FROM Flights
					JOIN Tickets ON Flights.Id = Tickets.FlightId
					WHERE Flights.Origin = @origin AND Flights.Destination = @destination)

				SET @returnValue = CONCAT('Total price ', @price)
			END
	RETURN @returnValue
END 
GO

SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', 33)

SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', -1)

SELECT dbo.udf_CalculateTickets('Invalid','Rancabolang', 33)


-- 19. Wrong Data

GO
CREATE PROCEDURE usp_CancelFlights
AS
BEGIN 
	UPDATE Flights 
		SET ArrivalTime = NULL,
		    DepartureTime = NULL
	WHERE ArrivalTime > DepartureTime
END
GO;


-- 20. Deleted Planes
CREATE TABLE DeletedPlanes (
             Id INT NOT NULL,
			 [Name] NVARCHAR(30) NOT NULL,
			 Seats INT NOT NULL,
			 [Range] INT NOT NULL)

GO

CREATE TRIGGER tr_DeletedPlane 
            ON Planes FOR DELETE
            AS
		INSERT INTO DeletedPlanes
		SELECT *
		  FROM deleted 