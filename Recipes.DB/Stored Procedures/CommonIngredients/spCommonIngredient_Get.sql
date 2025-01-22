CREATE PROCEDURE [dbo].[spCommonIngredient_Get]
	@Id INT
AS
BEGIN
	SELECT [Id], [Name] FROM [dbo].[CommonIngredients]
	WHERE [Deleted] = 0 AND [Id] = @Id
END
