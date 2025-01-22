using Mapster;
using Recipes.API.Dtos;
using Recipes.API.Models;
using Recipes.API.Repositories.Data;

namespace Recipes.API.Services;

public class RecipeService
{
    private readonly RecipeData _data;


    public RecipeService(RecipeData recipeData)
    {
        _data = recipeData;
    }

    public async Task<IEnumerable<RecipeDto>> GetAll(int page)
    {
        var entities = await _data.GetAll(page);

        return entities.Adapt<IEnumerable<RecipeDto>>();
    }

    public async Task<RecipeResponseDto> Get(int id)
    {
        var entity = await _data.Get(id);

        if (entity == null)
            throw new KeyNotFoundException();

        var ingredients = await _data.GetIngredientsByRecipe(id);
        var steps = await _data.GetStepsByRecipe(id);

        return new RecipeResponseDto(
            entity.Adapt<RecipeDto>(),
            ingredients.Adapt<IEnumerable<IngredientResponseDto>>(),
            steps.Adapt<IEnumerable<StepDto>>());
    }

    public async Task<RecipeResponseDto> Create(CreateRecipeRequestDto dto)
    {
        if (!ValidateRecipeDto(dto, out Exception? ex))
            throw ex!;

        var entity = await _data.Create(dto.Recipe.Name, dto.Recipe.Description);

        var ingredients = dto.Ingredients.Adapt<IEnumerable<RecipeIngredientModel>>();
        ingredients = ingredients.Select(x => { x.RecipeId = entity.Id; return x; });

        var steps = dto.Steps.Adapt<IEnumerable<RecipeStepModel>>();
        steps = steps.Select(x => { x.RecipeId = entity.Id; return x; });

        await _data.CreateIngredientsList(ingredients);
        await _data.CreateStepsList(steps);

        return new RecipeResponseDto(
            entity.Adapt<RecipeDto>(),
            ingredients.Adapt<IEnumerable<IngredientResponseDto>>(),
            steps.Adapt<IEnumerable<StepDto>>());
    }

    public async Task<RecipeResponseDto> Update(UpdateRecipeRequestDto dto)
    {
        if (!ValidateRecipeDto(dto.Adapt<CreateRecipeRequestDto>(), out Exception? ex))
            throw ex!;

        var entity = await _data.Get(dto.Recipe.Id);

        if (entity == null)
            throw new KeyNotFoundException();

        var ingredients = dto.Ingredients.Adapt<IEnumerable<RecipeIngredientModel>>();
        ingredients = ingredients.Select(x => { x.RecipeId = dto.Recipe.Id; return x; });

        var steps = dto.Steps.Adapt<IEnumerable<RecipeStepModel>>();
        steps = steps.Select(x => { x.RecipeId = dto.Recipe.Id; return x; });

        //Estou fazendo a parte lógica do update na proc para demonstrar que ambas as abordagens são possíveis
        await _data.Update(dto.Recipe.Id, dto.Recipe.Name, dto.Recipe.Description, ingredients, steps);

        return await Get(dto.Recipe.Id);
    }

    public async Task Delete(int id)
    {
        var entity = await _data.Get(id);

        if (entity == null)
            throw new KeyNotFoundException();

        await _data.Delete(id);
    }

    public async Task<IEnumerable<RecipeDto>> FindByNameAndDescription(string query, int page)
    {
        if(string.IsNullOrWhiteSpace(query))
            throw new ArgumentNullException("Busca vazia");

        var results = await _data.FindByNameAndDescription(query, page);

        return results.Adapt<IEnumerable<RecipeDto>>();
    }
    
    public async Task<IEnumerable<RecipeDto>> FindByCommonIngredient(int commonIngredientId, int page)
    {
        var results = await _data.FindByCommonIngredient(commonIngredientId, page);

        return results.Adapt<IEnumerable<RecipeDto>>();
    }

    //Validando no serviço, tambem pode-se validar por Fluent Validation ou outras bibliotecas
    //Decido tomar essa abordagem para não adicionar mais complexidade ao projeto
    private static bool ValidateRecipeDto(CreateRecipeRequestDto dto, out Exception? ex)
    {
        if (dto.Recipe == null
            || string.IsNullOrWhiteSpace(dto.Recipe.Name)
            || dto.Recipe.Name.Length > 50)
        {
            ex = new FormatException("Receita");
            return false;
        }


        if (dto.Ingredients == null
            || !dto.Ingredients.Any()
            || dto.Ingredients.Any(x => string.IsNullOrWhiteSpace(x.Description)
            || x.Description.Length > 100
            || x.Order <= 0)
            || dto.Ingredients.GroupBy(x => x.Order).Any(x => x.Count() > 1))
        {
            ex = new FormatException("Ingredientes");
            return false;
        }

        if (dto.Steps == null
            || !dto.Steps.Any()
            || dto.Steps.Any(x => string.IsNullOrWhiteSpace(x.Description) 
            || x.Description.Length > 500
            || x.Order <= 0)
            || dto.Steps.GroupBy(x => x.Order).Any(x => x.Count() > 1))
        {
            ex = new FormatException("Passos da receita");
            return false;
        }

        ex = null;
        return true;
    }
}