CREATE DATABASE Hotel

USE Hotel 

CREATE TABLE Employees(
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Title NVARCHAR(250) NOT NULL,
	Notes NVARCHAR(500) NOT NULL
)

INSERT INTO Employees(FirstName, LastName, Title, Notes)
	VALUES
		  ('Ivan', 'Ivanov', 'Security', 'nope' ),
		  ('Ivan1', 'Ivanov', 'Security', 'nope' ),	
		  ('Ivan3', 'Ivanov', 'Security', 'nope' )	

CREATE TABLE Customers(
    AccountNumber INT PRIMARY KEY IDENTITY NOT NULL, 
    FirstName NVARCHAR(50) NOT NULL, 
    LastName NVARCHAR(50) NOT NULL, 
    PhoneNumber VARCHAR(22) NOT NULL,
    EmergencyName NVARCHAR(30) NOT NULL, 
    EmergencyNumber INT NOT NULL, 
    Notes NVARCHAR(500) NOT NULL
)

INSERT INTO Customers(FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber, Notes)
	VALUES
		  ('Pesho', 'Peshev', '0877769699', 'Hospital', 112, 'fast' ),	
		  ('Pesho', 'Peshev', '0877769699', 'Hospital', 112, 'fast' ),
		  ('Pesho', 'Peshev', '0877769699', 'Hospital', 112, 'fast' )

CREATE TABLE RoomStatus(
    RoomStatus NVARCHAR(50) PRIMARY KEY NOT NULL, 
    Notes NVARCHAR(500) NOT NULL
)

INSERT INTO RoomStatus(RoomStatus, Notes) 
	VALUES
		  ('occupied', 'room'),
		  ('non occupied', 'room'),
		  ('repairs', 'room')

CREATE TABLE RoomTypes(
    RoomType NVARCHAR(50) PRIMARY KEY NOT NULL, 
    Notes NVARCHAR(500) NOT NULL
)

INSERT INTO RoomTypes(RoomType, Notes) 
	VALUES
		  ('single', 'room'),
		  ('double', 'room'),
		  ('appartment', 'room')
		  
CREATE TABLE BedTypes(
    BedType NVARCHAR(50) PRIMARY KEY NOT NULL, 
    Notes NVARCHAR(500) NOT NULL
)

INSERT INTO BedTypes(BedType,Notes) 
	VALUES
		  ('single', 'room'),
		  ('double', 'room'),
		  ('couch', 'room')

CREATE TABLE Rooms(
    RoomNumber INT PRIMARY KEY NOT NULL, 
	RoomType NVARCHAR(50) FOREIGN KEY REFERENCES RoomTypes(RoomType) NOT NULL, 
	BedType NVARCHAR(50) FOREIGN KEY REFERENCES BedTypes(BedType) NOT NULL, 
	Rate DECIMAL(5, 2) NOT NULL, 
	RoomStatus NVARCHAR(50) FOREIGN KEY REFERENCES RoomStatus(RoomStatus) NOT NULL , 
	Notes NVARCHAR(500) NOT NULL
)

INSERT INTO Rooms(RoomNumber, RoomType, BedType, Rate, RoomStatus, Notes) 
	VALUES
		  (201, 'single', 'single', 40.0, 'occupied', 'room'),
		  (205, 'double', 'double', 70.0, 'non occupied', 'room'),
		  (208, 'appartment', 'double', 110.0, 'repairs', 'room')

CREATE TABLE Payments(
    Id INT PRIMARY KEY IDENTITY NOT NULL, 
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	PaymentDate DATETIME2 NOT NULL, 
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL, 
	FirstDateOccupied DATETIME2 NOT NULL, 
	LastDateOccupied DATETIME2 NOT NULL, 
	TotalDays AS DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied),
	AmountCharged DECIMAL(9, 2) NOT NULL, 
	TaxRate DECIMAL(9, 2) NOT NULL, 
	TaxAmount AS AmountCharged * TaxRate, 
	PaymentTotal AS AmountCharged + AmountCharged * TaxRate, 
	Notes NVARCHAR(500) NOT NULL
)

INSERT INTO Payments(EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, AmountCharged, TaxRate, Notes) 
	VALUES
		  (1, '2011-11-25', 2, '2017-11-30', '2017-12-04', 250.0, 0.2, 'room'),
		  (3, '2014-06-03', 3, '2014-06-06', '2014-06-09', 340.0, 0.2, 'room'),
		  (2, '2016-02-25', 2, '2016-02-27', '2016-03-04', 500.0, 0.2, 'room')

CREATE TABLE Occupancies (
    Id INT PRIMARY KEY IDENTITY NOT NULL,  
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL, 
	DateOccupied DATETIME2 NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL, 
	RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber) NOT NULL, 
	RateApplied DECIMAL(5, 2) NOT NULL, 
	PhoneCharge DECIMAL(5, 2) NOT NULL, 
	Notes NVARCHAR(500) NOT NULL
)

INSERT INTO Occupancies(EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge, Notes) 
	VALUES
		  (2, '2011-02-04', 3, 205, 70.0, 12.54, 'room'),
		  (1, '2015-04-09', 1, 201, 40.0, 11.22, 'room'),
		  (3, '2012-06-08', 2, 208, 110.0, 10.05, 'room')

UPDATE Payments 
SET TaxRate = TaxRate * 0.97
SELECT TaxRate FROM Payments

DELETE Occupancies