using System.ComponentModel.DataAnnotations;

namespace Recipes.Shared.Domain;

public class RecipeImage
{
    [Key] public int Id { get; set; }

    public int RecipeId { get; set; }

    public byte[]? Image { get; set; }
}