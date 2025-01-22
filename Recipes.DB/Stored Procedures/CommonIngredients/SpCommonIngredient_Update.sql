CREATE PROCEDURE [dbo].[SpCommonIngredient_Update]
	@Id INT,
	@Name NVARCHAR(50)
AS
BEGIN
	UPDATE [dbo].[CommonIngredients]
	SET [Name] = @Name
	WHERE [Id] = @Id
END