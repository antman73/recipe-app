using Microsoft.AspNetCore.Components;
using Recipes.App.Services;
using Recipes.Shared.Domain;

namespace Recipes.App.Components;

public partial class EditRecipe
{
    [Parameter] public int? Id { get; set; }

    [Inject] private IRecipeDataService RecipeDataService { get; set; } = null!;

    private Recipe _recipe = new();

    protected override async Task OnParametersSetAsync()
    {
        if ((Id ?? 0) > 0) await LoadRecipe(Id!.Value);
    }

    private async Task LoadRecipe(int id)
    {
        _recipe = await RecipeDataService.GetRecipe(id);
    }

    private Task SaveRecipe()
    {
        throw new NotImplementedException();
    }
}