CREATE PROCEDURE [dbo].[spRecipes_Delete]
	@Id INT
AS
BEGIN
	UPDATE [dbo].[Recipes]
	SET [Deleted] = 1
	WHERE [Id] = @Id
END