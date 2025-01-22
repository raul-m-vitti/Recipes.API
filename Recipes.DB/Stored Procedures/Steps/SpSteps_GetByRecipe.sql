CREATE PROCEDURE [dbo].[SpSteps_GetByRecipe]
	@RecipeId INT
AS
BEGIN
	SELECT [RecipeId], [Order], [Description] FROM [dbo].[Steps]
	WHERE [RecipeId] = @RecipeId
	ORDER BY [Order]
END