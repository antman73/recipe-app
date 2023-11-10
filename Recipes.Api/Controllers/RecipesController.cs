using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Models;

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
    public IActionResult GetAllRecipes()
    {
        return Ok(_recipeRepository.GetAllRecipes());
    }

}