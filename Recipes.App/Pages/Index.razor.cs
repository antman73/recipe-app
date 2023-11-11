using Microsoft.AspNetCore.Components;
using Recipes.App.Components;
using Recipes.App.Services;
using Recipes.Shared.Domain;

namespace Recipes.App.Pages;

public partial class Index
{
    [Inject] private IRecipeDataService RecipeDataService { get; set; } = null!;

    private List<Recipe>? _recipeList;
    public Recipe AddRecipeModal { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        _recipeList = (await RecipeDataService.GetAllRecipes()).ToList();
    }

    private async Task AddNewRecipe()
    {
        throw new NotImplementedException();
    }
}