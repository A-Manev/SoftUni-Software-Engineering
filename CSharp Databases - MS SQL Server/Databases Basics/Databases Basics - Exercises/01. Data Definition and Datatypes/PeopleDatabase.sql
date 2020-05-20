CREATE DATABASE People

USE People

CREATE TABLE People (
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX)
	CHECK(DATALENGTH(Picture) <= 2048 * 1024),
	Height DECIMAL(10, 2), 
    [Weight] DECIMAL(10, 2), 
	Gender CHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX)
)

INSERT INTO People([Name], Gender, Birthdate)
    VALUES
	      ('Pesho1', 'm', '05.20.2020'),
		  ('Pesho2', 'm', '05.20.2020'),
		  ('Pesho3', 'm', '05.20.2020'),
		  ('Pesho4', 'm', '05.20.2020'),
		  ('Pesho5', 'm', '05.20.2020')

SELECT * FROM People