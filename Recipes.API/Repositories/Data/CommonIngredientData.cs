using Recipes.API.Models;

namespace Recipes.API.Repositories.Data;

public class CommonIngredientData
{
    private readonly DataAccess _db;

    public CommonIngredientData(DataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CommonIngredientModel>> GetAll()
    {
        return await _db.LoadData<CommonIngredientModel, dynamic>(
            "dbo.spCommonIngredients_GetAll",
            new { });
    }
    
    public async Task<CommonIngredientModel?> Get(int id)
    {
        var query = await _db.LoadData<CommonIngredientModel, dynamic>(
            "dbo.spCommonIngredients_Get",
            new { Id = id });

        return query.FirstOrDefault();
    }
    
    public async Task<CommonIngredientModel> Create(string name)
    {
        var query = await _db.LoadData<CommonIngredientModel, dynamic>(
            "dbo.spCommonIngredients_Insert",
            new { Name = name });

        return query.First();
    }

    public async Task Update(CommonIngredientModel model)
    {
        await _db.ExecuteCommand<dynamic>(
            "dbo.spCommonIngredients_Insert",
            model);
    }
    
    public async Task Delete(int id)
    {
        await _db.ExecuteCommand<dynamic>(
            "dbo.spCommonIngredients_Delete",
            new { Id = id });
    }
}