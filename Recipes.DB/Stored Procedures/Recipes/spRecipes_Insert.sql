CREATE PROCEDURE [dbo].[spRecipes_Insert]
	@Name NVARCHAR(50),
	@Description NVARCHAR(500)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Recipes] ([Name], [Description])
	VALUES (@Name, @Description)

	SELECT [Id], [Name], [Description] FROM [dbo].[Recipes]
	WHERE [Id] = SCOPE_IDENTITY()
END