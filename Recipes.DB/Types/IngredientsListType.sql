CREATE TYPE [dbo].[IngredientsListType] AS TABLE
(
	[RecipeId] INT,
	[CommonIngredientId] INT,
	[Order] INT,
	[Description] NVARCHAR(100)
)
