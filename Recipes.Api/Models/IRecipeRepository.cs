using System.Data;
using Microsoft.EntityFrameworkCore;
using Recipes.Shared.Domain;

namespace Recipes.Api.Models
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipes();
        Task<Recipe> GetRecipe(int id);
        Task<bool> SaveRecipe(Recipe recipe);
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

        public async Task<Recipe> GetRecipe(int id)
        {
            try
            {
                var recipe = await _appDbContext.Recipes.FindAsync(id) ?? throw new DataException($"Recipe ID {id} not found!");
                recipe.Ingredients = await _appDbContext.Ingredients.Where(x => x.Recipe == recipe).ToListAsync();
                recipe.Instructions = await _appDbContext.Instructions.Where(x => x.Recipe == recipe).ToListAsync();
                return recipe;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> SaveRecipe(Recipe recipe)
        {
            try
            {
                var existing = await _appDbContext.Recipes.FindAsync(recipe.Id);
                if (existing == null)
                {
                    // Create
                    await _appDbContext.Recipes.AddAsync(recipe);
                    await _appDbContext.SaveChangesAsync();
                    await _appDbContext.Ingredients.AddRangeAsync(recipe.Ingredients);
                    await _appDbContext.Instructions.AddRangeAsync(recipe.Instructions);
                }
                else
                {
                    // Update
                    _appDbContext.Entry(existing).CurrentValues.SetValues(recipe);
                    _appDbContext.Update(existing);
                    _appDbContext.Ingredients.RemoveRange(_appDbContext.Ingredients.Where(x => x.Recipe == recipe));
                    _appDbContext.Instructions.RemoveRange(_appDbContext.Instructions.Where(x => x.Recipe == recipe));
                    await _appDbContext.Ingredients.AddRangeAsync(recipe.Ingredients);
                    await _appDbContext.Instructions.AddRangeAsync(recipe.Instructions);
                    await _appDbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
