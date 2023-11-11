using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Models;
using Recipes.Shared.Domain;

namespace Recipes.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipesController : Controller
{
    private readonly IRecipeRepository _recipeRepository;

    public RecipesController(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecipesAsync()
    {
        return Ok(await _recipeRepository.GetAllRecipes());
    }

    [HttpGet("api/[controller]/{id:int}")]
    public async Task<IActionResult> GetRecipeAsync(int id)
    {
        return Ok(await _recipeRepository.GetRecipe(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewRecipe([FromBody] Recipe? recipe)
    {
        if (recipe?.Ingredients == null || recipe.Instructions == null)
            return BadRequest();

        // Having to validate manually because cannot use EditForm properly in modal
        if(string.IsNullOrEmpty(recipe.Title))
            ModelState.AddModelError("Title", "Title is required");
        if(string.IsNullOrEmpty(recipe.Description))
            ModelState.AddModelError("Description", "Description is required");
        if(recipe.Image == null)
            ModelState.AddModelError("Image", "Image is required");
        if(recipe.CookTimeMinutes == 0)
            ModelState.AddModelError("Cook Time", "Cook Time is required");
        if(recipe.PrepTimeMinutes == 0)
            ModelState.AddModelError("Prep Time", "Prep Time is required");
        if(recipe.Servings == 0)
            ModelState.AddModelError("Servings", "Servings is required");

        if(recipe.Ingredients.Count == 0)
            ModelState.AddModelError("Ingredients", "At least one ingredient is required");

        if(recipe.Instructions.Count == 0)
            ModelState.AddModelError("Instructions", "At least one instruction is required");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdRecipe = await _recipeRepository.CreateRecipe(recipe);

        return Ok(createdRecipe);
    }
}