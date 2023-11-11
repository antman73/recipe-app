using Microsoft.EntityFrameworkCore;
using Recipes.Shared.Domain;

namespace Recipes.Api.Models
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipes();
        Task<bool> CreateRecipe(Recipe recipe);
        Task<Recipe?> GetRecipe(int id);
    }

    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _appDbContext;

        public RecipeRepository(RecipeDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            try
            {
                return await _appDbContext.Recipes.OrderBy(x => x.Description).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> CreateRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe?> GetRecipe(int id)
        {
            throw new NotImplementedException();
        }
    }
}
