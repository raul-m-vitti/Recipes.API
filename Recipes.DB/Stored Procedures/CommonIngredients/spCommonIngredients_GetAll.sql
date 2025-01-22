CREATE PROCEDURE [dbo].[spCommonIngredients_GetAll]
AS
BEGIN
	SELECT [Id], [Name] FROM [dbo].[CommonIngredients]
	WHERE [Deleted] = 0
END