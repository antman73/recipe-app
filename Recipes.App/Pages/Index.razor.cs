using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Recipes.App.Components;
using Recipes.App.Services;
using Recipes.Shared.Entities;

namespace Recipes.App.Pages;

public partial class Index
{
    [Inject] private IRecipeDataService RecipeDataService { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private string _recipeFilter = "";

    private List<DtoRecipe>? _recipeList;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        _recipeList = string.IsNullOrEmpty(_recipeFilter) ? 
            (await RecipeDataService.GetAllRecipes()).ToList() : 
            (await RecipeDataService.GetRecipes(_recipeFilter)).ToList();
        await InvokeAsync(StateHasChanged);
    }

    private async Task AddNewRecipe()
    {
        var editViewRecipe = ModalService.Show<EditRecipe>("View/Edit Recipe",
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

    private async Task ViewRecipe(int id)
    {
        await JsRuntime.InvokeVoidAsync("open", $"./ViewRecipe/{id}", "_blank");
    }

    private async Task FilterRecipes(ChangeEventArgs arg)
    {
        if (arg.Value?.ToString() != _recipeFilter)
        {
            _recipeFilter = arg.Value?.ToString() ?? "";
            await LoadData();
        }
    }
}