CREATE TABLE [dbo].[CommonIngredients]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Deleted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT PK_CommonIngredients PRIMARY KEY CLUSTERED (Id)
)
