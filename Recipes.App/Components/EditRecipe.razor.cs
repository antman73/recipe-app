﻿using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Recipes.App.Services;
using Recipes.Shared.Domain;
using Recipes.Shared.Entities;

namespace Recipes.App.Components;

public partial class EditRecipe
{
    [Inject] private IRecipeDataService RecipeDataService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;

    private DtoRecipe? _recipe;
    private Ingredient _newIngredient = new();
    private Instruction _newInstruction = new();

    protected override async Task OnInitializedAsync()
    {
        _recipe = new DtoRecipe { Ingredients = new List<Ingredient>(), Instructions = new List<Instruction>() };
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

    private async Task SaveRecipe()
    {
        var isSaved = await RecipeDataService.CreateRecipe(_recipe!);
        if (isSaved)
        {
            //Close dialog
            await BlazoredModal.CloseAsync(ModalResult.Ok(isSaved));
        }
        else
            await JsRuntime.InvokeVoidAsync("alert", "Error during save!");
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
            }
        }
    }
}