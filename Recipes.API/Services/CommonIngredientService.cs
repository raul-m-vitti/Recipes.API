using Mapster;
using Recipes.API.Repositories.Data;
using Recipes.API.Dtos;
using Recipes.API.Models;

namespace Recipes.API.Services;

public class CommonIngredientService
{
    private readonly CommonIngredientData _data;

    public CommonIngredientService(CommonIngredientData data)
    {
        _data = data;
    }

    public async Task<IEnumerable<CommonIngredientDto>> GetAll()
    {
        var entities = await _data.GetAll();

        return entities.Adapt<IEnumerable<CommonIngredientDto>>();
    }
    
    public async Task<CommonIngredientDto> Get(int id)
    {
        var entity = await _data.Get(id);

        if (entity == null)
            throw new KeyNotFoundException();

        return entity.Adapt<CommonIngredientDto>();
    }

    public async Task<CommonIngredientDto> Create(CreateCommonIngredientDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length > 50)
            throw new FormatException("Nome do ingrediente.");

        var entity = await _data.Create(dto.Name);

        return entity.Adapt<CommonIngredientDto>();
    }
    
    public async Task<CommonIngredientDto> Update(CommonIngredientDto dto)
    {
        var entity = await _data.Get(dto.Id);

        if (entity == null)
            throw new KeyNotFoundException();

        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length > 50)
            throw new FormatException("Nome do ingrediente.");

        await _data.Update(dto.Adapt<CommonIngredientModel>());

        return await Get(dto.Id);
    }
    
    public async Task Delete(int id)
    {
        var entity = await _data.Get(id);

        if (entity == null)
            throw new KeyNotFoundException();

        await _data.Delete(id);
    }
}