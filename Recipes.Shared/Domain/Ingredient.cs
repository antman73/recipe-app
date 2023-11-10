using System.ComponentModel.DataAnnotations;

namespace Recipes.Shared.Domain;

public class Ingredient
{
    [Key] public int Id { get; set; }

    public Recipe Recipe { get; set; } = null!;

    [MaxLength(255)] public string IngredientText { get; set; } = null!;

    public int SortOrder { get; set; }
}