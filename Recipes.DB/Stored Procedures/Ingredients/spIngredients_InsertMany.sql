CREATE PROCEDURE [dbo].[spIngredients_InsertMany]
	@Ingredients [dbo].[IngredientsListType] READONLY
AS
BEGIN
	INSERT INTO [dbo].[Ingredients] ([RecipeId], [CommonIngredientId], [Order], [Description])
	SELECT [RecipeId], [CommonIngredientId], [Order], [Description]
	FROM @Ingredients
END