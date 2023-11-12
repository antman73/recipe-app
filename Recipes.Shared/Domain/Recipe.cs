using System.ComponentModel.DataAnnotations;

namespace Recipes.Shared.Domain;

public class Recipe
{
    [Key] public int Id { get; set; }

    [Required][MaxLength(100)] public string Title { get; set; } = null!;

    [Required][MaxLength(2000)] public string Description { get; set; } = null!;

    public byte[]? Image { get; set; }

    [Required]public int PrepTimeMinutes { get; set; }

    [Required]public int CookTimeMinutes { get; set; }

    [Required]public int Servings { get; set; }

    [Required] public List<Ingredient> Ingredients { get; set; } = new();

    [Required]public List<Instruction> Instructions { get; set; } = new();
}