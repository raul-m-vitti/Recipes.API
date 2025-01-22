CREATE PROCEDURE [dbo].[spRecipes_Update]
	@Id INT,
	@Name NVARCHAR(50),
	@Description NVARCHAR(50),
	@Ingredients [dbo].[IngredientsListType] READONLY,
	@Steps [dbo].[StepsListType] READONLY
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM [dbo].[Recipes] WHERE [Id] = @Id AND [Deleted] = 1)
	BEGIN
		RETURN -1
	END

	DECLARE @IngredientsOrderList TABLE ([Order] SMALLINT)
	DECLARE @StepsOrderList TABLE ([Order] SMALLINT)

	INSERT INTO @IngredientsOrderList ([Order])
	SELECT [Order] FROM [dbo].[Ingredients] WHERE [RecipeId] = @Id

	INSERT INTO @StepsOrderList ([Order])
	SELECT [Order] FROM [dbo].[Steps] WHERE [RecipeId] = @Id

	-- Update na Recipe

	UPDATE [dbo].[Recipes]
	SET [Name] = @Name, [Description] = @Description
	WHERE [Id] = @Id;

	-- Update nos Ingredients

	INSERT INTO [dbo].[Ingredients] ([RecipeId], [CommonIngredientId], [Order], [Description])
	SELECT @Id, [CommonIngredientId], [Order], [Description]
	FROM @Ingredients
	WHERE [Order] NOT IN(SELECT [Order] FROM @IngredientsOrderList)

	UPDATE i SET
	i.[CommonIngredientId] = il.[CommonIngredientId],
	i.[Description] = il.[Description]
	FROM [dbo].[Ingredients] i
	INNER JOIN @Ingredients il ON i.[RecipeId] = il.[RecipeId]
	WHERE i.[Order] = il.[Order]
	AND (i.[Description] <> il.[Description]
	OR i.[CommonIngredientId] <> il.[CommonIngredientId])

	DELETE FROM [dbo].[Ingredients]
	WHERE [RecipeId] = @Id
	AND [Order] NOT IN(SELECT [Order] FROM @Ingredients)


	-- Update nos Steps
	
	INSERT INTO [dbo].[Steps] ([RecipeId], [Order], [Description])
	SELECT @Id, [Order], [Description]
	FROM @Steps
	WHERE [Order] NOT IN(SELECT [Order] FROM @StepsOrderList)

	UPDATE s
	SET s.[Description] = sl.[Description]
	FROM [dbo].[Steps] s
	INNER JOIN @Steps sl ON s.[RecipeId] = sl.[RecipeId]
	WHERE s.[Order] = sl.[Order]
	AND s.[Description] <> sl.[Description]

	DELETE FROM [dbo].[Steps]
	WHERE [RecipeId] = @Id
	AND [Order] NOT IN(SELECT [Order] FROM @Steps)


	-- Retornar nova Recipe

	SELECT [Id], [Name], [Description] FROM [dbo].[Recipes]
	WHERE [Id] = @Id
END