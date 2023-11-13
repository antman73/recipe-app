using Microsoft.AspNetCore.Components;
using Recipes.Shared.Domain;

namespace Recipes.App.Components;

public partial class RecipeCard
{
    [Parameter] public Recipe? Recipe { get; set; }
    [Parameter] public EventCallback<int> OnRecipeClick { get; set; }
    [Parameter] public EventCallback<int> OnEditClick { get; set; }

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
}