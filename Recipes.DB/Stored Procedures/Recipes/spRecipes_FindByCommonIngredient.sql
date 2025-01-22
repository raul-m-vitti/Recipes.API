CREATE PROCEDURE [dbo].[spRecipes_FindByCommonIngredient]
    @CommonIngredientId INT,
    @Page INT = 1
AS
BEGIN
    DECLARE @PageSize INT = 100;

SELECT DISTINCT r.[Id], r.[Name], r.[Description]
FROM [dbo].[Recipes] r
    INNER JOIN [dbo].[Ingredients] i ON r.[Id] = i.[RecipeId]
WHERE r.[Deleted] = 0
  AND i.[CommonIngredientId] = @CommonIngredientId
ORDER BY r.[Id]
OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END