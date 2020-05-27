CREATE DATABASE CarRental 

USE CarRental 

CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	CategoryName NVARCHAR(50) NOT NULL, 
	DailyRate DECIMAL(5,2) NOT NULL,
	WeeklyRate DECIMAL(5,2) NOT NULL,
	MonthlyRate DECIMAL(5,2) NOT NULL,
	WeekendRate DECIMAL(5,2) NOT NULL
)

INSERT INTO Categories(CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
    VALUES
	      ('Economy', 14.5, 41.5, 13.5, 71.5),
	      ('Economy1', 15.5, 17.5, 90.5, 10.5),
	      ('Economy2', 60.5, 40.5, 80.5, 10.5)


CREATE TABLE Cars (
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	PlateNumber NVARCHAR(10) NOT NULL,
	Manufacturer NVARCHAR(50) NOT NULL,
	Model  NVARCHAR(50) NOT NULL,
	CarYear DATETIME2 NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Doors INT NOT NULL,
	Picture VARBINARY(MAX),
	Condition NVARCHAR(50) NOT NULL,
	Available BIT NOT NULL
)

INSERT INTO Cars(PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Condition, Available)
    VALUES
	       ('K 1001 BK', 'VW', 'Arteon R Line', '2019', 1, 5, 'new', 1),
		   ('K 1002 BK', 'VW', 'Arteon R Line', '2019', 2, 5, 'new', 0),
		   ('K 1003 BK', 'VW', 'Arteon R Line', '2019', 3, 5, 'new', 1)

CREATE TABLE Employees(
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName  NVARCHAR(50) NOT NULL,
	LastName  NVARCHAR(50) NOT NULL,
	Title  NVARCHAR(50) NOT NULL,
	Notes  NVARCHAR(500) NOT NULL
)

INSERT INTO Employees(FirstName, LastName, Title, Notes)
    VALUES
	      ('Pesho1', 'Peshev1', 'Mechanics', 'Very good!'),
		  ('Pesho2', 'Peshev2', 'Mechanics', 'Very good!'),
		  ('Pesho3', 'Peshev3', 'Mechanics', 'Very good!')

CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	DriverLicenceNumber NVARCHAR(50) NOT NULL,
	FullName NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR(150) NOT NULL,
	City NVARCHAR(20) NOT NULL,
	ZIPCode INT NOT NULL,
	Notes NVARCHAR(500) NOT NULL
)

INSERT INTO Customers(DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes)
    VALUES
	      ('4654SDFSDF1', 'Pesho Geshev1', 'Students Grad, Blok 3', 'Sofia', 1700, 'Noting'),
		  ('4654SDFSDF2', 'Pesho Geshev2', 'Students Grad, Blok 2', 'Sofia', 1700, 'Noting'),
		  ('4654SDFSDF3', 'Pesho Geshev3', 'Students Grad, Blok 1', 'Sofia', 1700, 'Noting')

CREATE TABLE RentalOrders(
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL,
	CarId  INT FOREIGN KEY REFERENCES Cars(Id) NOT NULL,
	TankLevel DECIMAL(5,2) NOT NULL,
	KilometrageStart FLOAT(20) NOT NULL,
	KilometrageEnd FLOAT(20) NOT NULL,
	TotalKilometrage FLOAT(20) NOT NULL,
	StartDate DATETIME2 NOT NULL,
	EndDate DATETIME2 NOT NULL,
	TotalDays BIGINT NOT NULL,
	RateApplied DECIMAL(5,2) NOT NULL,
	TaxRate  DECIMAL(5,2) NOT NULL,
	OrderStatus BIT NOT NULL, 
	Notes NVARCHAR(500) NOT NULL
)

INSERT INTO RentalOrders(EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes)
    VALUES
	      (1, 1, 1, 50, 1200, 1500, 300, '02.02.2020', '03.02.2020', 30, 7.2, 2.3, 1, 'Good car!'),
		  (2, 2, 2, 50, 1200, 1500, 300, '02.02.2020', '03.02.2020', 30, 7.2, 2.3, 1, 'Good car!'),
		  (3, 3, 3, 50, 1200, 1500, 300, '02.02.2020', '03.02.2020', 30, 7.2, 2.3, 1, 'Good car!')