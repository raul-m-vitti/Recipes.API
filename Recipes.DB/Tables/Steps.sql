CREATE TABLE [dbo].[Steps]
(
    [RecipeId] INT NOT NULL,
    [Order] SMALLINT NOT NULL, 
    [Description] NVARCHAR(500) NOT NULL, 
    CONSTRAINT PK_Steps PRIMARY KEY CLUSTERED (RecipeId, [Order]),
    CONSTRAINT FK_Steps_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes (Id)
)