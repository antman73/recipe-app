using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Recipes.App.Services;
using Recipes.Shared.Domain;
using Recipes.Shared.Entities;

namespace Recipes.App.Pages;

public partial class ViewRecipe
{
    [Inject] private IRecipeDataService RecipeDataService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    private DtoRecipe? _recipe;
    private Ingredient _newIngredient = new();
    private Instruction _newInstruction = new();
    private string? _imageUrl;
    private bool _editMode;

    protected override async Task OnInitializedAsync()
    {
        await LoadRecipe();
    }

    private async Task LoadRecipe()
    {
        _editMode = false;
        _recipe = await RecipeDataService.GetRecipe(Id);
        await DisplayImage();
    }

    private async Task LoadFile(InputFileChangeEventArgs arg)
    {
        if (arg is { FileCount: 1, File.Size: > 0 })
        {
            var imgFile = arg.File;
            var buffers = new byte[imgFile.Size];
            var byteCount = await imgFile.OpenReadStream().ReadAsync(buffers);
            if (byteCount != imgFile.Size)
            {
                await JsRuntime.InvokeVoidAsync("alert", "File size incorrect!");
            }
            else
            {
                _recipe!.Image = buffers;
                await DisplayImage();
            }
        }
    }

    private async Task DisplayImage()
    {
        _imageUrl = _recipe?.Image != null
            ? _imageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(_recipe.Image)}"
            : "";

        await InvokeAsync(StateHasChanged);
    }

    private void AddIngredient()
    {
        _newIngredient.RecipeId = _recipe!.Id;
        _recipe!.Ingredients.Add(_newIngredient);
        _newIngredient = new Ingredient();
    }

    private async Task RemoveIngredient(Ingredient ingredient)
    {
        if (await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?"))
            _recipe!.Ingredients.Remove(ingredient);
    }

    private void AddInstruction()
    {
        _newInstruction.RecipeId = _recipe!.Id;
        _recipe!.Instructions.Add(_newInstruction);
        _newInstruction = new Instruction();
    }

    private async Task RemoveInstruction(Instruction instruction)
    {
        if (await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?"))
            _recipe!.Instructions.Remove(instruction);
    }

    private async Task UpdateRecipe()
    {
        var isSaved = await RecipeDataService.UpdateRecipe(_recipe!);
        if (isSaved)
        {
            NavigationManager.NavigateTo("./", true, true);
        }
        else
            await JsRuntime.InvokeVoidAsync("alert", "Error during save!");
    }

    private async Task DeleteRecipe()
    {
        if (await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?"))
        {
            var isDeleted = await RecipeDataService.DeleteRecipe(_recipe!.Id);
            if(isDeleted)
                NavigationManager.NavigateTo("./", true, true);
            else
                await JsRuntime.InvokeVoidAsync("alert", "Error during delete!");
        }
    }
}