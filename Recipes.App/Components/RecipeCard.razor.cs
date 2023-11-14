using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Recipes.Shared.Entities;

namespace Recipes.App.Components;

public partial class RecipeCard
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public DtoRecipe? Recipe { get; set; }

    private string? _imageUrl;

    protected override void OnInitialized()
    {
        if (Recipe is not null)
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        if (Recipe?.Image != null)
            _imageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(Recipe.Image)}";
    }

    private void ViewRecipe(int recipeId)
    {
        NavigationManager.NavigateTo($"./ViewRecipe/{recipeId}", true);
    }
}