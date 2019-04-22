CREATE TABLE [dbo].[Users]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL
)

INSERT INTO Users (UserName, Password)
VALUES ('username','password')
