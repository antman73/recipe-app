using System.ComponentModel.DataAnnotations;

namespace Recipes.Shared.Domain;

public class Ingredient
{
    [Key] public int Id { get; set; }

    public int RecipeId { get; set; }

    [MaxLength(255)] public string IngredientText { get; set; } = null!;

    public int SortOrder { get; set; }
}