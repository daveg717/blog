USE Blog;
GO


DROP TABLE IF EXISTS Categories;
CREATE Table Categories (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL
)

DROP TABLE IF EXISTS Posts;
CREATE TABLE Posts (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Title NVARCHAR(100) NOT NULL,
	Contents NVARCHAR(MAX) NOT NULL,
	Timestamp DATETIME2 NOT NULL,
	Category INT NOT NULL CONSTRAINT FK_Categories REFERENCES Categories(Id)
)


INSERT INTO Categories (Name)
VALUES ('General'),
		('Technology'),
		('Random')

INSERT INTO Posts (Title, Contents, Timestamp, Category)
VALUES ('First', 'this is the first post content', GETDATE(), 1),
('Second', 'this is the 2nd post content', GETDATE(), 3),
('Third', 'this is the 3rd post content', GETDATE(), 2)
