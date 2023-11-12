using Microsoft.AspNetCore.Components;
using Recipes.App.Services;
using Recipes.Shared.Domain;

namespace Recipes.App.Components;

public partial class ViewRecipe
{
    [Inject] private IRecipeDataService RecipeDataService { get; set; } = null!;

    [Parameter] public int? Id { get; set; }

    [Parameter] public EventCallback<Recipe> RecipeSaved { get; set; }

    private Recipe _recipe = new();
    private Ingredient _newIngredient = new();
    private Instruction _newInstruction = new();

    protected override async Task OnParametersSetAsync()
    {
        _recipe = (Id ?? 0) > 0
            ? await RecipeDataService.GetRecipe(Id!.Value)
            : new Recipe { Ingredients = new List<Ingredient>(), Instructions = new List<Instruction>() };
    }

    private void AddIngredient()
    {
        _recipe.Ingredients.Add(_newIngredient);
        _newIngredient = new Ingredient();
    }

    private void RemoveIngredient(Ingredient ingredient)
    {
        _recipe.Ingredients.Remove(ingredient);
    }

    private void AddInstruction()
    {
        _recipe.Instructions.Add(_newInstruction);
        _newInstruction = new Instruction();
    }

    private void RemoveInstruction(Instruction instruction)
    {
        _recipe.Instructions.Remove(instruction);
    }

    private async Task SaveRecipe()
    {
        await RecipeSaved.InvokeAsync();
    }
}