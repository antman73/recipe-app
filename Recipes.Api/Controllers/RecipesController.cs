using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Models;
using Recipes.Shared.Entities;

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
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _recipeRepository.GetAllRecipes(""));
    }

    [HttpGet]
    [Route("{filter}")]
    public async Task<IActionResult> GetAllAsync(string filter)
    {
        return Ok(await _recipeRepository.GetAllRecipes(filter));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var recipe = await _recipeRepository.GetRecipe(id);
        return Ok(recipe);
    }

    [HttpPost]
    public async Task<IActionResult> PostRecipe([FromBody] DtoRecipe recipe)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdRecipe = await _recipeRepository.CreateRecipe(recipe);

        return Ok(createdRecipe);
    }

    [HttpPut]
    public async Task<IActionResult> PutRecipe([FromBody] DtoRecipe recipe)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdRecipe = await _recipeRepository.UpdateRecipe(recipe);

        return Ok(createdRecipe);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        var isDeleted = await _recipeRepository.DeleteRecipe(id);
        return Ok(isDeleted);
    }
}