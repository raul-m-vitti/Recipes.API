CREATE PROCEDURE [dbo].[spCommonIngredients_Delete]
	@Id INT
AS
BEGIN
	UPDATE [dbo].[CommonIngredients]
	SET [Deleted] = 1
	WHERE [Id] = @Id

	UPDATE [dbo].[Ingredients]
	SET [CommonIngredientId] = NULL
	WHERE [CommonIngredientId] = @Id
END
