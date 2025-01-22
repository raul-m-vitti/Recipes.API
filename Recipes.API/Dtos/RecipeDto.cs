namespace Recipes.API.Dtos;

public record RecipeDto(
    int Id,
    string Name,
    string Description);

public record CreateRecipeDto(
    string Name,
    string Description);

public record RecipeResponseDto(
    RecipeDto Recipe,
    IEnumerable<IngredientResponseDto> Ingredients,
    IEnumerable<StepDto> Steps);

public record CreateRecipeRequestDto(
    CreateRecipeDto Recipe,
    IEnumerable<CreateIngredientDto> Ingredients,
    IEnumerable<StepDto> Steps);

public record UpdateRecipeRequestDto(
    RecipeDto Recipe,
    IEnumerable<CreateIngredientDto> Ingredients,
    IEnumerable<StepDto> Steps);

public record StepDto(
    short Order,
    string Description);

public record CreateIngredientDto(
    short Order,
    string Description,
    int? CommonIngredientId = null);

public record IngredientResponseDto(
    short Order,
    string Description);