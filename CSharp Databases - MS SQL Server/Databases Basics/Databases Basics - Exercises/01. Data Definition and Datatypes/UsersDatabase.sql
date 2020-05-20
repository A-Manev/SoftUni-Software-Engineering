CREATE DATABASE Users

USE Users

CREATE TABLE Users(
    Id BIGINT PRIMARY KEY IDENTITY NOT NULL,    
	Username VARCHAR(30) UNIQUE NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX)
	CHECK(DATALENGTH(ProfilePicture) <= 900 * 1024),
	LastLoginTime DATETIME2 NOT NULL,
	IsDeleted BIT NOT NULL
)

INSERT INTO Users (Username, [Password], LastLoginTime, IsDeleted)
    VALUES
	      ('Pesho1', '123456', '05.20.2020', 0),
		  ('Pesho2', '123456', '05.20.2020', 1),
		  ('Pesho3', '123456', '05.20.2020', 0),
		  ('Pesho4', '123456', '05.20.2020', 1),
		  ('Pesho5', '123456', '05.20.2020', 0)

SELECT * FROM Users	

-- Remove current primary key 
ALTER TABLE Users
DROP CONSTRAINT [PK__Users__3214EC07A524EE33]

-- Create new primary key that would be the combination of fields Id and Username.
ALTER TABLE Users
ADD CONSTRAINT PK_Users_CompositeIdUsername PRIMARY KEY(Id, Username)

-- Check constraint to ensure that the values in the Password field are at least 5 symbols long. 
ALTER TABLE Users
ADD CONSTRAINT CK_Users_PasswordLength CHECK(LEN([Password]) >= 5)

-- Make the default value of LastLoginTime field to be the current time.
ALTER TABLE Users
ADD CONSTRAINT DF_Users_LastLoginTime DEFAULT GETDATE() FOR LastLoginTime

--Remove Username field from the primary key so only the field Id would be primary key.
ALTER TABLE Users
DROP CONSTRAINT PK_Users_CompositeIdUsername

ALTER TABLE Users
ADD CONSTRAINT PK_Users_Id PRIMARY KEY(Id)

-- Check constraint to ensure that the values in the Username field are at least 3 symbols long. 
ALTER TABLE Users
ADD CONSTRAINT CK_Users_UsernameLenght CHECK(LEN(Username) >= 3)