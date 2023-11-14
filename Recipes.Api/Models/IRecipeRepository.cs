using Recipes.Shared.Entities;

namespace Recipes.Api.Models
{
    public interface IRecipeRepository
    {
        public Task<IEnumerable<DtoRecipe>> GetAllRecipes(string filter);

        public Task<DtoRecipe?> GetRecipe(int id);

        public Task<bool> CreateRecipe(DtoRecipe recipe);

        public Task<bool> UpdateRecipe(DtoRecipe recipe);

        public Task<bool> DeleteRecipe(int id);
    }
}