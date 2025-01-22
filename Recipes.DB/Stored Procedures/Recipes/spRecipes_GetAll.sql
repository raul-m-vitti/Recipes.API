CREATE PROCEDURE [dbo].[spRecipes_GetAll]
AS
BEGIN
	SELECT [Id], [Name], [Description] FROM [dbo].[Recipes]
	WHERE [Deleted] = 0
END