CREATE PROCEDURE [dbo].[spCommonIngredients_Insert]
	@Name NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[CommonIngredients] ([Name])
	VALUES (@Name)

	SELECT [Id], [Name] FROM [dbo].[CommonIngredients]
	WHERE [Id] = SCOPE_IDENTITY()
END