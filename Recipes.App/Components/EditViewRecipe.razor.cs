using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Recipes.App.Services;
using Recipes.Shared.Domain;

namespace Recipes.App.Components;

public partial class EditViewRecipe
{
    [Inject] private IRecipeDataService RecipeDataService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
    [Parameter] public int RecipeId { get; set; }

    private Recipe? _recipe;
    private Ingredient _newIngredient = new();
    private Instruction _newInstruction = new();

    protected override async Task OnInitializedAsync()
    {
        _recipe = await RecipeDataService.GetRecipe(RecipeId);
    }

    private void AddIngredient()
    {
        _recipe!.Ingredients.Add(_newIngredient);
        _newIngredient = new Ingredient();
    }

    private void RemoveIngredient(Ingredient ingredient)
    {
        _recipe!.Ingredients.Remove(ingredient);
    }

    private void AddInstruction()
    {
        _recipe!.Instructions.Add(_newInstruction);
        _newInstruction = new Instruction();
    }

    private void RemoveInstruction(Instruction instruction)
    {
        _recipe!.Instructions.Remove(instruction);
    }

    private async Task SaveRecipe()
    {
        var isSaved = await RecipeDataService.SaveRecipe(_recipe!);
        if(isSaved) 
            await BlazoredModal.CloseAsync(ModalResult.Ok(isSaved));
        else
            await JsRuntime.InvokeVoidAsync("alert", "Error during save!"); 
    }
}