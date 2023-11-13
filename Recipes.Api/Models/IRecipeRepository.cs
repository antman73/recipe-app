using Recipes.Shared.Entities;

namespace Recipes.Api.Models
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<DtoRecipe>> GetAllRecipes(string filter);

        Task<DtoRecipe?> GetRecipe(int id);

        Task<bool> CreateRecipe(DtoRecipe recipe);

        Task<bool> UpdateRecipe(DtoRecipe recipe);
    }
}