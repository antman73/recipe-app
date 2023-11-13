using Recipes.Shared.Domain;

namespace Recipes.Api.Models
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipes();
        Task<Recipe> GetRecipe(int id);
        Task<bool> SaveRecipe(Recipe recipe);
    }
}
