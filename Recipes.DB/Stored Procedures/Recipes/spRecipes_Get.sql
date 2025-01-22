CREATE PROCEDURE [dbo].[spRecipes_Get]
	@Id INT
AS
BEGIN
	SELECT [Id], [Name], [Description] FROM [dbo].[Recipes]
	WHERE [Deleted] = 0 AND [Id] = @Id
END