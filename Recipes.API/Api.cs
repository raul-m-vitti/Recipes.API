using Recipes.API.Services;
using Recipes.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Recipes.API;

public static class Api
{
    //Utilizando abordagem de Minimal API, caso a API cresça podemos separar cada grupo em arquivos diferentes como uma classe parcial
    public static void MapApi(this WebApplication app)
    {
        //Recipes
        app.MapGet("/recipes", GetAllRecipes).WithTags("Recipes");
        app.MapGet("/recipes/{id}", GetRecipe).WithTags("Recipes");
        app.MapPost("/recipes", CreateRecipe).WithTags("Recipes");
        app.MapPut("/recipes", UpdateRecipe).WithTags("Recipes");
        app.MapDelete("/recipes/{id}", DeleteRecipe).WithTags("Recipes");
        app.MapGet("/recipes/search/{query}", FindRecipe).WithTags("Recipes");
        app.MapGet("/recipes/search/common-ingredient/{commonIngredientId}", FindRecipeByCommonIngredient).WithTags("Recipes");

        //Common Ingredients
        app.MapGet("/common-ingredients", GetAllCommonIngredients).WithTags("Common Ingredients");
        app.MapPost("/common-ingredients", CreateCommonIngredient).WithTags("Common Ingredients");
        app.MapPut("/common-ingredients", UpdateCommonIngredient).WithTags("Common Ingredients");
        app.MapDelete("/common-ingredients", DeleteCommonIngredient).WithTags("Common Ingredients");
    }


    /// <summary>
    /// Listar todas as receitas
    /// </summary>
    /// <returns>Coleção de receitas</returns>
    /// <response code="200">Sucesso</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> GetAllRecipes(RecipeService service, int page = 1)
    {
        var response = await service.GetAll(page);

        return Results.Ok(response);
    }
    
    /// <summary>
    /// Consultar uma receita
    /// </summary>
    /// <param name="id">Identificador da receita</param>
    /// <returns>Receita completa</returns>
    /// <response code="200">Sucesso</response>
    /// <response code="404">Não encontrado</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> GetRecipe(RecipeService service, int id)
    {
        var response = await service.Get(id);

        return Results.Ok(response);
    }

    /// <summary>
    /// Cadastra uma receita
    /// </summary>
    /// <param name="dto">Dados da receita</param>
    /// <remarks>Objeto JSON</remarks>
    /// <returns>Receita cadastrada</returns>
    /// <response code="201">Sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public static async Task<IResult> CreateRecipe(RecipeService service, CreateRecipeRequestDto dto)
    {
        var response = await service.Create(dto);

        return Results.Created($"/recipes/{response.Recipe.Id}", response);
    }

    /// <summary>
    /// Edita uma receita
    /// </summary>
    /// <param name="dto">Dados da receita</param>
    /// <remarks>Objeto JSON</remarks>
    /// <returns>Receita atualizada</returns>
    /// <response code="201">Sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Não encontrado</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> UpdateRecipe(RecipeService service, UpdateRecipeRequestDto dto)
    {
        var response = await service.Update(dto);

        return Results.Ok(response);
    }

    /// <summary>
    /// Remove uma receita
    /// </summary>
    /// <param name="id">Identificador da receita</param>
    /// <remarks>Objeto JSON</remarks>
    /// <returns>Nada</returns>
    /// <response code="201">Sucesso</response>
    /// <response code="404">Não encontrado</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> DeleteRecipe(RecipeService service, int id)
    {
        await service.Delete(id);

        return Results.NoContent();
    }

    /// <summary>
    /// Busca receitas por texto
    /// </summary>
    /// <param name="query">Texto a ser buscado</param>
    /// <returns>Lista de receitas</returns>
    /// <response code="200">Sucesso</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> FindRecipe(RecipeService service, string query, int page = 1)
    {
        var response = await service.FindByNameAndDescription(query, page);

        return Results.Ok(response);
    }

    /// <summary>
    /// Listar receitas por ingrediente comum
    /// </summary>
    /// <param name="commonIngredientId">Identificador do ingrediente comum</param>
    /// <returns>Lista de receitas</returns>
    /// <response code="200">Sucesso</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> FindRecipeByCommonIngredient(RecipeService service, int commonIngredientId, int page = 1)
    {
        var response = await service.FindByCommonIngredient(commonIngredientId, page);

        return Results.Ok(response);
    }

    /// <summary>
    /// Listar todas os ingredientes comuns
    /// </summary>
    /// <returns>Coleção de ingredientes comuns</returns>
    /// <response code="200">Sucesso</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> GetAllCommonIngredients(CommonIngredientService service)
    {
        var response = await service.GetAll();

        return Results.Ok(response);
    }

    /// <summary>
    /// Cadastra um ingrediente comum
    /// </summary>
    /// <param name="dto">Dados do ingrediente</param>
    /// <remarks>Objeto JSON</remarks>
    /// <returns>Ingrediente comum cadastrado</returns>
    /// <response code="201">Sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public static async Task<IResult> CreateCommonIngredient(CommonIngredientService service, CreateCommonIngredientDto dto)
    {
        var response = await service.Create(dto);

        return Results.Created($"/common-ingredients/{response.Id}", response);
    }

    /// <summary>
    /// Edita um ingrediente comum
    /// </summary>
    /// <param name="dto">Dados do ingrediente comum</param>
    /// <remarks>Objeto JSON</remarks>
    /// <returns>Ingrediente comum atualizado</returns>
    /// <response code="201">Sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Não encontrado</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> UpdateCommonIngredient(CommonIngredientService service, CommonIngredientDto dto)
    {
        var response = await service.Update(dto);

        return Results.Ok(response);
    }

    /// <summary>
    /// Remove um ingrediente comum
    /// </summary>
    /// <param name="id">Identificador do ingrediente comum</param>
    /// <remarks>Objeto JSON</remarks>
    /// <returns>Nada</returns>
    /// <response code="201">Sucesso</response>
    /// <response code="404">Não encontrado</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> DeleteCommonIngredient(CommonIngredientService service, int id)
    {
        await service.Delete(id);

        return Results.NoContent();
    }
}