using System.ComponentModel.DataAnnotations;

namespace Recipes.Shared.Domain;

public class Recipe
{
    [Key] public int Id { get; set; }

    [MaxLength(100)] public string Title { get; set; } = null!;

    [MaxLength(2000)] public string Description { get; set; } = null!;

    public byte[]? Image { get; set; }

    public int PrepTimeMinutes { get; set; }

    public int CookTimeMinutes { get; set; }

    public int Servings { get; set; }

    public List<Ingredient>? Ingredients { get; set; }

    public List<Instruction>? Instructions { get; set; }
}