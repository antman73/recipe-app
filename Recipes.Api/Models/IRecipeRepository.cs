using Recipes.Shared.Domain;

namespace Recipes.Api.Models
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetAllRecipes();
    }

    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _appDbContext;

        public RecipeRepository(RecipeDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _appDbContext.Recipes.OrderBy(x => x.Description);
        }
    }
}
