CREATE DATABASE Movies

USE Movies

CREATE TABLE Directors (
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	DirectorName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Directors(DirectorName)
     VALUES 
	       ('Pesho1'),
		   ('Pesho2'),
		   ('Pesho3'),
		   ('Pesho4'),
		   ('Pesho5')

CREATE TABLE Genres (
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	GenreName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Genres(GenreName)
     VALUES 
	       ('Mystery1'),
		   ('Mystery2'),
		   ('Mystery3'),
		   ('Mystery4'),
		   ('Mystery5')
		   
CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	CategoryName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Categories(CategoryName)
     VALUES 
	       ('Horror1'),
		   ('Horror2'),
		   ('Horror3'),
		   ('Horror4'),
		   ('Horror5')

CREATE TABLE Movies (
    Id INT PRIMARY KEY IDENTITY NOT NULL,
	Title NVARCHAR(50) NOT NULL, 
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id) NOT NULL,
	CopyrightYear DATE NOT NULL,
	[Length] TIME NOT NULL,
	GenreId INT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Rating DECIMAL(2, 1) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Movies (Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes)
     VALUES
	       ('film1', 1, '02.02.2020', '02:10:00', 1, 1, 7.3, 'Very good!'),
		   ('film2', 2, '02.02.2020', '02:10:00', 2, 2, 7.3, 'Very good!'),
		   ('film3', 3, '02.02.2020', '02:10:00', 3, 3, 7.3, 'Very good!'),
		   ('film4', 4, '02.02.2020', '02:10:00', 4, 4, 7.3, 'Very good!'),
		   ('film5', 5, '02.02.2020', '02:10:00', 5, 5, 7.3, 'Very good!')

SELECT * FROM Movies