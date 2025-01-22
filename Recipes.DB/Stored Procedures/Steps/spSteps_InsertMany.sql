CREATE PROCEDURE [dbo].[spSteps_InsertMany]
	@Steps [dbo].[StepsListType] READONLY
AS
BEGIN
	INSERT INTO [dbo].[Steps] ([RecipeId], [Order], [Description])
	SELECT [RecipeId], [Order], [Description]
	FROM @Steps
END