CREATE TABLE [dbo].[Ingredients]
(
    [RecipeId] INT NOT NULL,
    [Order] SMALLINT NOT NULL,
    [CommonIngredientId] INT,
    [Description] NVARCHAR(100) NOT NULL, 
    CONSTRAINT PK_Ingredients PRIMARY KEY CLUSTERED (RecipeId, [Order]),
    CONSTRAINT FK_Ingredients_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes (Id),
    CONSTRAINT FK_Ingredients_CommonIngredients FOREIGN KEY (CommonIngredientId) REFERENCES CommonIngredients (Id)
)