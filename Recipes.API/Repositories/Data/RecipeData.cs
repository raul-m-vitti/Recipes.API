using Dapper;
using Recipes.API.Models;

namespace Recipes.API.Repositories.Data;

public class RecipeData
{
    private readonly DataAccess _db;

    public RecipeData(DataAccess db)
    {
        _db = db;
    }

    public async Task<RecipeModel?> Get(int id)
    {
        var query = await _db.LoadData<RecipeModel, dynamic>(
            "dbo.spRecipes_Get",
            new { Id = id });

        return query.FirstOrDefault();
    }

    public async Task<IEnumerable<RecipeModel>> GetAll(int page)
    {
        return await _db.LoadData<RecipeModel, dynamic>(
            "dbo.spRecipes_GetAll",
            new { Page = page });
    }

    public async Task<IEnumerable<RecipeModel>> FindByNameAndDescription(string textQuery, int page)
    {
        return await _db.LoadData<RecipeModel, dynamic>(
            "dbo.spRecipes_FindByNameAndDescription",
            new { TextQuery = textQuery, Page = page });
    }

    public async Task<IEnumerable<RecipeModel>> FindByCommonIngredient(int commonIngredientId, int page)
    {
        return await _db.LoadData<RecipeModel, dynamic>(
            "dbo.spRecipes_FindByCommonIngredient",
            new { CommonIngredientId = commonIngredientId, Page = page });
    }

    public async Task<RecipeModel> Create(string name, string? description)
    {
        var query = await _db.LoadData<RecipeModel, dynamic>(
            "dbo.spRecipes_Insert",
            new { Name = name, Description = description });

        return query.First();
    }

    public async Task Update(int id, string name, string? description, IEnumerable<RecipeIngredientModel> ingredients, IEnumerable<RecipeStepModel> steps)
    {
        var ingredientsDt = ingredients.ToDataTable().AsTableValuedParameter("dbo.IngredientsListType");
        var stepsDt = steps.ToDataTable().AsTableValuedParameter("dbo.StepsListType");

        await _db.ExecuteCommand<dynamic>(
            "dbo.spRecipes_Update",
            new { Id= id, Name = name, Description = description, Ingredients = ingredientsDt, Steps = stepsDt });
    }

    public async Task Delete(int id)
    {
        await _db.ExecuteCommand<dynamic>(
            "dbo.spRecipes_Delete",
            new { Id = id });
    }

    public async Task<IEnumerable<RecipeStepModel>> GetStepsByRecipe(int recipeId)
    {
        return await _db.LoadData<RecipeStepModel, dynamic>(
            "dbo.spSteps_GetByRecipe",
            new { RecipeId = recipeId });
    }

    public async Task CreateStepsList(IEnumerable<RecipeStepModel> steps)
    {
        var stepsDt = steps.ToDataTable().AsTableValuedParameter("dbo.StepsListType");

        await _db.ExecuteCommand<dynamic>(
            "spSteps_InsertMany",
            new { Steps = stepsDt });
    }

    public async Task CreateIngredientsList(IEnumerable<RecipeIngredientModel> ingredients)
    {
        var ingredientsDt = ingredients.ToDataTable().AsTableValuedParameter("dbo.IngredientsListType");

        await _db.ExecuteCommand<dynamic>(
            "spIngredients_InsertMany",
            new { Ingredients = ingredientsDt });
    }

    public async Task<IEnumerable<RecipeIngredientModel>> GetIngredientsByRecipe(int recipeId)
    {
        return await _db.LoadData<RecipeIngredientModel, dynamic>(
            "dbo.spIngredients_GetByRecipe",
            new { RecipeId = recipeId });
    }
}