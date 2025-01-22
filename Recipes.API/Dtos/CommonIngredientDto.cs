using System.ComponentModel.DataAnnotations;

namespace Recipes.API.Dtos;

public record CommonIngredientDto(int Id, string Name);

public record CreateCommonIngredientDto(string Name);