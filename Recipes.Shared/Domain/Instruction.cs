using System.ComponentModel.DataAnnotations;

namespace Recipes.Shared.Domain;

public class Instruction
{
    [Key] public int Id { get; set; }

    public Recipe Recipe { get; set; } = null!;

    [MaxLength(1000)] public string InstructionText { get; set; } = null!;

    public int SortOrder { get; set; }
}