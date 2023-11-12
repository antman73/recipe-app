using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Recipes.App.Components;
using Recipes.App.Services;
using Recipes.Shared.Domain;

namespace Recipes.App.Pages;

public partial class Index
{
    [Inject] private IRecipeDataService RecipeDataService { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private List<Recipe>? _recipeList;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        _recipeList = (await RecipeDataService.GetAllRecipes()).ToList();
    }

    private async Task EditRecipe(Recipe recipe)
    {
        var editViewRecipe =  ModalService.Show<EditViewRecipe>("View/Edit Recipe", 
            new ModalParameters().Add("RecipeId", recipe.Id), 
            new ModalOptions
        {
            Size = ModalSize.Large
        });
        var result = await editViewRecipe.Result;
        if (result.Confirmed && (bool?)result.Data == false)
        {
            await JsRuntime.InvokeVoidAsync("alert", "Recipe not saved!"); 
        }

        await LoadData();
    }
}