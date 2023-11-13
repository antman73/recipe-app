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

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetRecipeAsync(int id)
    {
        var recipe = await _recipeRepository.GetRecipe(id);
        return Ok(recipe);
    }

    [HttpPost]
    public async Task<IActionResult> SaveRecipe([FromBody] Recipe recipe)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdRecipe = await _recipeRepository.SaveRecipe(recipe);

        return Ok(createdRecipe);
    }

}