namespace Recipes.API.Models;

public class RecipeModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public RecipeModel() { }
}

public class RecipeStepModel
{
    public int RecipeId { get; set; }
    public short Order { get; set; }
    public string? Description { get; set; }

    public RecipeStepModel() { }
}

public class RecipeIngredientModel
{
    public int RecipeId { get; set; }
    public int? CommonIngredientId { get; set; }
    public short Order { get; set; }
    public string? Description { get; set; }

    public RecipeIngredientModel() { }
}