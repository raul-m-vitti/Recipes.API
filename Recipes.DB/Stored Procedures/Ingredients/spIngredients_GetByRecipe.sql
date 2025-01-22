CREATE PROCEDURE [dbo].[spIngredients_GetByRecipe]
	@RecipeId INT
AS
BEGIN
	SELECT [RecipeId], [Order], [Description] FROM [dbo].[Ingredients]
	WHERE [RecipeId] = @RecipeId
	ORDER BY [Order]
END