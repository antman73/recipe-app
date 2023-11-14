using Recipes.Shared.Entities;

namespace Recipes.App.Services
{
    public interface IRecipeDataService
    {
        public Task<IEnumerable<DtoRecipe>> GetAllRecipes();

        public Task<IEnumerable<DtoRecipe>> GetRecipes(string filter);

        public Task<DtoRecipe?> GetRecipe(int id);

        public Task<bool> CreateRecipe(DtoRecipe recipe);

        public Task<bool> UpdateRecipe(DtoRecipe recipe);

        public Task<bool> DeleteRecipe(int id);
    }
}